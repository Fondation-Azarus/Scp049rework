using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using UnityEngine;
using Synapse.Api.Enum;
using MEC;
using System;

namespace Scp049rework
{
    public class Handler
    {
        public Handler()
        {
            Server.Get.Events.Round.WaitingForPlayersEvent += OnWait;
            Server.Get.Events.Scp.ScpAttackEvent += OnScpAttack;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Scp.Scp049.Scp049ReviveEvent += OnRevive;
        }
        private void OnWait()
        {
            PluginClass.killAmount = 0;
            PluginClass.reviveAmount = 0;
            PluginClass.alreadyUpgrading = false;
        }   // Resets a bunch of static var

        private void OnScpAttack(ScpAttackEventArgs ev)
        {
            if (ev.Scp == null || ev.Scp.RoleID != (int)RoleType.Scp049 || ev.Target.GodMode)
                return;
            
            ev.Scp.Heal(Math.Min(PluginClass.Config.killHealAmount + PluginClass.Config.killHealKillAdditive * PluginClass.killAmount + PluginClass.Config.killHealReviveMAdditiver * PluginClass.reviveAmount, PluginClass.Config.killHealMax));
            PluginClass.killAmount++;
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Player == null)
                return;

            try
            {
                Timing.CallDelayed(0.3f, () =>
                {
                    if (ev.Player.RoleID != (int)RoleType.Scp049)
                        return;

                    ev.Player.SendBroadcast(10, PluginClass.Translation.ActiveTranslation.broadcast);
                    if (PluginClass.Config.maxHP != 0)
                        ev.Player.MaxHealth = PluginClass.Config.maxHP;
                    if (PluginClass.Config.intialHP != 0)
                        ev.Player.Health = PluginClass.Config.intialHP;
                    if (PluginClass.Config.regenAmout != 0)
                        Timing.RunCoroutine(RegenScp049(ev.Player));
                    if (PluginClass.Config.scp049Speed != 0)
                        ev.Player.GiveEffect(PluginClass.Config.scp049Speed > 0 ? Effect.Scp207 : Effect.Disabled, (byte)Math.Abs(PluginClass.Config.scp049Speed));
                    if (PluginClass.Config.scp049MovementBoost > 0)
                        ev.Player.GiveEffect(Effect.MovementBoost, PluginClass.Config.scp049MovementBoost);
                });
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error(e);
            }
        }

        private IEnumerator<float> RegenScp049(Player p)
        {
            while (p.RoleID == (int)RoleType.Scp049)
            {
                p.Heal(Math.Min(PluginClass.Config.regenAmout + PluginClass.Config.regenKillAdditive * PluginClass.killAmount + PluginClass.Config.regenReviveAdditive * PluginClass.reviveAmount, PluginClass.Config.regenMax)); // healing repetively is bugged

                foreach (Player z in Server.Get.GetPlayers(z => z.RoleType == RoleType.Scp0492 && Vector3.Distance(p.Position, z.Position) <= (PluginClass.Config.regenInstanceRadius == 0 ? float.PositiveInfinity : PluginClass.Config.regenInstanceRadius)))
                    z.Heal(Math.Min(PluginClass.Config.regenInstanceAmout + PluginClass.Config.regenKillAdditive * PluginClass.killAmount + PluginClass.Config.regenReviveAdditive * PluginClass.reviveAmount, PluginClass.Config.regenMax));

                yield return Timing.WaitForSeconds(PluginClass.Config.regenTime);
            }
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim == null || !PluginClass.Config.scp0492Explode)
                return;

            if (ev.Victim.RoleType == RoleType.Scp0492)
                Map.Get.Explode(ev.Victim.Position, GrenadeType.Grenade, ev.Victim);
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == null || ev.DamageType != DamageType.Scp207)
                return;
            if (ev.Victim.RoleType == RoleType.Scp049 || (ev.Victim.RoleType == RoleType.Scp0492 && PluginClass.Config.scp0492IgnoreDamageScp207))
                ev.Allow = false;
        }

        private void OnRevive(Scp049ReviveEvent ev)
        {
            if (!ev.Finish || ev.Scp049 == null) return;

            ev.Scp049.Heal(Math.Min(PluginClass.Config.reviveHealAmount + PluginClass.Config.reviveHealKillAdditive * PluginClass.killAmount + PluginClass.Config.reviveHealReviveMAdditiver * PluginClass.reviveAmount, PluginClass.Config.reviveHealMax));
            PluginClass.reviveAmount++;
        }
    }
}