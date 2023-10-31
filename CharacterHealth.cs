using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour {

    public float PlayerHealth;
    public GameObject FPScam;
    public GameObject Crosshair;
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Slider HealthBar;
    public Transform Player;
    public Transform spawnpoint;
    public float damageValue = 10f;
    public float healValue = 60f;
    public GameObject deathcam;
    public Text deathText;

    void Start()
    {
        deathText.enabled = false;
        MaxHealth = PlayerHealth;
        CurrentHealth = MaxHealth;
        HealthBar.value = CalculateHealth();
        FPScam.gameObject.SetActive(true);
        deathcam.SetActive(false);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(damageValue);
        }
        else
        {
            if (other.gameObject.CompareTag("HealthPickup"))
            {
                AddHealth(healValue);
                Destroy(other.gameObject);
            }
        }
    }

    void AddHealth(float healValue)
    {
        if (CurrentHealth == 100)
        {
            return;
        }
        else
        {
            CurrentHealth += healValue;
            HealthBar.value = CalculateHealth();
        }
        
    }

    void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        HealthBar.value = CalculateHealth();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    public void Die()
    {
        CurrentHealth = 0;
        Debug.Log("You Died");
        FPScam.SetActive(false);
        deathcam.SetActive(true);
        
        //StartCoroutine(StartAgain());

        /*if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
            print("e3e");
        }
        */

        StartCoroutine(Restart());

    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        deathText.enabled = true;


        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
    }



    /*IEnumerator StartAgain()
    {
        
        int p;


        Time.timeScale = 0.01f;
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        //Time.timeScale = 1;

        
        
        
    }
    */
    


    /*IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(3);

        Player.transform.position = spawnpoint.transform.position;
    }
    */


    /*void OnCollisionEnter(Collision collision)
    {
        

        CharacterHealth characterhealth = collision.gameObject.GetComponent<CharacterHealth>();


    }
    */
}
