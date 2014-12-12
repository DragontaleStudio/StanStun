using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{
	public delegate void gameAction();
	public static event gameAction gameStart;
	public static event gameAction gameFinish;

	public delegate void stunAction();
	public static event stunAction gotStunned;
	public static event stunAction stunnEnemy;

//	public delegate void obstacleAction();
//	public static event obstacleAction totemonBreak;
//	public static event obstacleAction totemonJump;
//	public static event obstacleAction totemonPass;
//	public static event obstacleAction wrongObstacle;
//
//	public delegate void junctionAction(string nextPathToFollow, int nodeToFollow);
//	public static event junctionAction nextPathToFollow;
//
//	public delegate void collectAction();
//	public static event collectAction totemonCollect;

	public static void onGameStart()
	{
		if(gameStart != null)
		{	
			gameStart();
		}
	}
	
	public static void onGameFinish()
	{
		if(gameFinish != null)
		{	
			gameFinish();
		}
	}

	public static void onGotStunned()
	{
		if(gotStunned != null)
		{	
			gotStunned();
		}
	}

	public static void onStunnEnemy()
	{
		if(stunnEnemy != null)
		{	
			stunnEnemy();
		}
	}
}
