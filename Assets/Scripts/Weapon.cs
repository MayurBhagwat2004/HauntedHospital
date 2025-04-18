using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon;

    public static Weapon instance { get; set; }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (instance!=this)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        
    }
}
