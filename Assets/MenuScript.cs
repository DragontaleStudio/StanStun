using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("Main_Scene");
		}	
	}
}
