using UnityEngine;
using System.Collections;

public class CtrlStun : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		CtrlPlayer player = other.GetComponent<CtrlPlayer>();
		
		if (player!=null && player.hasResources() && gameObject.transform.parent.gameObject!=other.gameObject)
		{
			player.setStunned(3.0f);
		}
		else
		{
			Debug.Log("Not stunned:"+other.gameObject.name);
		}
	}
}
