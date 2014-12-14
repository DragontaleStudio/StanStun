using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CtrlUiManager : MonoBehaviour 
{
	public Player model;

	public Animator resourcesAnimator;
	public Animator pointsAnimator;

	public Text resources;
	public Text stunned;

	//Score UI
	public Text blueTeam;
	public Text redTeam;
	public GameObject scoreDummy;

	//Resources Pie
	public GameObject pie1;
	public GameObject pie2;
	public GameObject pie3;
	public GameObject pie4;
	public GameObject pie5;
	public GameObject pie6;
	public GameObject full;
	public Text points;


	public GameObject stunn;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{ 
			goBack();
		}

//		if (model != null)
//		{
//			if (model.stunnedFor > 0)
//			{
//				model.stunnedFor -= Time.deltaTime;
//				stunned.text = "Stunned for: " + model.stunnedFor.ToString();
//			}else
//			{
//				stunned.text = "Not Stunned";
//
//				if (model.carryResources == 0)
//				{
//					stunn.SetActive(true);
//				}
//			}
//		}
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
		switch(model.carryResources)
		{
		 case 0:
			resourcesAnimator.SetBool("fullpoints", false);
			pie1.SetActive(false);
			pie2.SetActive(false);
			pie3.SetActive(false);
			pie4.SetActive(false);
			pie5.SetActive(false);
			pie6.SetActive(false);
			full.SetActive(false);
			break;

		case 1:
			pie1.SetActive(true);
			break;
		case 2:
			pie2.SetActive(true);
			break;
		case 3:
			pie3.SetActive(true);
			break;
		case 4:
			pie4.SetActive(true);
			break;
		case 5:
			pie5.SetActive(true);
			break;
		case 6:
			pie6.SetActive(true);
			full.SetActive(true);
			resourcesAnimator.SetBool("fullpoints", true);

			break;
		}

		this.points.text = model.points.ToString();
	}

	private void onStuffDepositTobase()
	{
		blueTeam.text = ((int) scoreDummy.transform.localPosition.y).ToString();
		redTeam.text = ((int) scoreDummy.transform.localPosition.x).ToString();

		if (!pointsAnimator.enabled)
		{
			pointsAnimator.enabled=true;
		}

		pointsAnimator.Play("addPoints");

		updateResourcesText();
	}
}
