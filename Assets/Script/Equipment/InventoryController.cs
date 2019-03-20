using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public PlayerWeaponController playerWeaponController;

    public Item staff;
    public Item sword;

    private void Start()
    {
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(6, "Power", "Your Power Level"));
        playerWeaponController = GetComponent<PlayerWeaponController>();
    //    sword = new Item(swordStats, "sword");
        staff = new Item(swordStats, "staff");


      
        swordStats.Add(new BaseStat(6, "Power", "Your Power Level"));
        playerWeaponController = GetComponent<PlayerWeaponController>();
        //    sword = new Item(swordStats, "sword");
        sword= new Item(swordStats, "sword");
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.X))
        {
            playerWeaponController.EquipWeapon(sword);
            Debug.Log("equiped a weapomn");
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            playerWeaponController.EquipWeapon(staff);
            Debug.Log("equiped a weapomn");
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            playerWeaponController.PerformWeaponAttack();
            Debug.Log("equiped a weapomn");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            playerWeaponController.PerformSpecialAttack();
            Debug.Log("equiped a weapomn");
        }


    }
}
