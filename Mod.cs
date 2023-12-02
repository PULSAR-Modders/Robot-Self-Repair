using PulsarModLoader;

namespace Robot_Self_Repair
{
    public class Mod : PulsarMod
    {
        public static Mod Instance { get; private set; }

        public Mod()
        {
            Instance = this;
        }

        public override string Version => "0.0.0";

        public override string Author => "18107";

        public override string ShortDescription => "Allows robots to repair themselves with repair tools by holding reload";

        public override string Name => "Robot Self Repair";

        public override string ModID => "robotselfrepair";

        public override string HarmonyIdentifier()
        {
            return "id107.robotselfrepair";
        }
    }
}
