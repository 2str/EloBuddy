using EloBuddy;
using EloBuddy.SDK;
using Settings = GenesisAlistar.Config.Modes.PermaActive;

namespace GenesisAlistar
{
    internal class AnnoyManager
    {

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        internal static void OnAnnoyable(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Player.Instance.Distance(args.Target) > 500 || args.Target.IsEnemy) return;
            Obj_AI_Base annoy = sender;
            if (sender.Type != GameObjectType.AIHeroClient) return;
            Obj_AI_Minion minion = args.Target as Obj_AI_Minion;
            if (annoy == null) return;            
            if (minion == null) return;
            if(minion.Health < annoy.GetAutoAttackDamage(minion) && (SpellManager.E.Level * 30) + 30 + (0.2 * annoy.FlatMagicDamageMod) + minion.Health > annoy.GetAutoAttackDamage(minion) && Settings.UseEA && SpellManager.E.IsReady()) //TODO: Add Config!
            {
                
                SpellManager.E.Cast();
            }
            
            return;
        }
    }
}
