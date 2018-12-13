using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {


	public float zPos;

	public Transform water1;
	public Transform water2;

	void Start()
	{
		this.GetComponent<MoveBack>().speed = GameLogic.instance.WallSpeed;
	}

	private void Update() {
		if(water1.transform.position.z<=-300)
			water1.Translate(-Vector3.right*zPos*10);
		if(water2.transform.position.z<=-300)
			water2.Translate(-Vector3.right*zPos*10);

		
	}
}
