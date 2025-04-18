using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponSlot;
    public bool weaponAcquired;
    public static Lighter instance { get; set; }
    void Start()
    {
        instance = this;
        weaponSlot = GameObject.FindWithTag("LighterSlot");
    }

    void Update()
    {
    }

    public void AddWeaponToSlot(GameObject lighter)
    {
        if (weaponSlot.transform.childCount==0)
        {
            if (!weaponAcquired)
            {
                weaponAcquired = true;
                lighter.transform.SetParent(weaponSlot.transform, true);
                GameObject lighterGameObject = lighter.gameObject;
                weaponSlot.transform.localRotation = Quaternion.Euler(5.917f, -174.583f, 24.314f);
                lighter.transform.localRotation = Quaternion.Euler(-90f, lighterGameObject.transform.localRotation.y, 90f);
                lighter.transform.localPosition = new Vector3(lighter.transform.localPosition.x, 0.02f, 7.42f);
                if (lighterGameObject.GetComponent<Rigidbody>())
                {
                    lighterGameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
       
    }

    public void DropLighter()
    {

        var lighterToDrop = weaponSlot.transform.GetChild(0).gameObject;
        if (lighterToDrop != null) 
        {
            if (lighterToDrop.GetComponent<Rigidbody>())
            {
                weaponAcquired = false;
                lighterToDrop.GetComponent<Rigidbody>().isKinematic = false;
                lighterToDrop.transform.SetParent (null);
            }
        }

    }
}
