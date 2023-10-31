using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	//Vector2 sensitivity = new Vector2(0.5f, 0.5f);
	Vector2 mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X") * 2, Input.GetAxisRaw("Mouse Y") * 2);

	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
		{
			mouseMovement = mouseMovement * 1000;
		}
    }
}
