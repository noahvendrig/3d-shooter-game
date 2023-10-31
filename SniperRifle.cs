using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SniperRifle : MonoBehaviour
{
	public SniperRifle() { }

	//sniper

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
	public static bool isReloading = false;

	public GameObject sniperHolder;


	//scope

	public Animator scopeAnimator;
	public GameObject scopeOverlay;
	public GameObject weaponCamera;
	public Camera mainCamera;
	public float scopedFOV = 15f;
	private float normalFOV;
	private FirstPersonController firstPerson;

	public bool isScoped = false;


	//public Transform parentTransform;
	public GameObject scopeCross;


	public GameObject healthBar;
	public GameObject scopeNorm;

	void Start()
	{
		animator = GetComponent<Animator>();
		
	}

	private void Awake()
	{
		scopeCross.SetActive(false);
	}

	void OnEnable()
	{
		//
		animator.SetBool("Reloading", false);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			isScoped = !isScoped;
			scopeAnimator.SetBool("Scoped", isScoped);

			if (isScoped)
				StartCoroutine(OnScoped());
			else
				OnUnscoped();
		}

		if (isReloading)
		{
			Scope scope = sniperHolder.GetComponent<Scope>();
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

	public void OnUnscoped()
	{
		scopeOverlay.SetActive(false);
		weaponCamera.SetActive(true);

		healthBar.SetActive(true);

		scopeCross.SetActive(false);
		scopeNorm.SetActive(true);

		mainCamera.fieldOfView = normalFOV;

		firstPerson = GameObject.Find("FPSPlayer").GetComponent<FirstPersonController>();

		/*
		if (firstPerson != null)
		{
		   float doubleSpeed = firstPerson.GetSpeed() * 2f;
		   firstPerson.SetSpeed(doubleSpeed);

		}
		*/

	}

	public IEnumerator OnScoped()
	{
		animator.Play("Scoped");

		yield return new WaitForSeconds(.15f);
		scopeOverlay.SetActive(true);
		// false or true??
		weaponCamera.SetActive(false);
		//

		/*
		for (int j = 0; j < parentTransform.childCount; j++)
		{
			parentTransform.GetChild(j).gameObject.SetActive(false);
		}
		*/

		healthBar.SetActive(false);

		scopeCross.SetActive(true);
		scopeNorm.SetActive(false);

		firstPerson = GameObject.Find("FPSPlayer").GetComponent<FirstPersonController>();

		/*
		if (firstPerson != null)
		{
			firstPerson.SetSpeed(firstPerson.GetSpeed() / 2);

		}
		*/

		normalFOV = mainCamera.fieldOfView;
		mainCamera.fieldOfView = scopedFOV;
	}


	IEnumerator Reload()
	{
		//reloadSound.Play();

		

		isReloading = true;
		Debug.Log("RELOADING");

		//
		animator.Play("SniperReload");
		//

		animator.SetBool("Reloading", true);
		reloadSound.Play();
		Animation reload = animator.GetComponent<Animation>();
		reload.Play();
		yield return new WaitForSeconds(reloadTime - .4f);

		animator.SetBool("Reloading", false);
		yield return new WaitForSeconds(.4f);

		currentAmmo = maxAmmo;
		isReloading = false;

	}

	void Shoot()
	{
		
		//Snipershot.Play();
		//bullet.Play();
		if(isScoped == true)
		{
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
			OnUnscoped();
			StartCoroutine(Reload());
		}

		

	}
}
