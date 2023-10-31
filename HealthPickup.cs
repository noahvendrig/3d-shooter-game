using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public GameObject objectToSpawn;
    public GameObject newobject;

    // Use this for initialization
    void Start () {
        InvokeRepeating("SpawnObject", 7f, 7f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnObject()
    {
        float randomX = Random.Range(57,157);
        float randomZ = Random.Range(39, 139); 
        Vector3 randomLocation = new Vector3(randomX,-3,randomZ);
        newobject = Instantiate(objectToSpawn, randomLocation, transform.rotation);
        Destroy(newobject, 7f);
    }

    
}
