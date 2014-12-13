using UnityEngine;
using System.Collections;

public class CtrlPlayerDemo : MonoBehaviour {

	public int speed=1;
	public int turnSpeed=1;

	public int playerCoins=0;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 vec=transform.localPosition;
		bool moved=false;
		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(0,0,speed*Time.deltaTime);
			moved=true;
		}

		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(0,0,-speed*Time.deltaTime);
			moved=true;
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-speed*Time.deltaTime,0,0);
			moved=true;
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(speed*Time.deltaTime,0,0);
			moved=true;
		}

		if (moved)
		{
			int xCoord=Mathf.CeilToInt(transform.localPosition.x)+16;
			int yCoord=Mathf.CeilToInt(transform.localPosition.z)+16;
//			Debug.Log("Player map coord:" + xCoord+","+yCoord);
			if ( xCoord<0 || yCoord<0 ||xCoord>31 || yCoord>31 ||getMap()[xCoord,yCoord]==1)
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

	void FixedUpdate () 
	{
		// Generate a plane that intersects the transform's position with an upwards normal.
		Plane playerPlane = new Plane(Vector3.up, transform.position);
		
		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		//   then find the point along that ray that meets that distance.  This will be the point
		//   to look at.
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) 
		{
			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint(hitdist);
			
			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			
			// Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
		}
	}

	public int[,] getMap()
	{
		return gameObject.transform.parent.GetComponent<GenLevelCellular>().map;
	}
}
