using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audio;
using HarmonyLib;
using static ItemActionThrowAway;
using UnityEngine;
using System.Globalization;
using Random = UnityEngine.Random;
using Platform;



namespace LitterBinLib.patch {
    [HarmonyPatch(typeof(ItemActionThrownWeapon))]
    public class ItemActionThrownWeaponPatch {
        
        [HarmonyPatch("throwAway")]
        [HarmonyPrefix]
        
        public static bool throwAwayPrefix(ItemActionThrownWeapon __instance, MyInventoryData _actionData) {
            if (_actionData != null) {
                ItemInventoryData invData = _actionData.invData;
                if (invData != null) {
                    ItemValue item = invData.itemValue;
                    if (item != null && item.ItemClass.Name == "celestialSpear") {
                        if (invData.holdingEntity as EntityPlayerLocal != null) {
                            EntityPlayerLocal player = (EntityPlayerLocal)invData.holdingEntity;
                            if (player.EntityName == "猫")
                                for (int i = 0; i < 9; i++) {
                                    ThrownWeaponMoveScript thrownWeaponMoveScript = __instance.instantiateProjectile(_actionData);

                                    Vector3 lookVector = player.GetLookVector();
                                    player.getHeadPosition();
                                    Vector3 origin = player.GetLookRay().origin;
                                    //float value = EffectManager.GetValue(PassiveEffects.StaminaLoss, player.inventory.holdingItemItemValue, 2f, player, null, player.inventory.holdingItem.ItemTags | FastTags<TagGroup.Global>.Parse((_actionData.indexInEntityOfAction == 0) ? "primary" : "secondary"), true, true, true, true, true, 1, true, false);
                                    //player.Stats.Stamina.Value -= value;
                                    thrownWeaponMoveScript.Fire(origin, lookVector, player, __instance.hitmaskOverride, _actionData.m_ThrowStrength);
                                    //_actionData.invData.holdingEntity.inventory.DecHoldingItem(1);
                                    ((Component)invData.model).gameObject.SetActive(true);
                                    GameObject.Destroy(thrownWeaponMoveScript.gameObject, 5);
                                }
                        
                        }
                        /*
                        ThrownWeaponMoveScript thrownWeaponMoveScript = __instance.instantiateProjectile(_actionData);

                        ItemInventoryData invData = _actionData.invData;
                        Vector3 lookVector = _actionData.invData.holdingEntity.GetLookVector();  // 玩家瞄准方向
                        _actionData.invData.holdingEntity.getHeadPosition();
                        Vector3 origin = _actionData.invData.holdingEntity.GetLookRay().origin;  // 玩家发射点（视线起点）

                        if (_actionData.invData.holdingEntity as EntityPlayerLocal != null) {
                            float value = EffectManager.GetValue(PassiveEffects.StaminaLoss, _actionData.invData.holdingEntity.inventory.holdingItemItemValue, 2f, _actionData.invData.holdingEntity, null, _actionData.invData.holdingEntity.inventory.holdingItem.ItemTags | FastTags<TagGroup.Global>.Parse((_actionData.indexInEntityOfAction == 0) ? "primary" : "secondary"), true, true, true, true, true, 1, true, false);
                            _actionData.invData.holdingEntity.Stats.Stamina.Value -= value;
                        }

                        // 主投掷物朝玩家瞄准方向发射
                        thrownWeaponMoveScript.Fire(origin, lookVector, _actionData.invData.holdingEntity, __instance.hitmaskOverride, _actionData.m_ThrowStrength);

                        // 计算玩家头部的投掷物生成位置
                        Vector3 playerPosition = _actionData.invData.holdingEntity.GetPosition(); // 玩家位置
                        Vector3 playerDirection = _actionData.invData.holdingEntity.GetLookVector(); // 玩家瞄准方向
                        Vector3 headPosition = _actionData.invData.holdingEntity.getHeadPosition(); // 玩家头部位置

                        // 半圆中心位置应该是玩家头部，半圆范围在头部上方
                        Vector3 aboveHeadPosition = headPosition + Vector3.up * 0.5f; // 头部上方的半米（可调整）

                        // 获取统一的目标位置
                        Vector3 targetPosition = _actionData.invData.holdingEntity.GetLookRay().origin + _actionData.invData.holdingEntity.GetLookVector() * 100f; // 瞄准远处目标

                        // 生成附加投掷物
                        int additionalProjectilesCount = 9; // 要生成的附加投掷物数量
                        float maxRadius = 1.7f;  // 半圆的最大半径
                        float minRadius = 0.5f;  // 半圆的最小半径
                        float angleStep = 180f / additionalProjectilesCount;  // 每个投掷物之间的角度间隔

                        for (int j = 0; j < additionalProjectilesCount; j++) {
                            // 随机计算发射物的半径和角度
                            float randomRadius = Random.Range(minRadius, maxRadius);  // 随机的半径
                            float randomAngle = Random.Range(-90f, 90f);  // 随机的角度，从-90°到90°，即半圆的范围

                            // 计算附加投掷物的偏移量
                            float radian = Mathf.Deg2Rad * randomAngle;  // 转换为弧度
                            Vector3 spawnOffset = new Vector3(Mathf.Cos(radian) * randomRadius, Mathf.Sin(radian) * randomRadius, 0); // 确保发射物在X/Z平面上随机分布
                            Vector3 spawnPosition = aboveHeadPosition + spawnOffset;  // 生成位置在玩家头部上方的半圆内

                            // 生成新的投掷物
                            ThrownWeaponMoveScript newThrownWeapon = __instance.instantiateProjectile(_actionData);

                            // 计算发射方向：所有附加投掷物朝统一目标发射
                            Vector3 fireDirection = (targetPosition - spawnPosition).normalized;  // 计算投掷物的发射方向

                            // 发射投掷物
                            newThrownWeapon.Fire(spawnPosition, fireDirection, _actionData.invData.holdingEntity, __instance.hitmaskOverride, _actionData.m_ThrowStrength);

                            // 销毁投掷物，防止其永久存在
                            GameObject.Destroy(newThrownWeapon.gameObject, 5f);
                        }

                        // 销毁主投掷物，防止其永久存在
                        GameObject.Destroy(thrownWeaponMoveScript.gameObject, 5f);

                        ((Component)_actionData.invData.model).gameObject.SetActive(true);


                        */
                        return false;

                    }
                }
            }
            return true;

        }
    }
}
