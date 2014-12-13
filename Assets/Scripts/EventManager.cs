using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{
	public delegate void gameAction();
	public static event gameAction gameStart;
	public static event gameAction gameFinish;

	public delegate void stunAction(string player);
	public static event stunAction gotStunned;
	public static event stunAction stunnEnemy;


	public delegate void stuffAction();
	public static event stuffAction stuffPickup;
	public static event stuffAction stuffFullyLoaded;
	public static event stuffAction stuffDropped;
	public static event stuffAction stuffDepositTobase;
	

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

	public static void onGotStunned(string player)
	{
		if(gotStunned != null)
		{	
			gotStunned(player);
		}
	}

	public static void onStunnEnemy(string player)
	{
		if(stunnEnemy != null)
		{	
			stunnEnemy(player);
		}
	}

	public static void onStuffPickup()
	{
		if(stuffPickup != null)
		{	
			stuffPickup();
		}
	}

	public static void onStuffDropped()
	{
		if(stuffDropped != null)
		{	
			stuffDropped();
		}
	}

	public static void onStuffFullyLoaded()
	{
		if(stuffFullyLoaded != null)
		{	
			stuffFullyLoaded();
		}
	}

	public static void onStuffDepositTobase()
	{
		if(stuffDepositTobase != null)
		{	
			stuffDepositTobase();
		}
	}
}
