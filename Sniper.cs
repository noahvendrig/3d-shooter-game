using UnityEngine;
using System.Collections;

public class Sniper : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public GameObject impactEffect;

    public ParticleSystem bullet;

    public AudioSource Snipershot;
    public AudioSource reloadSound;

    public bool Scoped;

    private float nextTimeToFire = 0f;

    public Animator animator;

    //
    public float radius = 10f;
    public float force = 2000f;
    //

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public GameObject sniperHolder;


    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void OnEnable()
    {
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
        {
            //Scope scope = sniperHolder.GetComponent<Scope>();
            //scope.OnUnscoped();
        }
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
        

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
            
        isReloading = true;
        Debug.Log("RELOADING");

        //
        animator.Play("SniperReload");
        //

        animator.SetBool("Reloading", true);
        reloadSound.Play();
        yield return new WaitForSeconds(reloadTime - .4f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.4f);

        currentAmmo = maxAmmo;
        isReloading = false;

    }

    void Shoot()
    {
        Snipershot.Play();
        bullet.Play();
        reloadSound.Play();
        //animator.SniperReload();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            target target = hit.transform.GetComponent<target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

            StartCoroutine(Reload());
        }

        //
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.Destroy();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        //
    }
}
