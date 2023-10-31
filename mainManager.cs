using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainManager : MonoBehaviour {

	public GameObject pauseFolder;
	public GameObject settingsFolder;
	// Use this for initialization
	void Start () {

		pauseFolder.SetActive(false);
		settingsFolder.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
