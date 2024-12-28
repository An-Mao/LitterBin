﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace LitterBin.patch {

    [HarmonyPatch(typeof(GameManager))]
    internal class GameManagerPatch {
        [HarmonyPrefix]
        [HarmonyPatch("ItemDropServer")]
        [HarmonyPatch(new Type[]{
                                typeof(ItemStack),
                                typeof(Vector3),
                                typeof(Vector3),
                                typeof(Vector3),
                                typeof(int),
                                typeof(float),
                                typeof(bool),
                                typeof(int)
                                })]
        public static bool ItemDropServerPrefix(ItemStack _itemStack, Vector3 _dropPos, Vector3 _randomPosAdd, Vector3 _initialMotion, int _entityId, float _lifetime, bool _bDropPosIsRelativeToHead, int _clientEntityId) {
            // 如果是 celestialSpear 投掷物，阻止掉落物生成
            if (_itemStack.itemValue.ItemClass.Name == "celestialSpear") {
                return false; // 阻止掉落物的生成
            }
            return true; // 继续执行原始的掉落物生成逻辑
        }
    }
}
