using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Consumable
{
    // Start is called before the first frame update
    public override void Collect()
    {
        Debug.Log("Collected");
        if (PlayerMgr.inst.currentGunID == 0)
        {
            GameMgr.inst.pistolToRemove.SetActive(false);
            PlayerMgr.inst.gun = GameMgr.inst.gunList[PlayerMgr.inst.currentGunID + 1];
            PlayerMgr.inst.currentGunID += 1;
            GameMgr.inst.sniperToRemove.SetActive(true);
        }
        if (PlayerMgr.inst.currentGunID == 1)
        {
            GameMgr.inst.sniperToRemove.SetActive(false);
            PlayerMgr.inst.gun = GameMgr.inst.gunList[PlayerMgr.inst.currentGunID - 1];
            PlayerMgr.inst.currentGunID -= 1;
            GameMgr.inst.pistolToRemove.SetActive(true);
        }

        base.Collect();
    }

    public void testFunc()
    {
        Debug.Log(GameMgr.inst.sniperToRemove.activeSelf);
        Debug.Log(GameMgr.inst.sniperToRemove.activeInHierarchy);
    }
}
