using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator {

    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    protected override void Start () {
        base.Start ();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        // Initialise weapon animations dictionary
        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]> ();
        foreach (WeaponAnimations a in weaponAnimations) {
            weaponAnimationsDict.Add (a.weapon, a.clips);
        }
    }

    protected virtual void OnEquipmentChanged (Equipment newItem, Equipment oldItem) {
        // If we are equipping a new weapon
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon) {
            animator.SetLayerWeight (1, 1);
            
            // Get new weapon animations
            if (weaponAnimationsDict.ContainsKey (newItem)) {
                currentAttackAnimSet = weaponAnimationsDict[newItem];
            }
            
            // Set transition to equipped item locomotion
            animator.SetBool("equipLocomotion", true);
        }

        // If we are unequipping a weapon
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon) {
            animator.SetLayerWeight (1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
            
            // Transition to unequipped locomotion
            animator.SetBool("equipLocomotion", false);
        }

        // If we are equipping a new shield
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield) {
            animator.SetLayerWeight (2, 1);
        }

        // If we are unequipping a shield
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield) {
            animator.SetLayerWeight (2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}