using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;

namespace GenesisAlistar
{
    public static class SpellManager
    {
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Active Q { get; private set; }
        public static Spell.Targeted W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Active R { get; private set; }
        public static Spell.Skillshot F { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Active(SpellSlot.Q, 315);
            W = new Spell.Targeted(SpellSlot.W, 625);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R);
            SpellDataInst flash = Player.Instance.Spellbook.Spells.Where(spell => spell.Name.Contains("flash")).Any() ? Player.Instance.Spellbook.Spells.Where(spell => spell.Name.Contains("flash")).First() : null;
            if (flash != null)
            {
                F = new Spell.Skillshot(flash.Slot, 425, SkillShotType.Linear);
            }
        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
