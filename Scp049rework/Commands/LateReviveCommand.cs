using Synapse.Command;
using Synapse;
using System.Linq;

namespace Scp049rework.Commands
{
    [CommandInformation(
        Name = "LateRevive",
        Aliases = new[] { "ReanimationTardive", "LateCure" },
        Description = "Allows you to revive someone if it's normally too late to do so.",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = ".LateRevive [Look at a corpse]"
    )]
    class LateReviveCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Player.RoleID != (int)RoleType.Scp049)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notScp049Error;
                result.State = CommandResultState.Error;
                return result;
            }

            Synapse.Api.Ragdoll r = Synapse.Api.Map.Get.Ragdolls.FirstOrDefault(rag => rag.ragdoll?.gameObject?.transform != null && UnityEngine.Vector3.Distance(rag.ragdoll.gameObject.transform.position, context.Player.Position) < 3);

            if (r == null)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notNearACorpseError;
                result.State = CommandResultState.Error;
                return result;
            }

            if (r.RoleType == RoleType.Scp0492)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notNearACorpseError;
                result.State = CommandResultState.Error;
                return result;
            }

            Synapse.Api.Player p = Server.Get.GetPlayer(r.Owner.PlayerId); ;
            if (p == null)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notNearACorpseError;
                result.State = CommandResultState.Error;
            }

            /*else if (UnityEngine.Vector3.Distance(context.Player.Position, r.GameObject.transform.position) > 5)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notLookingAtACorpseError;
                result.State = CommandResultState.Error;
            }*/

            else if (p.RoleID != (int)RoleType.Spectator || p.OverWatch)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.currentlyAliveError;
                result.State = CommandResultState.Error;
            }

            else if (r.ragdoll.Info.ExistenceTime < 10)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.freshCorpseError;
                result.State = CommandResultState.Error;
            }

            else if (PluginClass.killAmount < PluginClass.Config.lateReviveMinKills)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notEnoughKillsError.Replace("%lateReviveMinKills%", PluginClass.Config.lateReviveMinKills.ToString()).Replace("%current%", PluginClass.killAmount.ToString());
                result.State = CommandResultState.Error;
            }

            else if (PluginClass.reviveAmount < PluginClass.Config.lateReviveMinRevives)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notEnoughRevivesError.Replace("%lateReviveMinRevives%", PluginClass.Config.lateReviveMinRevives.ToString()).Replace("%current%", PluginClass.reviveAmount.ToString());
                result.State = CommandResultState.Error;
            }

            else
            {
                r.Destroy();
                p.RoleType = RoleType.Scp0492;
                context.Player.Heal(System.Math.Min(PluginClass.Config.reviveHealAmount + PluginClass.Config.reviveHealKillAdditive * PluginClass.killAmount + PluginClass.Config.reviveHealReviveMAdditiver * PluginClass.reviveAmount, PluginClass.Config.reviveHealMax));
                PluginClass.reviveAmount++;
                result.Message = PluginClass.Translation.ActiveTranslation.revivedPlayer;
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }
}