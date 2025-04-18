using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region variables
    public GameObject spawnPoint;
    public float health;
    private Camera camera1;
    [SerializeField]
    public bool interacted;
    [SerializeField]
    private float speed;
    private CharacterController characterController;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float checkGroundDistance = 1.72f;
    private bool isGrounded;
    [SerializeField]
    private Vector3 velocity;
    private float gravity = -9.8f;
    private bool positionSet;
    private enum Interactables
    {
        weapon
    }
    
    #endregion
    void Start()
    {
        spawnPoint = GameObject.FindWithTag("spawnPoint");
        speed = 8f;
        characterController = GetComponent<CharacterController>();
        camera1 = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        if (spawnPoint!=null)
        {
            this.transform.position = spawnPoint.transform.position;
        }
    }

    void Update()
    {
        movement();
        if (spawnPoint != null && !positionSet)
        {
            this.transform.position = spawnPoint.transform.position;
            positionSet = true;
        }
        else
        {
            spawnPoint = GameObject.FindWithTag("spawnPoint");

        }
    }

    #region movement()
    private void movement()
    {

        //Fetching horizontal and vertical inputs 
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //Assigning vector3 variable "movement" the inputs for using it in Move()
        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(movement * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * characterController.height / 2,checkGroundDistance, groundLayer);


        if (isGrounded && velocity.y<0)
        {
            velocity.y = gravity;
        }
       
        velocity.y += Time.deltaTime * gravity;
        characterController.Move(velocity*Time.deltaTime);

    }
    #endregion

    



}


