using HarmonyLib;
using UnityEngine;

namespace Robot_Self_Repair
{
    [HarmonyPatch(typeof(PLPawnItem_RepairGun), "OnActive")]
    internal class RepairGun
    {
        static void Postfix(PLPawnItem_RepairGun __instance, PLPawn ___MySetupPawn)
        {
            if (!__instance.LocalPlayerIsOwner() || !__instance.Active || ___MySetupPawn.GetPlayer().RaceID != 2 ||
                __instance.IsRepairingSystem) return;

            if (PLInput.Instance.GetHeldDownTime(PLInputBase.EInputActionName.activate_station) > 0.2f && ___MySetupPawn.Health < ___MySetupPawn.MaxHealth)
            {
                Message.SendRepairMessage(___MySetupPawn.PlayerID, __instance.NetID, true);
            }
            else
            {
                Message.SendRepairMessage(___MySetupPawn.PlayerID, __instance.NetID, false);
            }
        }
    }
}
