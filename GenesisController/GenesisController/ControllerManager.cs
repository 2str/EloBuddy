using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using SharpDX.XInput;

namespace GenesisController
{
    internal class ControllerManager
    {
        private static readonly Controller MyController = new Controller(UserIndex.One);
        static ControllerManager()
        {
            // Listen to events we need
            Game.OnUpdate += OnTick;
        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        private static void OnTick(EventArgs args)
        {
            
            State state = MyController.GetState();
            float x = state.Gamepad.LeftThumbX;
            if (x < 500 && x > -500) x = 0;
            float y = state.Gamepad.LeftThumbY;
            if (y < 500 && y > -500) y = 0;
            const int rangeMax = 32767;
            
            Vector2 move = new Vector2(Player.Instance.Position.X + 300 * (x / rangeMax),
                Player.Instance.Position.Y + 150 * (y / rangeMax));
            Orbwalker.DisableMovement = false;
            Orbwalker.MoveTo(move.To3DWorld());
            Orbwalker.DisableMovement = true;
            if ((state.Gamepad.Buttons & GamepadButtonFlags.A) != 0)
            {
                Orbwalker.ActiveModesFlags = Orbwalker.ActiveModes.Combo;
            }
            else
            {
                if ((state.Gamepad.Buttons & GamepadButtonFlags.B) != 0)
                {
                    Orbwalker.ActiveModesFlags = Orbwalker.ActiveModes.LaneClear;
                }
                else
                {
                    if ((state.Gamepad.Buttons & GamepadButtonFlags.X) != 0)
                    {
                        Orbwalker.ActiveModesFlags = Orbwalker.ActiveModes.LastHit;
                    }
                    else
                    {
                        if ((state.Gamepad.Buttons & GamepadButtonFlags.Y) != 0)
                        {
                            Orbwalker.ActiveModesFlags = Orbwalker.ActiveModes.Harass;
                        }
                        else
                        {
                            Orbwalker.ActiveModesFlags = Orbwalker.ActiveModes.None;
                        }
                    }
                }
            }
        }

    }
}
