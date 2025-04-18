using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    private Weapon hoveredWeapon;
    [SerializeField]
    private Camera camera1;
    private float raycastDistance;
    public GameObject exitDoor;
    private enum Interactables
    {
        weapon
    }
    private enum CutSceneNames
    {
        firstScare
    }
    void Start()
    {
        raycastDistance = 6f;
        hoveredWeapon = null;
        camera1 = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Interact();
        if (GameHandler.instance.playerDied && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    #region Interact()
    private void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera1.transform.position, camera1.transform.TransformDirection(Vector3.forward), out hit, raycastDistance))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;
            if (Input.GetButton("Interact"))
            {
                GameHandler.instance.setDidPlayerInteracted(true);
            }
            else
            {
                GameHandler.instance.setDidPlayerInteracted(false);
            }
            GameHandler.instance.SetTagName(hit.transform.gameObject.tag);



            if (objectHitByRaycast.GetComponent<Weapon>() && !Weapon.instance.isActiveWeapon)
            {
                hoveredWeapon = objectHitByRaycast.transform.gameObject.GetComponent<Weapon>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    WeaponHandler.instance.PickUpWeapon(objectHitByRaycast.gameObject);

                }


            }

            if (objectHitByRaycast.transform.CompareTag("Key") && Input.GetKeyDown(KeyCode.E))
            {
                GameHandler.instance.keyAcquired = true;
                Destroy(objectHitByRaycast);
            }
            if (objectHitByRaycast.transform.CompareTag("ExitDoor"))
            {
                if (GameHandler.instance.keyAcquired && exitDoor!=null)
                {
                    objectHitByRaycast.GetComponent<Animator>().SetBool("open", true);
                    objectHitByRaycast.GetComponent<Animator>().SetBool("close", false);
                    objectHitByRaycast.GetComponent<BoxCollider>().isTrigger = true;
                    exitDoor.gameObject.SetActive(true);
                }

                else
                {
                    GameHandler.instance.keyAcquired = false;
                    GameHandler.instance.nearExitDoor = true;
                }

            }

            if (objectHitByRaycast.transform.CompareTag("Door") && GameHandler.instance.GetDidPlayerInteracted())
            {
                objectHitByRaycast.GetComponent<door>().openDoor = true;
                StartCoroutine(objectHitByRaycast.GetComponent<door>().DoorAnimation());
            }
            if (objectHitByRaycast.CompareTag("Lighter") && Input.GetKeyDown(KeyCode.E))
            {
                Lighter.instance.AddWeaponToSlot(objectHitByRaycast);
            }
            if (objectHitByRaycast.CompareTag("FuelTank") && Input.GetKeyDown(KeyCode.E))
            {
                FuelTank.instance.AddFuelTankToSlot(objectHitByRaycast);
            }
            if (objectHitByRaycast.CompareTag("GraveYardBody") && Input.GetKeyDown(KeyCode.E))
            {
                if(FuelTank.instance.fuelTankAcquired && !Lighter.instance.weaponAcquired)
                {
                    FuelTank.instance.fuelTankPoured = true;
                    GameObject tank = GameObject.FindWithTag("FuelTank").gameObject;
                    tank.GetComponent<Animator>().enabled = true;
                    Destroy(tank,1f);
                }
            }
            if (objectHitByRaycast.CompareTag("GraveYardBody") && Input.GetKeyDown(KeyCode.E) && FuelTank.instance.fuelTankPoured && Lighter.instance.weaponAcquired)
            {
                GameObject burnGameObject = GameObject.Find("MediumFlames");
                if (burnGameObject!=null)
                {
                    burnGameObject.GetComponent<ParticleSystem>().Play();
                    Destroy(GameObject.FindWithTag("Lighter"));
                    Ghost.instance.ghostDied = true;
                    GameHandler.instance.won.SetActive(true);
                    GameHandler.instance.wonCutScene.Play();
                }
                else
                {
                    Debug.Log("Not Found");
                }
            }

        }
        else
        {
            if (Weapon.instance != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && Weapon.instance.isActiveWeapon)
                {
                    WeaponHandler.instance.DropWeapon(WeaponHandler.instance.activeWeaponSlot.gameObject);

                }
            }

            if (Input.GetKeyDown(KeyCode.E) && Lighter.instance.weaponAcquired)
            {
                Lighter.instance.DropLighter();
            }
            if (Input.GetKeyDown(KeyCode.E) && FuelTank.instance.fuelTankAcquired)
            {
                FuelTank.instance.DropFuelTank();
            }
        }
       


    }


    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interact") && other.gameObject.CompareTag("Doll"))
        {
            GameHandler.instance.setDidPlayerInteracted(true);
            Doll.instance.shouldPlay = true;

        }
        else if (other.gameObject.CompareTag("Doll"))
        {
            GameHandler.instance.SetTagName(other.gameObject.tag);
            GameHandler.instance.setDidPlayerInteracted(false);

        }

        else
        {
            GameHandler.instance.setDidPlayerInteracted(false);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        GameHandler.instance.setDidPlayerInteracted(false);
    }

    
}

