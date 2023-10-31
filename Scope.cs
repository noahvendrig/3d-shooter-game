using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Scope : MonoBehaviour {

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
    //public GameObject scopeNorm;
    

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
    }

    public void OnUnscoped ()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        healthBar.SetActive(true);

        scopeCross.SetActive(false);
        //scopeNorm.SetActive(true);

        mainCamera.fieldOfView = normalFOV;

        firstPerson = GameObject.Find("FPSPlayer").GetComponent<FirstPersonController>();
        if (firstPerson != null)
        {
            float doubleSpeed = firstPerson.GetSpeed() * 2;
            firstPerson.SetSpeed(doubleSpeed);
        }
    }

    public IEnumerator OnScoped()
    {
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
        //scopeNorm.SetActive(false);

        firstPerson = GameObject.Find("FPSPlayer").GetComponent<FirstPersonController>();
        if (firstPerson != null)
        {
            firstPerson.SetSpeed(firstPerson.GetSpeed() / 2);
        }

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }
}
