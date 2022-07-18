using Save;

namespace Shop
{
    public class Turret : Purchasable, ISaver
    {
        private Turret(string name, int price, float baseFlamethrowerRange, float baseFlamethrowerDamage,
            float baseFlamethrowerCooldown, float baseCanonDamages) : base(name, price)
        {
            this.baseFlamethrowerRange = baseFlamethrowerRange;
            this.baseFlamethrowerDamage = baseFlamethrowerDamage;
            this.baseFlamethrowerCooldown = baseFlamethrowerCooldown;
            this.baseCanonDamages = baseCanonDamages;
        }

        public readonly float baseFlamethrowerRange;
        public readonly float baseFlamethrowerDamage;
        public readonly float baseFlamethrowerCooldown;
        public readonly float baseCanonDamages;

        public Upgrade flamethrowerRangeUpgrade = new("Flamethrower Range", "", 0, 40, 10);
        public Upgrade flamethrowerDamageUpgrade = new("Flamethrower Damage", "", 0, 40, 10);
        public Upgrade flamethrowerCooldownUpgrade = new("Flamethrower Cooldown", "", 0, 40, 10);
        public Upgrade mainCanonDamagesUpgrade = new("Main Canon Damages", "", 0, 40, 10);

        public Upgrade[] Upgrades => new[]
        {
            flamethrowerRangeUpgrade,
            flamethrowerDamageUpgrade,
            flamethrowerCooldownUpgrade,
            mainCanonDamagesUpgrade
        };

        public static readonly Turret FirstTurret =
            new("First Turret", 100, 5, 10, 10, 10);

        public static readonly Turret[] Turrets = { FirstTurret };

        public static Turret CurrentTurret { get; set; }

        private static string PathFlamethrowerRangeUpgrade(string path) => $"{path}/FlamethrowerRangeUpgrade";
        private static string PathFlamethrowerCooldownUpgrade(string path) => $"{path}/flamethrowerCooldown";
        private static string PathFlamethrowerDamageUpgrade(string path) => $"{path}/flamethrowerDamage";
        private static string PathMainCanonDamagesUpgrade(string path) => $"{path}/mainCanonDamages";

        public override void Save(string path)
        {
            base.Save(path);
            flamethrowerRangeUpgrade.Save(PathFlamethrowerRangeUpgrade(path));
            flamethrowerCooldownUpgrade.Save(PathFlamethrowerCooldownUpgrade(path));
            flamethrowerDamageUpgrade.Save(PathFlamethrowerDamageUpgrade(path));
            mainCanonDamagesUpgrade.Save(PathMainCanonDamagesUpgrade(path));
        }


        public override bool Load(string path) =>
            base.Load(path) &&
            flamethrowerRangeUpgrade.Load(PathFlamethrowerRangeUpgrade(path)) &&
            flamethrowerCooldownUpgrade.Load(PathFlamethrowerCooldownUpgrade(path)) &&
            flamethrowerDamageUpgrade.Load(PathFlamethrowerDamageUpgrade(path)) &&
            mainCanonDamagesUpgrade.Load(PathMainCanonDamagesUpgrade(path));
    }
}