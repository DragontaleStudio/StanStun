using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class CtrlSoundManager : MonoBehaviour 
{
	public AudioClip stunSound;
	public AudioClip breakSound;
	public AudioClip passSound;
	public AudioClip collectSound;
	public AudioClip happyFeelingSound;
	public AudioClip moodyFeelingSound;
	public AudioClip angryFeelingSound;
	public AudioClip[] stepsSound;
	public AudioClip[] wingsSound;

	void OnEnable()
	{
//		TotemonEventManager.totemonJump += playJumpSound;
//		TotemonEventManager.totemonPass += playPassSound;
//		TotemonEventManager.totemonBreak += playBreakSound;
//		TotemonEventManager.totemonCollect += playCollectSound;
	}
	
	void OnDisable()
	{
//		TotemonEventManager.totemonJump -= playJumpSound;
//		TotemonEventManager.totemonPass -= playPassSound;
//		TotemonEventManager.totemonBreak -= playBreakSound;
//		TotemonEventManager.totemonCollect -= playCollectSound;
	}

	void playStunSound()
	{
		GetComponent<AudioSource>().PlayOneShot(stunSound);
	}

	void playPassSound()
	{
		GetComponent<AudioSource>().PlayOneShot(passSound);
	}

	void playBreakSound()
	{
		GetComponent<AudioSource>().PlayOneShot(breakSound);
	}

	void playCollectSound()
	{
		GetComponent<AudioSource>().PlayOneShot(collectSound);
	}

	public void playStepsSound()
	{
		GetComponent<AudioSource>().PlayOneShot(stepsSound[Random.Range(0, (stepsSound.Length-1))]);
	}


	public void playWingsSound()
	{
		GetComponent<AudioSource>().PlayOneShot(wingsSound[Random.Range(0, (wingsSound.Length-1))]);
	}

	public void playChangeFelingSound(int curFeeling)
	{
		switch (curFeeling)
		{
		case 1:
			GetComponent<AudioSource>().PlayOneShot(happyFeelingSound);
			break;
			
		case 2:
			GetComponent<AudioSource>().PlayOneShot(moodyFeelingSound);
			break;
			
		case 3:
			GetComponent<AudioSource>().PlayOneShot(angryFeelingSound);
			break;
		}
	}
}
