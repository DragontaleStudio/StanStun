using UnityEngine;
using System.Collections;

public class CameraRail : MonoBehaviour 
{
	public GameObject target;
	public float damping = 1;
	public bool lookAt=true;
	public bool rotate=true;

	public bool enableRotationTargeting;

	public Vector3 offset;
	
	public float tolerance = 0.1f;

	public Vector3 targetRotation;

	void Start() 
	{
		//		offset = transform.position - target.transform.position;
		//		offset=new Vector3(0,0,0);

		if (targetRotation==null)
		{
			targetRotation=transform.Find("Main Camera").transform.localRotation.eulerAngles;
		}
	}
	
	void LateUpdate() 
	{
		if (target!=null)
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

			if (enableRotationTargeting)
			{
				Quaternion curRotation=transform.Find("Main Camera").localRotation;
				Quaternion trgRotation=Quaternion.Euler(targetRotation);
				if (Quaternion.Angle(curRotation,trgRotation)>1)
				{

					transform.Find("Main Camera").transform.localRotation=Quaternion.Lerp(curRotation,trgRotation,damping*Time.deltaTime);
				}
			}
		}		
	}
}
