using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public static door instance;
    private Animator animator;
    public bool openDoor;
    public AudioSource audioSource;
    public AudioClip doorSound;
   
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null) 
        {
            animator.enabled = false;

        }
        if (audioSource!=null)
        {
            audioSource.clip = doorSound;
        }
    }



    void Update()
    {
        
    }


    public IEnumerator DoorAnimation()
    {
        animator.enabled = true;
        if (openDoor)
        {
            audioSource.Play();
            animator.SetBool("openDoor",true);
            animator.SetBool("closeDoor", false);


        }
        else
        {
            animator.SetBool("openDoor", false);
            animator.SetBool("closeDoor", true);

        }
        yield return new WaitForSeconds(1.5f);
    }
}
