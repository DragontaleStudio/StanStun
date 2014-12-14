using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class CtrlMusicManager : MonoBehaviour {

	public AudioClip intro;
	public AudioClip mainLoop;
	// Use this for initialization
	void Start () 
	{
		playMusic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void playMusic()
	{
//		musicBank.changeVolume(0.5);
		audio.PlayOneShot(intro);
		StartCoroutine("playLoop");	
	}

	IEnumerator playLoop() 
	{
		yield return new WaitForSeconds(intro.length);
		audio.clip = mainLoop;
		audio.loop = true;
		audio.Play();
	}
}
