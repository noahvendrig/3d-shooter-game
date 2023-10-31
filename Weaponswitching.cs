using UnityEngine;

public class Weaponswitching : MonoBehaviour {

    public Weaponswitching() { }

    public static int selectedWeapon = 0;

    public static bool canChange = true;

    public GameObject gunScript;
    public GameObject sniperScript;

	// Use this for initialization
	void Start () {
		SelectWeapon();
		selectedWeapon = 0;
	}
	
	// Update is called once per frame
	public void Update () {

        gun gunCheck = gunScript.GetComponent<gun>();
        SniperRifle sniperCheck = sniperScript.GetComponent<SniperRifle>();

        if ( (gun.isReloading == false) && (SniperRifle.isReloading == false) )
        {
			if(!PauseMenu.isPaused)
			{
				canChange = true;
			}
        }

		if (selectedWeapon == 1)
		{

		}

		if (selectedWeapon == 2)
		{

		}

		if (selectedWeapon == 3)
		{

		}



		int previousSelectedWeapon = selectedWeapon;



		//Scope objScope = null;



		/*if (selectedWeapon == 0)
		{
			objScope = GameObject.Find("SniperHolder").GetComponent<Scope>();
		}
		*/
		//if (previousSelectedWeapon > 0 || objScope.isScoped == false) 
		//{

			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				if (selectedWeapon >= transform.childCount - 1)
					selectedWeapon = 0;
				else
					selectedWeapon++;
			}
			
				if (Input.GetAxis("Mouse ScrollWheel") < 0f)
				{
					if (selectedWeapon <= 0)
						selectedWeapon = transform.childCount - 1;
					else
						selectedWeapon--;
				}

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				selectedWeapon = 0;
			}

			if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
			{
				selectedWeapon = 1;
			}

			if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 2)
			{
				selectedWeapon = 2;
				
			}

			if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 2)
			{
				selectedWeapon = 3;

			}

		if ( (previousSelectedWeapon != selectedWeapon) && canChange && (!PauseMenu.isPaused) && (SniperRifle.isReloading == false) )
			{
				SelectWeapon();
			}
		//}
		// else: Can't change weapons in Scope
	}
	void SelectWeapon ()
	{
	
		int i = 0;
		foreach (Transform weapon in transform)
		{
			if (i == selectedWeapon)
				weapon.gameObject.SetActive(true);
			else
				weapon.gameObject.SetActive(false);
			i++;

			//Debug.Log(selectedWeapon);
		}
	}
	
}
