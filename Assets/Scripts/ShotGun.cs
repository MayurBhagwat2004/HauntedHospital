using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShotGun : MonoBehaviour
{
    public Camera fpsCam;
    public bool canShoot;
    private float damage = 10f;
    private float range = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject groundSmokeImpact;
    public GameObject bulletImpact;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> shotGunSounds;
    [SerializeField]
    private List<AnimationClip> animationClips = new List<AnimationClip>();
    private enum ShotGunAnimations
    {
        reload,
        shoot
    }
    private void Start()
    {
        //if (SoundHandler.instance != null)
        //{
        //    audioSource.volume = SoundHandler.instance.audioSource.volume;
        //}
        canShoot = true;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        fpsCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        animator.SetBool(nameof(ShotGunAnimations.shoot),false);
        animator.SetBool(nameof(ShotGunAnimations.reload), false);

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Weapon.instance.isActiveWeapon && canShoot)
        {
            StartCoroutine(Shoot());
            
        }
    }


    private IEnumerator Shoot()
    {
        canShoot = false;
        audioSource.clip = shotGunSounds[0];
        audioSource.Play();

        //AnimatorStateInfo animStateInfo = new AnimatorStateInfo();
        //if (animStateInfo.IsName(nameof(ShotGunAnimations.shoot)))
        //{
        //    if (animStateInfo.normalizedTime >= 0f)
        //    {
        //        animator.SetBool(nameof(ShotGunAnimations.shoot), false);
        //        animator.SetBool(nameof(ShotGunAnimations.reload), false);

        //    }
        //    else
        //    {
        //        animator.SetBool(nameof(ShotGunAnimations.shoot), true);
        //        animator.SetBool(nameof(ShotGunAnimations.reload), false);

        //    }
        //}



        muzzleFlash.Play(); 
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit, range))
        {
            Ghost ghost = hit.transform.GetComponent<Ghost>();
            if (ghost != null)
            {

                ghost.TakeDamage(damage);
                ghost.isHurt = true;
                GameObject effect = Instantiate(impactEffect, hit.point,Quaternion.LookRotation(hit.normal));
                Destroy(effect,1f);

            }
            else if (hit.transform.gameObject.CompareTag("wall"))
            {

                GameObject bulletImpactGameObject = Instantiate(bulletImpact,hit.point+hit.normal*0.01f , Quaternion.LookRotation(hit.normal));
                GameObject impactEffectGameObject = Instantiate(groundSmokeImpact,hit.point,Quaternion.LookRotation(hit.normal));
                Destroy(impactEffectGameObject, 1f);
                Destroy(bulletImpactGameObject, 1f);

            }

        }

        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    
    

}
