using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f; 
    [SerializeField] private float range = 100f;
    [SerializeField] private float shootingTime;
    [SerializeField] private float nextShootingTime = 0f;
    [SerializeField] private float reloadTime = 2f;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private ParticleSystem shootingEffect;

    [SerializeField] private GameObject damageEffect;

    [SerializeField] private int maxAmmo; //max ammo of gun
    [SerializeField] private int clipSize; //max bullet a gun can take at one time
    private int currentAmmo; //how many bullets left in total
    private int currentClip; //how many bullets left in the gun

    private bool isReloading = false;
    private bool isEnabled;
    
    
    [SerializeField] private bool isAutomatic; //if it is automatic, player can hold down the left mouse button and shoot constantly
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        currentClip = clipSize;
        
        CanvasController.instance.EditBulletBar(clipSize, currentClip, maxAmmo); //edit numbers o canvas

        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled) //every time gun changes and gets enabled edit the canvas
        {
            CanvasController.instance.EditBulletBar(clipSize, currentClip, currentAmmo);
        }
        if (isReloading)
        {
            return;
        }
        if (currentClip <= 0) 
        {
            if (Input.GetKey(KeyCode.R)) //if bullet ends in gun, player can reload
            {
                StartCoroutine(Reload());
                PlayerController.instance.Reload(true);
                
            }
            return;
        }
        
        if (Input.GetKey(KeyCode.R)) //if bullet ends in gun, player can reload
        {
            StartCoroutine(Reload());
            PlayerController.instance.Reload(true);
            return;
        }

        if (isAutomatic) //shooting style change getkey vs getkeydown
        {
            if (Input.GetKey(KeyCode.Mouse0) && nextShootingTime <= Time.time)
            {
                nextShootingTime = Time.time + shootingTime;
                currentClip--;
                CanvasController.instance.Shoot(currentClip);
                Shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && nextShootingTime <= Time.time)
            {
                nextShootingTime = Time.time + shootingTime;
                currentClip--;
                CanvasController.instance.Shoot(currentClip);
                Shoot();
            }
        }
        
       
    }

    private IEnumerator Reload()
    {
        isReloading = true;
       
        yield return new WaitForSeconds(reloadTime);
        
        //ammo reload u cilala
        if (currentAmmo >= clipSize) //if there are bullets more than clipsize, it can fill whole clip
        {
            if (currentClip > 0) //if clip is empty
            {
                currentAmmo -= (clipSize - currentClip);
                currentClip = clipSize;
            }
            else //if there are bullets in clip but player still reloads
            {
                currentClip = clipSize;
                currentAmmo -= clipSize;
            }
            
        }
        else
        {
            currentClip = currentAmmo;
            currentAmmo = 0;
        }
        
        CanvasController.instance.Reload(currentClip, currentAmmo);
        PlayerController.instance.Reload(false);
        isReloading = false;
    }

    private void Shoot()
    {
        shootingEffect.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out hit, range )) //shooting ray
        {
            
            if (hit.collider != null) //checks if it hit sometihng
            {
                GameObject damageEffectGO = Instantiate(damageEffect, hit.point, Quaternion.LookRotation(hit.normal));
                
                Target target = hit.transform.GetComponent<Target>();

                if (target != null) //checks if the object is a target
                {
                    damageEffectGO.transform.parent = target.gameObject.transform;
                    target.TakeDamage(damage);
                }
            }

           
            
        }
        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //flag for disabling the object
    void OnDisable()
    {
        isEnabled = false;
    }

}
