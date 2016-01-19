using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using Settings = GenesisAlistar.Config.Modes.PermaActive;

namespace GenesisAlistar.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on flee mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            if (W.IsReady() && Settings.UseW)
            {
                var runFrom = EntityManager.Heroes.Enemies.Where(
                        hero => hero.Distance(Player.Instance) < 500).OrderBy(x => x.Distance(Player.Instance)).FirstOrDefault();
                if (runFrom == null) return;
                var targetMinion =
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                        minion => minion.Distance(Player.Instance) < W.Range && minion.Distance(Game.CursorPos) < Settings.WTarg)
                        .OrderBy(x => x.Distance(Game.CursorPos)).FirstOrDefault();
                var targetHero =
                    EntityManager.Heroes.Enemies.Where(
                        hero => hero.Distance(Player.Instance) < W.Range && hero.Distance(Game.CursorPos) < Settings.WTarg)
                        .OrderBy(x => x.Distance(Game.CursorPos)).FirstOrDefault();
                if (targetMinion != null) {
                    
                    W.Cast(targetMinion);
                }
                else
                {
                    if (targetHero != null)
                    {
                        W.Cast(targetHero);
                    }
                }

            }
        }
    }
}

