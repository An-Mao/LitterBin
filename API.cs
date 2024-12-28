using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using LitterBinLib.patch;

namespace LitterBin
{
    public class API : IModApi
    {
        private static Harmony harmony;
        public void InitMod(Mod _modInstance)
        {

            harmony = new Harmony("nws.dev.7d2d.litterbin");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            /*
            var original = AccessTools.Method(typeof(ItemActionThrownWeapon), "ExecuteAction");
            var preFix = AccessTools.Method(typeof(ItemActionThrownWeaponPatch), "throwAwayPrefix");
            harmony.Patch(original, new HarmonyMethod(preFix));
            harmony.PatchAll();
            */

            Log.Out("Mod dll load");
        }
    }
}
