using HarmonyLib;
using PulsarModLoader;
using UnityEngine;

namespace Robot_Self_Repair
{
    internal class Message : ModMessage
    {
        private static readonly int version = 2;
        public override void HandleRPC(object[] arguments, PhotonMessageInfo sender)
        {
            if (arguments.Length < 3 || ((int)arguments[0]) != version) return;

            PLPawn pawn = PLServer.Instance.GetPlayerFromPlayerID((int)arguments[1])?.GetPawn();
            if (pawn == null) return;
            PLPawnItem_RepairGun repairGun = pawn.GetPlayer().MyInventory.GetItemAtNetID((int)arguments[2]) as PLPawnItem_RepairGun;
            if (repairGun == null) return;
            PLPawnItemInstance myGunInstance = AccessTools.Field(typeof(PLPawnItem_RepairGun), "MyGunInstance").GetValue(repairGun) as PLPawnItemInstance;
            if (myGunInstance == null) return;
            myGunInstance.transform.rotation = Quaternion.Euler(-pawn.VerticalMouseLook.RotationY, pawn.HorizontalMouseLook.RotationX, 0f);

            if ((bool)arguments[3])
            {
                pawn.Health += Time.deltaTime * (1 + 0.5f * repairGun.Level); //Time.deltaTime * repairGun.GetHealAmount()
                pawn.Health = Mathf.Clamp(pawn.Health, 0f, pawn.MaxHealth);
                pawn.LastRepairActiveTime = Time.time;
                myGunInstance.transform.Rotate(0, 180, 0);
            }
        }

        public static void SendRepairMessage(int playerID, int repairGunID, bool repairing)
        {
            ModMessage.SendRPC(Mod.Instance.HarmonyIdentifier(), "Robot_Self_Repair.Message", PhotonTargets.All, new object[] {
                version,
                playerID,
                repairGunID,
                repairing
            });
        }
    }
}
