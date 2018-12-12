using UnityEngine;

public class WallBehaviour : MonoBehaviour {

	public BrickBehaviour Brick;

	public void Start() {
        GetComponent<MoveBack>().speed = GameLogic.instance.WallSpeed;
        // SpawnBricks();	
	}

    // void SpawnBricks()
    // {
    //     for(int i = 0; i<4;i++)
	// 	{
	// 	BrickBehaviour temp =  Instantiate(Brick,this.transform,false);
	// 	temp.transform.localPosition = new Vector3(-3+i*2,1,0);
	// 	}
    // }


}
