using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSaver : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }
}
