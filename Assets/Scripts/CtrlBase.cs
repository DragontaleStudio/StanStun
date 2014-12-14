using UnityEngine;
using System.Collections;

public class CtrlBase : MonoBehaviour 
{
	public int team;
	public GameObject depositSpark;
	
	void OnTriggerEnter(Collider other) 
	{
		CtrlPlayer player = other.GetComponent<CtrlPlayer>();
		
		if (player != null && player.model.team == team && player.hasResources())
		{
			//BroadcastEvent
			player.onStuffDepositTobase();
			
			//Instantiate deposit particle
			GameObject goSpark = (GameObject) GameObject.Instantiate(depositSpark);
			goSpark.transform.parent = this.transform;
			goSpark.transform.localPosition = Vector3.zero;
			goSpark.transform.rotation = Quaternion.identity;

			Destroy(goSpark, 3.0f);
		}
	}
}
