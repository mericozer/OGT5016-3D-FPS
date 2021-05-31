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

    [SerializeField] private int maxAmmo;
    [SerializeField] private int clipSize;
    private int currentAmmo;
    private int currentClip;

    private bool isReloading = false;
    private bool isEnabled;
    private bool isInConvo = false;
    [SerializeField] private bool isAutomatic;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        currentClip = clipSize;
        
        CanvasController.instance.EditBulletBar(clipSize, currentClip, maxAmmo);

        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled)
        {
            CanvasController.instance.EditBulletBar(clipSize, currentClip, currentAmmo);
        }
        if (isReloading)
        {
            return;
        }
        if (currentClip <= 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(Reload());
                
            }
            return;
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (isAutomatic)
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
        if (currentAmmo >= clipSize)
        {
            if (currentClip > 0)
            {
                currentAmmo -= (clipSize - currentClip);
                currentClip = clipSize;
            }
            else
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
        isReloading = false;
    }

    private void Shoot()
    {
        //TO DO
        shootingEffect.Play();
        
        
        //Debug.Log("CURRENT CLIP COUNT IS: " + currentClip);
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out hit, range ))
        {
            
            
            if (hit.collider != null)
            {
                GameObject damageEffectGO = Instantiate(damageEffect, hit.point, Quaternion.LookRotation(hit.normal));
                
                Target target = hit.transform.GetComponent<Target>();

                if (target != null)
                {
                    damageEffectGO.transform.parent = target.gameObject.transform;
                    target.TakeDamage(damage);
                   
                    //damageEffectGO.GetComponent<DestroyAfterTimeParticle>().QuickDestroy(0.1f);
                }
            }

           
            
        }
        
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnDisable()
    {
        isEnabled = false;
    }

}
