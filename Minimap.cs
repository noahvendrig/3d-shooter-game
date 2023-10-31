using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
	public Transform player;
	public Transform arrow;


	private void LateUpdate()
	{
		Vector3 newPosition = player.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;

		//transform.rotation = Quaternion.Euler(90f, arrow.eulerAngles.z, 0f);
		

	}

}
