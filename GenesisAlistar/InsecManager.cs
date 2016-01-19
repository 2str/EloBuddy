using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using SharpDX;

using Settings = GenesisAlistar.Config.Modes.Insec;

namespace GenesisAlistar
{
    class InsecManager
    {
        public static Vector3 _moveTo;
        public static int InsecState = 0;
        public static int c = 0;
        static InsecManager()
        {}

        static Vector3 getPos(AIHeroClient target, int c)
        {
            AIHeroClient ally1 = EntityManager.Heroes.Allies.FirstOrDefault(ally => ally.HealthPercent > Settings.InsecToHP && Player.Instance.Distance(ally) <= Settings.InsecToRange && ally.Name != Player.Instance.Name);
            Vector3 moveTo = new Vector3();
            Vector3 allyPos = new Vector3();
            Vector3 point = new Vector3();
            if (ally1 != null)
                point = ally1.Position;
            else
                point = Vector3.Zero;
            allyPos = point;
            if (allyPos == Vector3.Zero) return Vector3.Zero;
            var targetPos = target.Position;
            //From the Ally position we extended (the distance between both + 100) units in the target direction THANKS @WUJU!
            if(c == 3) { moveTo = allyPos.Extend(target, ally1.Distance(target) - Settings.Dist).To3DWorld(); }
            if(c == 1) { moveTo = allyPos.Extend(target, ally1.Distance(target) + Settings.Dist).To3DWorld(); }

            if (NavMesh.GetCollisionFlags(moveTo) == CollisionFlags.Building || NavMesh.GetCollisionFlags(moveTo) == CollisionFlags.Wall)
            {
                return Vector3.Zero;
            }
            _moveTo = moveTo;
            return moveTo;
        }

        public static int getEnabled(AIHeroClient target)
        {
            var c = 0;
            if(target.Name == Settings.champ1name)
            {
                c = Settings.champ1;
            }
            if (target.Name == Settings.champ2name)
            {
                c = Settings.champ2;
            }
            if (target.Name == Settings.champ3name)
            {
                c = Settings.champ3;
            }
            if (target.Name == Settings.champ4name)
            {
                c = Settings.champ4;
            }
            if (target.Name == Settings.champ5name)
            {
                c = Settings.champ5;
            }
            if (target.Name == Settings.champ6name)
            {
                c = Settings.champ6;
            }
            return c;
        }

        public static void Insec(AIHeroClient target)
        {
            if (target == null) return;
            
            Vector3 moveTo = new Vector3();
            if (getEnabled(target) == 2 || getEnabled(target) == 0) { return; }
            c = getEnabled(target);
            switch (InsecState)
            {
                case 0:
                    moveTo = getPos(target, c);
                    if (moveTo == Vector3.Zero) return;
                    if (EntityManager.Turrets.Enemies.Any(tower => tower.Distance(moveTo) < tower.AttackRange) || !SpellManager.W.IsReady() || !SpellManager.Q.IsReady()) return;
                    if (Player.Instance.Distance(moveTo) < 425 && Player.Instance.Distance(moveTo) > 100 && Settings.UseF && SpellManager.F != null)
                    {
                        //Flash there! Thanks WujuSan!
                        SpellManager.F.Cast(moveTo);
                    }
                    Player.IssueOrder(GameObjectOrder.MoveTo, moveTo, false);
                    Orbwalker.DisableMovement = true;
                    InsecState = 1;
                break;

                case 1:
                    if (Player.Instance.Distance(_moveTo) < 50)
                    {
                        InsecState = 2;
                    }
                    else
                    {
                        if (SpellManager.Q.IsReady())
                        {
                            moveTo = getPos(target, c);
                            if (moveTo == Vector3.Zero)
                            {
                                InsecState = 0;
                                Orbwalker.DisableMovement = false;
                                return;
                            }
                            if (Player.Instance.Distance(moveTo) < 425 && Player.Instance.Distance(moveTo) > 100 && Settings.UseF && SpellManager.F != null)
                            {
                                //Flash there! Thanks WujuSan!
                                SpellManager.F.Cast(moveTo);
                            }
                            Player.IssueOrder(GameObjectOrder.MoveTo, moveTo, false);
                        }
                        else
                        {
                            InsecState = 0;
                            Orbwalker.DisableMovement = false;
                        }
                        
                    }
                break;

                case 2:
                    //Perform Insec
                    var move = getPos(target, c);
                    if (move == Vector3.Zero) return;
                    Core.DelayAction(delegate
                    {
                        SpellManager.Q.Cast();
                    }, 100);
                    Player.IssueOrder(GameObjectOrder.MoveTo, move, false);
                    Core.DelayAction(delegate
                    {
                        SpellManager.W.Cast(target);
                        Orbwalker.DisableMovement = false;
                    }, 500);
                    Core.DelayAction(delegate
                    {
                        InsecState = 0;
                    }, 1000);
                break;
                default:
                    InsecState = 0;
                break;
            }    
        }



        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
