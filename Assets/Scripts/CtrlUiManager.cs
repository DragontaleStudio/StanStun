using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CtrlUiManager : MonoBehaviour 
{
	public Player model;

	public Text resources;
	public Text stunned;

	public Text blueTeam;
	public Text redTeam;
	public GameObject scoreDummy;

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
		EventManager.stuffPickup += updateResourcesText;
		EventManager.stuffFullyLoaded += updateResourcesText;
		EventManager.stuffDepositTobase += onStuffDepositTobase;
	}
	
	void OnDisable()
	{
		EventManager.gotStunned -= onGotStunned;
		EventManager.stunnEnemy -= onStunnEnemy;
		EventManager.stuffPickup -= updateResourcesText;
		EventManager.stuffFullyLoaded -= updateResourcesText;
		EventManager.stuffDepositTobase -= onStuffDepositTobase;
	}

	void onGotStunned(string player)
	{
		model.stunnedFor = 3.0f;

		updateResourcesText();
		stunned.text = "Stunned for: " + model.stunnedFor.ToString();
		stunn.SetActive(false);
	}

	void onStunnEnemy(string player)
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
		EventManager.onGotStunned("test");
	}

	private void updateResourcesText()
	{
		resources.text = "Resources: " + model.carryResources.ToString();
	}

	private void onStuffDepositTobase()
	{
		blueTeam.text = ((int) scoreDummy.transform.localPosition.y).ToString();
		redTeam.text = ((int) scoreDummy.transform.localPosition.x).ToString();
	}
}
