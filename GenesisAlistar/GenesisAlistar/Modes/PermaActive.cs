using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using Settings = GenesisAlistar.Config.Modes.PermaActive;


namespace GenesisAlistar.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            if (Player.Instance.IsRecalling()) return; //Why this requires overload
            if (Player.Instance.IsDead) return; //And this doesnt, is beyond me.

            if ((E.IsReady() && Settings.UseE && Player.Instance.ManaPercent > Settings.EMP && (Player.Instance.HealthPercent < Settings.EHP || EntityManager.Heroes.Allies.Any(ally => ally.HealthPercent < Settings.EHP && Player.Instance.Distance(ally) <= 550))))
            {
                E.Cast();
            }
            if (
                R.IsReady() && Settings.UseR &&
                (
                    Player.HasBuffOfType(BuffType.Fear) || Player.HasBuffOfType(BuffType.Silence) || Player.HasBuffOfType(BuffType.Snare) || 
                    Player.HasBuffOfType(BuffType.Stun) || Player.HasBuffOfType(BuffType.Charm) || Player.HasBuffOfType(BuffType.Blind) || 
                    Player.HasBuffOfType(BuffType.Taunt) 
                )
               )
            {
                R.Cast();
            }

            if (Config.Modes.Insec.InsecKey)
            {
                AIHeroClient target = TargetSelector.GetTarget(W.Range,DamageType.Magical);
                InsecManager.Insec(target);
            }
            else { InsecManager.InsecState = 0; }
        }
    }
}
