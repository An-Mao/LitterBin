using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace LitterBin.patch {
    [HarmonyPatch(typeof(EntityAlive))]
    class EntityAlivePatch {
        
        [HarmonyPatch("DamageEntity")]
        [HarmonyPrefix]
        /*
        [HarmonyPatch(new Type[]{
                                typeof(DamageSource),
                                typeof(int),
                                typeof(bool),
                                typeof(Vector3),
                                typeof(float)
                                })]
        */
        
        public static bool DamageEntityPrefix(EntityAlive __instance, DamageSource _damageSource, ref int _strength, bool _criticalHit, float _impulseScale) {
            if (_damageSource != null  && __instance != null) {
                ItemClass item = _damageSource.ItemClass;
                if (item != null && item.Name == "celestialSpear") _strength += (int)(__instance.GetMaxHealth()*0.1);
            }
            return true;
        }
    }
}
