using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    float countdown;
    bool hasExploded = false;

    public GameObject explosionEffect;
    public float radius = 5f;
    public float force = 700f;


    //taking damage:
    public float damage = 150f;

    // Use this for initialization
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Debug.Log("Exploded");

        Instantiate(explosionEffect, transform.position, transform.rotation);

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
        //AreaDamageEnemies();

        Destroy(gameObject);


        
    }

    /*
    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            target enemy = col.GetComponent<target>();
            if (enemy != null)
            {
                // linear falloff of effect
                float proximity = (location - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                if (target != null)
                {
                    target.TakeDamage(damage * effect);
                    
                }


                //enemy.ApplyDamage(damage * effect);
            }
        }
        */
    //}

}
