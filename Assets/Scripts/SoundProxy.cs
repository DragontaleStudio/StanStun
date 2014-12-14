using UnityEngine;
using System.Collections;

public class SoundProxy : MonoBehaviour {

	public CtrlPlayerSoundManager sound;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void steps()
	{
		sound.playFootsteps();
	}
}
