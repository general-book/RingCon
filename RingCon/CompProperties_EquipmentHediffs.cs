using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.Sound;

namespace RingCon
{
    public class CompProperties_EquipmentHediffs : CompProperties
    {
        public List<string> hediffDefnames;
        public CompProperties_EquipmentHediffs()
        {
            this.compClass = typeof(CompEquipmentHediffs);
        }
    }

    public class CompEquipmentHediffs : ThingComp
    {
        public List<Hediff> wielderHediffs = new List<Hediff>();
        public CompProperties_EquipmentHediffs Props
        {
            get
            {
                return (CompProperties_EquipmentHediffs)this.props;
            }
        }

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);
            var hediffStrings = this.Props.hediffDefnames;
            foreach (var hediffDefName in hediffStrings)
            {
                var hediff = HediffMaker.MakeHediff(HediffDef.Named(hediffDefName), pawn, null);
                pawn.health.AddHediff(hediff);
            }
            Log.Message("Notify_Equippedメソッド呼び出し");
        }
        public override void Notify_Unequipped(Pawn pawn)
        {
            base.Notify_Unequipped(pawn);
            foreach (var hediff in wielderHediffs)
            {
                pawn.health.hediffSet.hediffs.Remove(hediff);
            }
            Log.Message("Notify_Unequippedメソッド呼び出し");
        }

    }
}
