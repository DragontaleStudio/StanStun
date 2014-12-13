using UnityEngine;
using System.Collections;

public class CtrlCollectible : MonoBehaviour 
{
	public GameObject destroySpark;
	
	void OnTriggerEnter(Collider other) 
	{
		CtrlPlayer player = other.GetComponent<CtrlPlayer>();

		if (player.canCollect())
		{
			//Brodcast event
			player.onStuffPickup();
			
			//Instantiate destroy particle
			GameObject goSpark = (GameObject) GameObject.Instantiate(destroySpark);
			goSpark.transform.localPosition = gameObject.transform.localPosition;
			
			//Destroy collectible
			Destroy(gameObject);
		}
//		else
//		{
//			EventManager.on
//		}
	}
}
