using UnityEngine;
using System.Collections;

public class CtrlPlayerDemo : MonoBehaviour {

	public int speed=1;
	public int turnSpeed=1;
	public float damping=0.5f;

	public int playerCoins=0;

	int[] prevPosition;
	GameObject daPlayer;
	TheCube cube;

	public Vector3 curSpeed=Vector3.zero;


	// Use this for initialization
	void Start () 
	{
		prevPosition=getMapPos();
		daPlayer=GameObject.Find("PlayerMan");
		cube=new TheCube();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 vec=transform.localPosition;
		bool moved=false;
		if (Input.GetKey(KeyCode.W))
		{
//			transform.Translate(0,0,speed*Time.deltaTime);
//			transform.Translate(Vector3.forward*speed*Time.deltaTime);
			curSpeed.z=speed;
			moved=true;
		}

		if (Input.GetKey(KeyCode.S))
		{
//			transform.Translate(0,0,-speed*Time.deltaTime);
//			transform.Translate(Vector3.back*speed*Time.deltaTime);
			curSpeed.z=-speed;
			moved=true;
		}

		if (Input.GetKey(KeyCode.A))
		{
//			transform.Translate(-speed*Time.deltaTime,0,0);
//			transform.Translate(Vector3.left*speed*Time.deltaTime);
			curSpeed.x=-speed;
			moved=true;
		}

		if (Input.GetKey(KeyCode.D))
		{
//			transform.Translate(speed*Time.deltaTime,0,0);
//			transform.Translate(Vector3.right*speed*Time.deltaTime);
			curSpeed.x=speed;
			moved=true;
		}

		curSpeed=curSpeed*damping;
		transform.Translate(curSpeed*Time.deltaTime);


//		if (moved)
		{
			int[] mapPos=getMapPos();
			TheCube.Direction dir= cube.getOverflowDirection(mapPos);
			if (dir!=TheCube.Direction.ANY_FUCKIN_WHERE)
			{
				Debug.Log("Overflowed!!!");
				int targetSide=cube.getNeighbour( dir);
				Debug.Log("Overflowed to "+dir+ " to target side "+targetSide );
				GameObject theFloor=GameObject.Find("Face"+targetSide);

				daPlayer.transform.parent=theFloor.transform;
				Camera.main.transform.parent.parent=theFloor.transform;
				Camera.main.transform.parent.localRotation=Quaternion.identity;
				daPlayer.transform.localRotation=Quaternion.identity;


				cube.currentSide=targetSide;

				int[] overflowedClampedPos=cube.getOverFlowClampedPos(mapPos);
				Debug.Log("ClampedPos:"+overflowedClampedPos[0]+","+overflowedClampedPos[1]);
				transform.localPosition=new Vector3(overflowedClampedPos[0]-16,0,overflowedClampedPos[1]-16);

//				Vector3 eulerCameraAngles=new Vector3(theFloor.transform.eulerAngles.x+90,theFloor.transform.eulerAngles.y,theFloor.transform.eulerAngles.z);
//				Camera.main.transform.eulerAngles=eulerCameraAngles;
				mapPos=getMapPos();
			}

			int xCoord=mapPos[0];
			int yCoord=mapPos[1];
//			Debug.Log("Player map coord:" + xCoord+","+yCoord);
			if ( getMap()[xCoord,yCoord]==1)
			{
				transform.localPosition=vec;
			}
			else if (getMap()[xCoord,yCoord]==2)
			{

				if (gameObject.transform.parent.Find("LVL"+xCoord+","+yCoord)!=null)
				{
					Destroy(gameObject.transform.parent.Find("LVL"+xCoord+","+yCoord).gameObject);
					playerCoins++;
				}
			}
		}
	}

	public int[,] getMap()
	{
		return gameObject.transform.parent.GetComponent<GenLevelCellular>().map;
	}

	public int[] getMapPos()
	{
		int xCoord=Mathf.CeilToInt(transform.localPosition.x)+16;
		int yCoord=Mathf.CeilToInt(transform.localPosition.z)+16;

		return new int[]{xCoord,yCoord};
	}


}
