using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX.XInput;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace GenesisController
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "GenesisController";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to this GenesisController!");
            Menu.AddLabel("Use your XInput Ready device!");
            Menu.AddLabel("Xbox: A = Combo B = Lane Clear X = Last Hit Y = Harass");
            Menu.AddLabel("Dualshock: X = Combo O = Lane Clear ■ = Last Hit ▲ = Harass");

        }

        public static void Initialize()
        {
        }
    }
}
