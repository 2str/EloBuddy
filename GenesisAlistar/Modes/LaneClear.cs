using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using Settings = GenesisAlistar.Config.Modes.LC;

namespace GenesisAlistar.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && Settings.QMana < Player.Instance.ManaPercent)
            {
                if (EntityManager.MinionsAndMonsters.EnemyMinions.Count(minion=> minion.Distance(Player.Instance) < Q.Range) >= Settings.QNum)
                {
                    Q.Cast();
                }
            }
        }
    }
}
