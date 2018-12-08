using UnityEngine;

public class MoveBack : MonoBehaviour {

	public float speed;

    void LateUpdate () {
		transform.Translate(-Vector3.forward*Time.deltaTime*speed);
	}
}
