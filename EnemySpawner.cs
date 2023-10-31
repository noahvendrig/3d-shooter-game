using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject objectToSpawn;
    public GameObject newobject;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnObject", 4f, 4f);

        StartCoroutine(WaveOne());
    }

    IEnumerator WaveOne()
    {
        yield return new WaitForSeconds(10);

        InvokeRepeating("SpawnObject", 4f, 4f);

        StartCoroutine(WaveTwo());
    }

    IEnumerator WaveTwo()
    {
        yield return new WaitForSeconds(40);

        InvokeRepeating("SpawnObject", 5f, 5f);

        StartCoroutine(WaveThree());
    }

    IEnumerator WaveThree()
    {
        yield return new WaitForSeconds(40);

        InvokeRepeating("SpawnObject", 6f, 6f);
    }

    void SpawnObject()
    {
        newobject = Instantiate(objectToSpawn, transform.position, transform.rotation);
        
    }

    
}
