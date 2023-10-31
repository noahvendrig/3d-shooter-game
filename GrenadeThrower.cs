using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour {


	public GameObject player;

	public GameObject switchScript;

	public float throwForce = 40f;
	public GameObject grenadePrefab;

	void Start()
	{
		
	}


	// Update is called once per frame
	void Update () {
		Weaponswitching weaponSwitchingScript = switchScript.GetComponent<Weaponswitching>();

		if ( (Input.GetMouseButtonDown(0)) && (Weaponswitching.selectedWeapon == 3) && (!PauseMenu.isPaused) )
		{
			ThrowGrenade();
		}
	}

	void ThrowGrenade()
	{
		GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
		Rigidbody rb = grenade.GetComponent<Rigidbody>();

		rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
		Weaponswitching weaponSwitchingScript = switchScript.GetComponent<Weaponswitching>();
		weaponSwitchingScript.Update();
		//if (weaponSwitchingScript.selectedWeapon == 2)
		{
				
		}   
	}
	
}
