using Synapse.Translation;

namespace Scp049rework
{
    public class PluginTranslation : IPluginTranslation
    {
        public string broadcast = "SCP-049 has new abilities thanks to Scp049rework (by Bonjemus, Azarus), you can regenerate yourself and your instances, be healed when killing/reviving, revive corpse you wouldn't normally be able to with '.LateRevive' ou '.reussiter, and more...";
        public string notScp049Error = "You're not SCP-049.";
        public string notNearACorpseError = "You must near a corpse and, if it's already the case then this player disconnected.";
        public string scp0492Error = "You can't revive a SCP-049-2.";
        public string freshCorpseError = "You are still able to revive this corpse without using this command.";
        public string notEnoughKillsError = "You must kill at least %lateReviveMinKills% before using this command.";
        public string notEnoughRevivesError = "You must revive at least %lateReviveMinRevives% before using this command.";
        public string currentlyAliveError = "This player is currently alive, you can't revive his corpse.";
        public string revivedPlayer = "You revived this player successfully.";
        public string notLookingToAScp0492Error = "You must be looking at a SCP-049-2 while being close enough.";
        public string wrongRoomError = "You must be in your room to use this command.";
        public string startUpgradedScp0492 = "You're starting to upgraded this SCP-049-2.";
        public string alreadyUpradingError = "You're already upgrading a SCP-049-2.";
        public string upgradedScp0492 = "You upgraded this SCP-049-2 successfully.";
    }
}