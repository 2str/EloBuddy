using System;
using EloBuddy;
using EloBuddy.SDK;

// Using the config like this makes your life easier, trust me
using Settings = GenesisAlistar.Config.Modes.Combo;

namespace GenesisAlistar.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (InsecManager.InsecState > 0) InsecManager.InsecState = 0;
            Orbwalker.DisableMovement = false; //ALISTAR goes where he pleases, fuck mundo. (Keeps other pluGens from preventing movement during combo and ensures an instant switch from insec to combo)
            var target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            if (target == null) return;
            if (Settings.UseQW && W.IsReady() && Q.IsReady() && Player.Instance.Distance(target) > 300 && Player.Instance.Mana > (Player.Instance.Spellbook.GetSpell(SpellSlot.W).SData.Mana + Player.Instance.Spellbook.GetSpell(SpellSlot.Q).SData.Mana))
            { 
                    Core.DelayAction(delegate
                    {
                        Q.Cast();
                    }, (int)Math.Floor(725 - target.Distance(Player.Instance)));
                    W.Cast(target);
            }
            if (Settings.UseQ && Q.IsReady())
            {
                if (target != null)
                {
                    Q.Cast();
                }
            }
            if (Settings.UseW && W.IsReady())
            {
                if (target != null)
                {
                    W.Cast(target);
                }
            }
        }
    }
}
