using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private Vector3 forceDirection = Vector3.forward;
    private Rigidbody rb;
    [SerializeField]
    private float force = 10f;
    public bool playerCollided;
    public static Chair instance;
    private bool chairMoved;
    public AudioSource chairSound;
    public AudioClip chairClip;
    private void Awake()
    {
        instance = this;
        chairSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        chairSound.clip = chairClip;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!chairMoved && playerCollided)
        {
            rb.AddForce(forceDirection.normalized*force,ForceMode.Impulse);
            chairSound.Play();
            chairMoved = true;
        }
    }


}
