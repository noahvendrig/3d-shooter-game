using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {

    public gun() { }

	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 15f;

	public int maxAmmo = 10;
	private int currentAmmo;
	public float reloadTime = 1f;
	public static bool isReloading = false;

	public AudioSource reloadSound;

	public Camera fpsCam;
	public ParticleSystem muzzleflash;
	public GameObject impactEffect;

	public AudioSource Gunshot;
	private float nextTimeToFire = 0f;

	public Animator animator;

	//
	public float radius = 1f;
	//public float force = 2000f;
	//

	void Start()
	{
		currentAmmo = maxAmmo;
	}

	void OnEnable()
	{
		isReloading = false;
		animator.SetBool("Reloading", false);
	}

	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(Reload());
		}

		if(isReloading)
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
		currentAmmo--;

		Gunshot.Play();
		muzzleflash.Play();
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
					rb.AddExplosionForce(damage, nearbyObject.transform.position, radius);
				}
			}

		}
	}


	
}
