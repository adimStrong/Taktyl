using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }
    public Animator animator;
    IWeapon activeWeapon;

    CharacterStats characterStats;

    private void Start()
    {
        //getting the character stat class 
        characterStats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
    }

    public void EquipWeapon(Item itemToEquip)
    {
        // check if player has already equiped weapon then remove it and with its stats
        if(EquippedWeapon != null)
        {
            characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }
        else
        {
            // find the weapon object from the resources folder and parent it to the playerhand gameobject
            EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug),
            playerHand.transform.position, playerHand.transform.rotation);
            activeWeapon = EquippedWeapon.GetComponent<IWeapon>();

            activeWeapon.Stats = itemToEquip.Stats;
            EquippedWeapon.transform.SetParent(playerHand.transform);
            characterStats.AddStatBonus(itemToEquip.Stats);
        }
    }

    public void PerformWeaponAttack()
    {
        // getting the weapon and performing its attack function for the equipment
        animator.SetTrigger("SwordAttack");
        activeWeapon.PerformAttack();
    }

    public void PerformSpecialAttack() {

        activeWeapon.PerformSpecialAttack();
    
    }



}
