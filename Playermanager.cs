using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermanager : MonoBehaviour {

    #region Singleton

    public static Playermanager instance;

    void Awake()

    {
        instance = this;
    }
    #endregion 

    public GameObject player;
}
