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

	public AudioClip[] deposit;

	void OnEnable()
	{
		EventManager.stuffPickup += playCollect;
		EventManager.gotStunned += playBirds;
		EventManager.stunnEnemy += playRangedStun;
		EventManager.stunnEnemy += playLaugh;
	}
	
	void OnDisable()
	{
		EventManager.stuffPickup -= playCollect;
		EventManager.gotStunned -= playBirds;
		EventManager.stunnEnemy -= playRangedStun;
		EventManager.stunnEnemy -= playLaugh;
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

	void playLaugh(string enemy)
	{
		GetComponent<AudioSource>().PlayOneShot(laugh[Random.Range(0, laugh.Length-1)]);
	}

	public void playDeposit(int sound)
	{
		sound--;

		if (sound>5)
		{
			sound=5;
		}

		GetComponent<AudioSource>().PlayOneShot(deposit[sound]);
	}

}
