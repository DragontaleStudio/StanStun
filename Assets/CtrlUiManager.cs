using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CtrlUiManager : MonoBehaviour 
{
	public Player model;

	public Text resources;
	public Text stunned;
	public GameObject stunn;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{ 
			goBack();
		}

		if (model != null)
		{
			if (model.stunnedFor > 0)
			{
				model.stunnedFor -= Time.deltaTime;
				stunned.text = "Stunned for: " + model.stunnedFor.ToString();
			}else
			{
				stunned.text = "Not Stunned";

				if (model.carryResources == 0)
				{
					stunn.SetActive(true);
				}
			}
		}
	}

	void OnEnable()
	{
		EventManager.gotStunned += onGotStunned;
		EventManager.stunnEnemy += onStunnEnemy;
	}
	
	void OnDisable()
	{
		EventManager.gotStunned -= onGotStunned;
		EventManager.stunnEnemy -= onStunnEnemy;
	}

	void onGotStunned()
	{
		model.stunnedFor = 3.0f;

		resources.text = "Resources: " + model.carryResources.ToString();
		stunned.text = "Stunned for: " + model.stunnedFor.ToString();
		stunn.SetActive(false);
	}

	void onStunnEnemy()
	{
		resources.text = model.carryResources.ToString();
	}

	private void goBack()
	{	
		Application.Quit();
		//		if (!isPaused)
		//		{
		//			pauseGame();
		//		}else if (isPaused)
		//		{
		//			resumeGame();
		//		}
	}

	//XXX: TEST
	public void stunnMe()
	{
		EventManager.onGotStunned();
	}
}
