#pragma strict

function Start () 
{
	goToNextInSecs(4);
}

function Update () 
{
	if (Input.GetMouseButtonDown(0))
	{
		Application.LoadLevel("scene_menu");
	}	
}

function goToNextInSecs(secs:int)
{
	yield WaitForSeconds(3.5);
	GameObject.Find("Camera").GetComponent(FadeInout).fadeIn();
	yield WaitForSeconds(2);
	Application.LoadLevel("scene_menu");
}

