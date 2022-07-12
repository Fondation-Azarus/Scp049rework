using Synapse.Command;
using Synapse.Api;
using MEC;
using System.Collections.Generic;

namespace Scp049rework.Commands
{
    [CommandInformation(
        Name = "UpgradeInstance",
        Aliases = new[] { "UInstance", "AmeliorerInstance" },
        Description = "Allows you to upgrade an instance.",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = ".UpgradeInstance [Look at a SCP-049-2]"
    )]
    class UpgradeInstanceCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Player.RoleType != RoleType.Scp049)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notScp049Error;
                result.State = CommandResultState.Error;
                return result;
            }

            Player z = context.Player.LookingAt?.GetComponent<Player>();

            if (z == null)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notLookingToAScp0492Error;
                result.State = CommandResultState.Error;
            }

            else if (UnityEngine.Vector3.Distance(context.Player.Position, z.Position) > 3 || z.RoleType != RoleType.Scp0492)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.notLookingToAScp0492Error;
                result.State = CommandResultState.Error;
            }

            else if (!PluginClass.Config.upgradeInstanceEverywhere && context.Player.Room.RoomType != MapGeneration.RoomName.Hcz049)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.wrongRoomError;
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

            else if (PluginClass.alreadyUpgrading)
            {
                result.Message = "";
                result.State = CommandResultState.Error;
            }

            else
            {
                Timing.RunCoroutine(Upgrade(context.Player, z));
                PluginClass.alreadyUpgrading = true;
                result.Message = PluginClass.Translation.ActiveTranslation.startUpgradedScp0492;
                result.State = CommandResultState.Ok;
            }

            return result;
        }

        private IEnumerator<float> Upgrade(Player scp049, Player z)
        {
            UnityEngine.Vector3 position = scp049.Position;
            if (PluginClass.Config.upgradeInstanceImmobilized)
                z.GiveEffect(Synapse.Api.Enum.Effect.Ensnared);


            for (int t = 0; t < 10; t++)
            {
                if (UnityEngine.Vector3.Distance(scp049.Position, z.Position) > 3 || scp049.RoleType != RoleType.Scp049 || z.RoleType != RoleType.Scp0492 || (!PluginClass.Config.upgradeInstanceEverywhere && scp049.Room.RoomType != MapGeneration.RoomName.Hcz049) || position != scp049.Position)
                {
                    if (PluginClass.Config.upgradeInstanceImmobilized)
                        z.GiveEffect(Synapse.Api.Enum.Effect.Ensnared, 0, 0);
                    yield break;
                }

                string upgradeBar = "<b>[";
                for (int i = -1; i < t; i++)
                    upgradeBar += "<color=#00BCFF>▇</color>";

                for (int i = 1; i + t < 10; i++)
                    upgradeBar += "▇";

                upgradeBar += "]</b>";
                scp049.GiveTextHint(upgradeBar, PluginClass.Config.upgradeInstanceDuration/10 + 0.05f);

                yield return Timing.WaitForSeconds(PluginClass.Config.upgradeInstanceDuration / 10);
            }

            if (UnityEngine.Vector3.Distance(scp049.Position, z.Position) <= 3 && scp049.RoleType == RoleType.Scp049 && z.RoleType == RoleType.Scp0492 && (PluginClass.Config.upgradeInstanceEverywhere || scp049.Room.RoomType == MapGeneration.RoomName.Hcz049) && position == scp049.Position)
            {
                z.MaxHealth += PluginClass.Config.upgradeInstanceMaxHealthAdd;
                z.Heal(PluginClass.Config.upgradeInstanceHealAmount);
                z.GiveEffect(Synapse.Api.Enum.Effect.Scp207, PluginClass.Config.upgradeInstanceScp207Intensity);
                z.GiveEffect(Synapse.Api.Enum.Effect.MovementBoost, PluginClass.Config.upgradeInstanceMovementBoostIntensiry);
                if (PluginClass.Config.upgradeInstanceImmobilized)
                    z.GiveEffect(Synapse.Api.Enum.Effect.Ensnared, 0, 0);
                scp049.GiveTextHint(PluginClass.Translation.ActiveTranslation.upgradedScp0492, 4);
            }

            PluginClass.alreadyUpgrading = false;
        }
    }
}