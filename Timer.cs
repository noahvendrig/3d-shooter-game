using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;

    float theTime;

    //public float speed = 1;
	// Use this for initialization
	public void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	public void Update () {
        theTime += Time.deltaTime * 1;
        string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((theTime % 3600)/60).ToString("00");
        string seconds = (theTime % 60).ToString("00");
        timerText.text = hours + ":" + minutes + ":" + seconds;
    }
}

