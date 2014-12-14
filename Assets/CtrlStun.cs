using UnityEngine;
using System.Collections;

public class CtrlStun : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		CtrlPlayer player = other.GetComponent<CtrlPlayer>();
		
		if (player!=null && player.hasResources())
		{
			player.setStunned(3.0f);
		}
	}
}
