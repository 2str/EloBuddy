using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using Settings = GenesisAlistar.Config.Modes.Interrupt;

namespace GenesisAlistar
{
    class InterruptManager
    {
       
        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        internal static void OnInterruptable(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            var target = sender;
            if (target == null) return;
            if (!SpellManager.Q.IsReady() && !SpellManager.W.IsReady()) return;
            if (Settings.UseQ && SpellManager.Q.IsInRange(target) && SpellManager.Q.IsReady()) { SpellManager.Q.Cast(); return; }
            if (Settings.UseW && SpellManager.W.IsInRange(target) && SpellManager.W.IsReady()) { SpellManager.W.Cast(target); return; }
            return;
        }
    }
}
