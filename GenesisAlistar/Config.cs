using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace GenesisAlistar
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "GenesisAlistar";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to GenesisAlistar! Home of GenSec!");
            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu ComboMenu;
            private static readonly Menu HarassMenu;
            private static readonly Menu MiscMenu;
            private static readonly Menu InsecMenu;
            private static readonly Menu InterruptMenu;
            private static readonly Menu LCMenu;

            static Modes()
            {
                // Initialize the menu
                ComboMenu = Menu.AddSubMenu("Combo");
                Combo.Initialize();
                HarassMenu = Menu.AddSubMenu("Harass");
                Harass.Initialize();
                LCMenu = Menu.AddSubMenu("LC");
                LC.Initialize();
                MiscMenu = Menu.AddSubMenu("Misc");
                PermaActive.Initialize();
                InsecMenu = Menu.AddSubMenu("GenSec™");
                Insec.Initialize();
                InterruptMenu = Menu.AddSubMenu("Interrupt");
                Interrupt.Initialize();
                // Initialize all modes
                // Combo

                Menu.AddSeparator();

                // Harass
                
            }

            public static void Initialize()
            {
            }

            public static class PermaActive
            {
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useEA;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _useW;

                public static int EHP
                {
                    get { return MiscMenu["EHP"].Cast<Slider>().CurrentValue; }
                }
                public static int EMP
                {
                    get { return MiscMenu["EMP"].Cast<Slider>().CurrentValue; }
                }
                public static int WTarg
                {
                    get { return MiscMenu["WTarg"].Cast<Slider>().CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseEA
                {
                    get { return _useEA.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static PermaActive()
                {
                    MiscMenu.AddLabel("AlwaysOn");
                    _useE = MiscMenu.Add("AlwaysUseE", new CheckBox("Use E"));
                    _useEA = MiscMenu.Add("AlwaysUseEA", new CheckBox("Use E to deny farm (EXPERIMENTAL)"));
                    _useR = MiscMenu.Add("AlwaysUseR", new CheckBox("Use R on CC"));
                    MiscMenu.Add("EHP", new Slider("Heal when allies reach HP% ({0}%)", 80));
                    MiscMenu.Add("EMP", new Slider("Heal until you reach Mana% ({0}%)", 40));
                    _useW = MiscMenu.Add("AlwaysUseWFlee", new CheckBox("Use W to help flee"));
                    MiscMenu.Add("WTarg", new Slider("Look this many units from the cursor for a flee target ({0})", 300,100,700));


                }

                public static void Initialize() { }
                
                
            }

            public static class Insec
            {

                private static readonly CheckBox _useF;
                public static string champ1name;
                public static string champ2name;
                public static string champ3name;
                public static string champ4name;
                public static string champ5name;
                public static string champ6name;

                public static int champ1
                {
                    get { return InsecMenu["champ1"].Cast<Slider>().CurrentValue; }
                }
                public static int champ2
                {
                    get { return InsecMenu["champ2"].Cast<Slider>().CurrentValue; }
                }
                public static int champ3
                {
                    get { return InsecMenu["champ3"].Cast<Slider>().CurrentValue; }
                }
                public static int champ4
                {
                    get { return InsecMenu["champ4"].Cast<Slider>().CurrentValue; }
                }
                public static int champ5
                {
                    get { return InsecMenu["champ5"].Cast<Slider>().CurrentValue; }
                }
                public static int champ6 // In case hexakill comes back.
                {
                    get { return InsecMenu["champ6"].Cast<Slider>().CurrentValue; }
                }
                public static bool InsecKey
                {
                    get { return InsecMenu["Insec"].Cast<KeyBind>().CurrentValue; }
                }
                public static bool UseF
                {
                    get { return _useF.CurrentValue; }
                }
                public static int InsecToHP
                {
                    get { return InsecMenu["InsecToHP"].Cast<Slider>().CurrentValue; }
                }
                public static int InsecToRange
                {
                    get { return InsecMenu["InsecToRange"].Cast<Slider>().CurrentValue; }
                }
                public static int Dist
                {
                    get { return InsecMenu["Dist"].Cast<Slider>().CurrentValue; }
                }

                static Insec()
                {
                    
                    var i = 0;
                    
                    InsecMenu.Add("Insec", new KeyBind("Insec", false, KeyBind.BindTypes.HoldActive, 'X'));
                    _useF = InsecMenu.Add("AlwaysUseF", new CheckBox("Use Flash"));
                    InsecMenu.Add("InsecToHP", new Slider("% of HP ally needs to insec:", 50, 0, 100));
                    InsecMenu.Add("InsecToRange", new Slider("Range to start insec walk: (500 - 1500)", 700, 500, 1500));
                    InsecMenu.AddLabel("Smaller = Less Obvious Insec // Larger = Faster Insec");
                    InsecMenu.Add("Dist", new Slider("Range to insec for player: (100 - 300)", 100, 100, 300));
                    InsecMenu.AddSeparator();
                    InsecMenu.AddLabel("1 = Towards Team // 2 = Ignore // 3 = Away From Team");
                    
                    foreach (AIHeroClient Eplayer in EntityManager.Heroes.Enemies)
                    {
                        switch (i) {
                            case 0:
                                InsecMenu.Add("champ1", new Slider(Eplayer.Name.ToString() + " ("+Eplayer.ChampionName+")", 2, 1, 3));
                                champ1name = Eplayer.Name;
                                InsecMenu["champ1"].Cast<Slider>().CurrentValue = getPriority(Eplayer);
                            break;
                            case 1:
                                InsecMenu.Add("champ2", new Slider(Eplayer.Name.ToString() + " (" + Eplayer.ChampionName + ")", 2, 1, 3));
                                champ2name = Eplayer.Name;
                                InsecMenu["champ2"].Cast<Slider>().CurrentValue = getPriority(Eplayer);
                                break;
                            case 2:
                                InsecMenu.Add("champ3", new Slider(Eplayer.Name.ToString() + " (" + Eplayer.ChampionName + ")", 2, 1, 3));
                                champ3name = Eplayer.Name;
                                InsecMenu["champ3"].Cast<Slider>().CurrentValue = getPriority(Eplayer);
                                break;
                            case 3:
                                InsecMenu.Add("champ4", new Slider(Eplayer.Name.ToString() + " (" + Eplayer.ChampionName + ")", 2, 1, 3));
                                champ4name = Eplayer.Name;
                                InsecMenu["champ4"].Cast<Slider>().CurrentValue = getPriority(Eplayer);
                                break;
                            case 4:
                                InsecMenu.Add("champ5", new Slider(Eplayer.Name.ToString() + " (" + Eplayer.ChampionName + ")", 2, 1, 3));
                                champ5name = Eplayer.Name;
                                InsecMenu["champ5"].Cast<Slider>().CurrentValue = getPriority(Eplayer);
                                break;
                            case 5:
                                InsecMenu.Add("champ6", new Slider(Eplayer.Name.ToString() + " (" + Eplayer.ChampionName + ")", 2, 1, 3));
                                champ6name = Eplayer.Name;
                                InsecMenu["champ6"].Cast<Slider>().CurrentValue = getPriority(Eplayer);
                                break;
                        }
                        i++;
                    }
                    InsecMenu.AddLabel("Note: GenSec™ uses forced movement.");

                }

                public static void Initialize() { }


            }

            public static int getPriority(AIHeroClient Eplayer)
            {
                switch (TargetSelector.GetPriority(Eplayer))
                {
                    case 1:
                    case 2:
                        return 3;
                    case 5:
                    case 4:
                        return 1;
                    default:
                        return 2;
                }
                
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useQW;
                private static readonly CheckBox _useW;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseQW
                {
                    get { return _useQW.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                
                static Combo()
                {
                    // Initialize the menu values
                    ComboMenu.AddGroupLabel("Combo");
                    _useQ = ComboMenu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = ComboMenu.Add("comboUseW", new CheckBox("Use W"));
                    _useQW = ComboMenu.Add("comboUseQW", new CheckBox("Use Q->W"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Interrupt
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                static Interrupt()
                {
                    // Initialize the menu values
                    InterruptMenu.AddGroupLabel("Interrupt");
                    _useQ = InterruptMenu.Add("InterruptUseQ", new CheckBox("Use Q"));
                    _useW = InterruptMenu.Add("InterruptUseW", new CheckBox("Use W"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                public static bool UseQ
                {
                    get { return HarassMenu["harassUseQ"].Cast<CheckBox>().CurrentValue; }
                }



                static Harass()
                {
                    HarassMenu.AddGroupLabel("Harass");
                    HarassMenu.Add("harassUseQ", new CheckBox("Use Q"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LC
            {
                private static readonly CheckBox _useQ;


                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static int QNum
                {
                    get { return LCMenu["QNum"].Cast<Slider>().CurrentValue; }
                }
                public static int QMana
                {
                    get { return LCMenu["QMana"].Cast<Slider>().CurrentValue; }
                }

                static LC()
                {
                    // Initialize the menu values
                    LCMenu.AddGroupLabel("LaneClear");
                    _useQ = LCMenu.Add("UseQ", new CheckBox("Use Q"));
                    LCMenu.Add("QMana", new Slider("Q until you reach Mana% ({0}%)", 40));
                    LCMenu.Add("QNum", new Slider("Q if there are X minions", 3,1,7));
                }

                public static void Initialize()
                {
                }
            }

        }
    }
}
