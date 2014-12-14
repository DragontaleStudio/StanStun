using UnityEngine;
using System.Collections;

public class CtrlCollectible : MonoBehaviour 
{
	public GameObject destroySpark;
	public int team;

	void Start()
	{
		team = int.Parse(transform.parent.name.Substring(4))>3?2:1;
	}

	void OnTriggerEnter(Collider other) 
	{
		CtrlPlayer player = other.GetComponent<CtrlPlayer>();

		if (player!=null && player.canCollect())
		{
			//Brodcast event
			player.onStuffPickup();
			
			//Instantiate destroy particle
			GameObject goSpark = (GameObject) GameObject.Instantiate(destroySpark);
			goSpark.transform.localPosition = gameObject.transform.position;
			goSpark.transform.rotation = gameObject.transform.rotation;

			//Destroy collectible
			Destroy(gameObject);
		}
	}
}
