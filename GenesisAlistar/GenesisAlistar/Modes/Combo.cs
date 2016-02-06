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
            
            AIHeroClient target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            if (target == null) return;
            if (Settings.UseQW && W.IsReady() && Q.IsReady() && Player.Instance.Distance(target) > 300 && Player.Instance.Mana > (Player.Instance.Spellbook.GetSpell(SpellSlot.W).SData.Mana + Player.Instance.Spellbook.GetSpell(SpellSlot.Q).SData.Mana))
            { 
                    Core.DelayAction(delegate
                    {
                        Q.Cast();
                    }, (int)Math.Floor(750 - target.Distance(Player.Instance) + Settings.Delay));
                    W.Cast(target);
            }
            target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (Settings.UseQ && Q.IsReady())
            {
                if (target != null)
                {
                    Q.Cast();
                }
            }
            target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
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
