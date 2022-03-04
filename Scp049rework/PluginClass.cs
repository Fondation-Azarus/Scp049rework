using Synapse.Api.Plugin;
using Synapse.Translation;


namespace Scp049rework
{
    [PluginInformation(
        Name = "Scp049rework",
        Author = "Bonjemus, Azarus owner",
        Description = "Plug-in that adds a lot of things for SCP-049.",
        LoadPriority = 1,
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "1.0.0"
        )]
    public class PluginClass : AbstractPlugin
    {
        [Config(section = "Scp049rework")]
        public static Configs Config;

        [SynapseTranslation]
        public static new SynapseTranslation<PluginTranslation> Translation;

        public static int killAmount = 0;
        public static int reviveAmount = 0;
        public static bool alreadyUpgrading = false;
        public override void Load()
        {
            if (Config.disabled)
                return;

            new Handler();

            Translation.AddTranslation(new PluginTranslation());
            Translation.AddTranslation(new PluginTranslation
            {
                broadcast = "SCP-049 a de nouvelles capacités grâce à Scp049rework (de Bonjemus, d'Azarus), vous pouvez vous régénérez vous et vos instances, être soigné en tuant/ressusitant, ressusiter des corps que vous ne pourriez normalement pas avec la commande '.ReanimationTardive' ou '.LateRevive', etc...",
                notScp049Error = "Vous n'êtes pas SCP-049.",
                scp0492Error = "Vous ne pouvez pas resussiter un SCP-049-2",
                notNearACorpseError = "Vous devez être proche d'un cadavre un cadavre, si c'st déjà le cas alors ce joueur s'est déconnecté.",
                freshCorpseError = "Vous pouvez déjà réanimé ce corps sans utiliser cette commande.",
                notEnoughKillsError = "Vous devez tuer au moins %lateReviveMinKills% personnes avant d'utiliser cette commande.",
                notEnoughRevivesError = "Vous devez ressusiter au moins %lateReviveMinRevives% personnes avant d'utiliser cette commande.",
                currentlyAliveError = "Ce joueur est actuellement en vie, vous ne pouvez pas le ressusiter.",
                revivedPlayer = "Vous avez réssusité ce joueur avec succès.",
                notLookingToAScp0492Error = "Vous devez regarder un SCP-049-2 en étant proche de lui.",
                wrongRoomError = "Vous devez être dans votre salle pour utiliser cette commande.",
                startUpgradedScp0492 = "Vous commencez à améliorer ce SCP-049-2.",
                alreadyUpradingError = "Vous êtes déjà en train d'améliorer un SCP-049-2.",
                upgradedScp0492 = "Vous avez amélioré ce SCP-049-2 avec succès."
            }, "FRENCH");
        }
    }
}
