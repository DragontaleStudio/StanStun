using UnityEngine;
using System.Collections;

public class CtrlBase : MonoBehaviour 
{
	public int team;
	public GameObject depositSpark;
	
	void OnTriggerEnter(Collider other) 
	{
		CtrlPlayer player = other.GetComponent<CtrlPlayer>();
		
		if (player.model.team == team && player.hasResources())
		{
			//BroadcastEvent
			player.onStuffDepositTobase();
			
			//Instantiate deposit particle
			GameObject goSpark = (GameObject) GameObject.Instantiate(depositSpark);
			goSpark.transform.localPosition = gameObject.transform.localPosition;
		}
	}
}
