using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class CtrlPlayerSoundManager : MonoBehaviour 
{
	public AudioClip[] footsteps;
	public AudioClip rangedStun;
	public AudioClip closeStun;
	public AudioClip wallHit;
	public AudioClip collect;
	public AudioClip[] laugh;
	public AudioClip birds;

	void OnEnable()
	{
		EventManager.stuffPickup += playCollect;
		EventManager.gotStunned += playBirds;
		EventManager.stunnEnemy += playRangedStun;
	}
	
	void OnDisable()
	{
		EventManager.stuffPickup -= playCollect;
		EventManager.gotStunned -= playBirds;
		EventManager.stunnEnemy -= playRangedStun;
	}

	void playRangedStun(string enemy)
	{
		GetComponent<AudioSource>().PlayOneShot(closeStun);
	}

	void playCloseStun()
	{
		GetComponent<AudioSource>().PlayOneShot(rangedStun);
	}

	void playWallHit()
	{
		GetComponent<AudioSource>().PlayOneShot(wallHit);
	}

	void playCollect()
	{
		GetComponent<AudioSource>().PlayOneShot(collect);
	}

	public void playFootsteps()
	{
		GetComponent<AudioSource>().PlayOneShot(footsteps[Random.Range(0, (footsteps.Length-1))]);
	}

	void playBirds(string enemy)
	{
		GetComponent<AudioSource>().PlayOneShot(birds);
	}
}
