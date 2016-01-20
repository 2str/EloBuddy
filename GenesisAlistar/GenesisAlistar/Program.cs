using System;
using System.Collections.Generic;
//using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;


namespace GenesisAlistar
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Alistar";

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Verify the champion we made this addon for
            if (Player.Instance.ChampionName != ChampName)
            {
                // Champion is not the one we made this addon for,
                // therefore we return
                return;
            }

            // Initialize the classes that we need
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            InsecManager.Initialize();
            InterruptManager.Initialize();
            // Listen to events we need
            Interrupter.OnInterruptableSpell += InterruptManager.OnInterruptable;
            Drawing.OnDraw += OnDraw;
            Obj_AI_Base.OnBasicAttack += AnnoyManager.OnAnnoyable;
        }

        private static void OnDraw(EventArgs args)
        {
            if (InsecManager.InsecState != 0) { Circle.Draw(Color.White, 50, 4f, InsecManager._moveTo); Circle.Draw(Color.White, Config.Modes.Insec.InsecToRange, 2f, Player.Instance.Position); }
            //Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(80, -30), System.Drawing.Color.AliceBlue, InsecManager.InsecState.ToString() + InsecManager.c.ToString(), 5);
        }

    }

}
