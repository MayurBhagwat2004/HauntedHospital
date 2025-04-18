using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    public bool fuelTankAcquired;
    [SerializeField]
    private GameObject weaponSlot;
    public static FuelTank instance;
    public bool fuelTankPoured;
    public Animator animator;
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }



    public void AddFuelTankToSlot(GameObject fuelTank)
    {
        if (weaponSlot.transform.childCount==0)
        {
            if (!fuelTankAcquired)
            {
                fuelTankAcquired = true;
                fuelTank.transform.SetParent(weaponSlot.transform, true);
                GameObject fuelTankGameObject = fuelTank.gameObject;
                weaponSlot.transform.localRotation = Quaternion.Euler(5.917f, -174.583f, 24.314f);
                fuelTank.transform.localRotation = Quaternion.Euler(-90f, -90f, 90f);
                fuelTank.transform.localPosition = new Vector3(-0.45f, -1.44f, 7.42f);
                fuelTank.transform.localScale = new Vector3(fuelTank.transform.localScale.x, fuelTank.transform.localScale.y, 275f);
                if (fuelTankGameObject.GetComponent<Rigidbody>())
                {
                    fuelTankGameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

        }
    }

    public void PlayPourAnimation()
    {
        animator.SetBool("pour",true);
        animator.SetBool("idle",false);
    }

    public void DropFuelTank()
    {
        var fuelTankToDrop = weaponSlot.transform.GetChild(0).gameObject;
        if (fuelTankToDrop != null)
        {
            if (fuelTankToDrop.GetComponent<Rigidbody>())
            {
                fuelTankAcquired = false;
                fuelTankToDrop.GetComponent<Rigidbody>().isKinematic = false;
                fuelTankToDrop.transform.SetParent(null);
            }
        }

    }
}
