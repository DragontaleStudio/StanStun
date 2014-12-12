using UnityEngine;
using System.Collections;

public class CameraRail : MonoBehaviour 
{
	public GameObject target;
	public float damping = 1;
	public bool lookAt=true;
	public bool rotate=true;
	public Vector3 offset;
	
	public float tolerance = 0.1f;
	
	void Start() 
	{
		//		offset = transform.position - target.transform.position;
		//		offset=new Vector3(0,0,0);
	}
	
	void LateUpdate() 
	{
		Vector3 desiredPosition = target.transform.position + offset;
		Vector3 position;
		if (Vector3.Distance(desiredPosition,transform.position)<tolerance)
		{
			position=desiredPosition;
		}
		else
		{
			position= Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
		}
		
		
		if (lookAt) transform.LookAt(position);
		if (rotate) transform.localRotation=Quaternion.Lerp(transform.localRotation,target.transform.localRotation,Time.deltaTime * damping);
		transform.position = position;
		
		
	}
}
