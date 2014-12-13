using UnityEngine;
using System.Collections;

public class CtrlPlayerDemo : MonoBehaviour {

	public int speed=1;
	public int turnSpeed=1;

	public int playerCoins=0;

	int[] prevPosition;
	int width=32;
	int height=32;

	GameObject daPlayer;
	TheCube cube;
	Vector3 curSpeed=Vector3.zero;

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
			transform.Translate(Vector3.forward*speed*Time.deltaTime);
			moved=true;
		}

		if (Input.GetKey(KeyCode.S))
		{
//			transform.Translate(0,0,-speed*Time.deltaTime);
			transform.Translate(Vector3.back*speed*Time.deltaTime);
			moved=true;
		}

		if (Input.GetKey(KeyCode.A))
		{
//			transform.Translate(-speed*Time.deltaTime,0,0);
			transform.Translate(Vector3.left*speed*Time.deltaTime);
			moved=true;
		}

		if (Input.GetKey(KeyCode.D))
		{
//			transform.Translate(speed*Time.deltaTime,0,0);
			transform.Translate(Vector3.right*speed*Time.deltaTime);
			moved=true;
		}

		transform.Translate(curSpeed*Time.deltaTime);


		if (moved)
		{
			int[] mapPos=getMapPos();
			TheCube.Direction dir= getOverflowDirection(mapPos);
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

				int[] overflowedClampedPos=getOverFlowClampedPos(mapPos);
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

//	void FixedUpdate () 
//	{
//		// Generate a plane that intersects the transform's position with an upwards normal.
//		Plane playerPlane = new Plane(Vector3.up, transform.position);
//		
//		// Generate a ray from the cursor position
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		
//		// Determine the point where the cursor ray intersects the plane.
//		// This will be the point that the object must look towards to be looking at the mouse.
//		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
//		//   then find the point along that ray that meets that distance.  This will be the point
//		//   to look at.
//		float hitdist = 0.0f;
//		// If the ray is parallel to the plane, Raycast will return false.
//		if (playerPlane.Raycast (ray, out hitdist)) 
//		{
//			// Get the point along the ray that hits the calculated distance.
//			Vector3 targetPoint = ray.GetPoint(hitdist);
//			
//			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
//			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
//			
//			// Smoothly rotate towards the target point.
//			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
//		}
//	}

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

	public TheCube.Direction getOverflowDirection(int[] pos)
	{
		int x=pos[0];
		int y=pos[1];
		TheCube.Direction dir=TheCube.Direction.ANY_FUCKIN_WHERE;
		if (x<0) 
		{
			dir=TheCube.Direction.EAST;
		}
		else if (x>=width) 
		{
			dir=TheCube.Direction.WEST;
		}
		else if (y<0)
		{
			dir=TheCube.Direction.SOUTH;
		}
		else if (y>=height)
		{
			dir=TheCube.Direction.SOUTH;
		}
		return dir;
	}

	public int[] getOverFlowClampedPos(int[] pos)
	{
		int x=pos[0];
		int y=pos[1];
		if (x<0) 
		{
			x=width-1;
		}
		else if (x>=width) 
		{
			x=0;
		}
		else if (y<0)
		{
			y=height-1;
		}
		else if (y>=height)
		{
			y=0;
		}
		return new int[]{x,y};
	}
}
