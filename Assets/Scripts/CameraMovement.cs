using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    float xRotation;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float sensitivity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = 0f;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        sensitivity = 1000f;
    }

    void Update()
    {
        Look();
    }

    private void Look()
    {
        if (player != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(mouseX * Vector3.up);


        }
    }
}
