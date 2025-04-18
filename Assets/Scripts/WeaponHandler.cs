using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public static WeaponHandler instance { get; set; }
    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        if (instance != this)
        {
            Destroy(this);
        }
        instance = this;

        activeWeaponSlot = weaponSlots[0];
    }

    void Update()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot )
            {
                weaponSlot.SetActive(true);
            }
            else 
            {
                weaponSlot.SetActive(false);
            }
        }
    }

    public void PickUpWeapon(GameObject pickedUpWeapon)
    {
        AddWeaponIntoActiveSlot(pickedUpWeapon);
    }
    
    public void DropWeapon(GameObject pickedUpWeapon)
    {
        DropCurrentWeapon(pickedUpWeapon);
    }

    private void AddWeaponIntoActiveSlot(GameObject pickedUpWeapon)
    {
        pickedUpWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        Weapon weapon = pickedUpWeapon.GetComponent<Weapon>();
        pickedUpWeapon.transform.localPosition = Vector3.zero;
        pickedUpWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        pickedUpWeapon.transform.localScale = Vector3.one;
        weapon.isActiveWeapon = true;
        if (pickedUpWeapon.GetComponent<Rigidbody>())
        {
            pickedUpWeapon.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void DropCurrentWeapon(GameObject pickedUpweapon)
    {
        var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;
        if (weaponToDrop.GetComponent<Rigidbody>())
        {
            weaponToDrop.GetComponent<Rigidbody>().isKinematic = false;
        }
        if (activeWeaponSlot.transform.childCount >= 0)
        {
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;
            weaponToDrop.transform.SetParent(null);

        }
    }
}
