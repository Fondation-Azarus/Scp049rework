using Synapse.Config;
using System.ComponentModel;

namespace Scp049rework
{
    public class Configs : AbstractConfigSection
    {
        [Description("Disabled :")]
        public bool disabled = false;



        [Description("How much HP does SCP-049 start with (put 0 if you don't want anything to change) :")]
        public float intialHP = 1700;

        [Description("How much HP can SCP-049 have (put 0 if you don't want anything to change) :")]
        public float maxHP = 2000;



        [Description("How much HP does SCP-049 regenerate :")]
        public float regenAmout = 3;

        [Description("How many time should SCP-049 wait between his regeneration :")]
        public float regenTime = 1;

        [Description("How much HP does SCP-049-2 regenerate when SCP-049 regenerates while they are close enough :")]
        public float regenInstanceAmout = 1;

        [Description("How close does SCP-049-2 have to be from SCP-049 to regenerate (put 0 for an infinite radius) :")]
        public float regenInstanceRadius = 10;

        [Description("This value is added to 'regenAmout' for each person SCP-049 kills :")]
        public float regenKillAdditive = 0.2f;

        [Description("This value is added to 'regenAmout' for each person SCP-049 revives :")]
        public float regenReviveAdditive = 0.4f;

        [Description("The max value 'regenAmout' can have (it should be superior or equal to the default value) :")]
        public float regenMax = 10;



        [Description("How much HP does SCP-049 gain when killing :")]
        public float killHealAmount = 45;

        [Description("This value is added to 'killHealAmount' for each person SCP-049 kills :")]
        public float killHealKillAdditive = 5;

        [Description("This value is added to 'killHealAmount' for each person SCP-049 revives :")]
        public float killHealReviveMAdditiver = 0;

        [Description("The max value 'regenAmout' can have (it should be superior or equal to the default value) :")]
        public float killHealMax = 80;



        [Description("How much HP does SCP-049 gain when reviving :")]
        public float reviveHealAmount = 70;

        [Description("This value is added to 'reviveHealAmount' for each person SCP-049 kills :")]
        public float reviveHealKillAdditive = 2;

        [Description("This value is added to 'reviveHealAmount' for each person SCP-049 revives :")]
        public float reviveHealReviveMAdditiver = 8;

        [Description("The max value 'regenAmout' can have (it should be superior or equal to the default value) :")]
        public float reviveHealMax = 110;



        [Description("SCP-049 speed (if you put this value above 0 then SCP-049 will be able to sprint, you can also put a negative value) :")]
        public byte scp049Speed = 0;

        [Description("The intensity of SCP-049's movement speed boost (up to +255%) :")]
        public byte scp049MovementBoost = 10;



        [Description("How many kills should SCP-049 have before being able to use '.LateRevive' :")]
        public int lateReviveMinKills = 3;

        [Description("How many revives should SCP-049 have before being able to use '.LateRevive' :")]
        public int lateReviveMinRevives = 0;



        [Description("How many kills should SCP-049 have before being able to use '.UpgradeInstance' :")]
        public int upgradeInstanceMinKills = 5;

        [Description("How many revives should SCP-049 have before being able to use '.UpgradeInstance' :")]
        public int upgradeInstanceMinRevives = 0;

        [Description("Should '.UpgradeInstance' immobilize the SCP-049-2 :")]
        public bool upgradeInstanceImmobilized = true;

        [Description("Can SCP-049 upgrade his instances everywhere (or only in his containement cell) :")]
        public bool upgradeInstanceEverywhere = false;

        [Description("How much health does '.UpgradeInstance' heal the instance :")]
        public float upgradeInstanceHealAmount = 100;

        [Description("How much health does '.UpgradeInstance' add to the instance's max health : :")]
        public float upgradeInstanceMaxHealthAdd = 150;

        [Description("How long does '.UpgradeInstance' take (in seconds) : :")]
        public float upgradeInstanceDuration = 10;

        [Description("The intensity of SCP-207 effect SCP-049-2 gets when upgraded :")]
        public byte upgradeInstanceScp207Intensity = 0;

        [Description("The intensity of SCP-049-2's movement speed boost (up to +255%) :")]
        public byte upgradeInstanceMovementBoostIntensiry = 25;

        [Description("Should SCP-049-2 ignore SCP-207 damages :")]
        public bool scp0492IgnoreDamageScp207 = false;



        [Description("Should SCP-049-2 explode on death :")]
        public bool scp0492Explode = false;
    }
}