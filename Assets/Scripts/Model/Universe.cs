using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Universe 
{
	static Universe instance;

	Team[] teams;
	Level[] levels;
	
	public static Universe getInstance()
	{
		if(instance == null) 
		{
			loadUniverse();
		}
		
		return instance;
	}
	
	public Universe()
	{
		this.teams = new Team[2];
		
		teams[0] = new Team(1);
		teams[1] = new Team(2);
		
		this.levels = new Level[6];
		
		levels[0] = new Level(1);
		levels[1] = new Level(2);
		levels[2] = new Level(3);
		levels[3] = new Level(4);
		levels[4] = new Level(5);
		levels[5] = new Level(6);
	}
	
	public void saveUniverse()
	{
		PlayerPrefs.SetString("_state", stringify());
	}
	
	private static void loadUniverse()
	{
		instance = destringify(PlayerPrefs.GetString("_state"));
		
		if (instance == null)
		{
			instance = new Universe();
		}
	}
	
	private static string stringify()
	{
		//Get a binary formatter
		BinaryFormatter b = new BinaryFormatter();
		
		//Create an in memory stream
		MemoryStream m = new MemoryStream();
		
		//Save the scores
		b.Serialize(m, instance);
		
		// return the string
		return Convert.ToBase64String(m.GetBuffer());
	}
	
	private static Universe destringify(string worldSerialisedString)
	{
		string data = worldSerialisedString;
		
		//If not blank then load it
		if(!String.IsNullOrEmpty(data))
		{
			//Binary formatter for loading back
			BinaryFormatter b = new BinaryFormatter();
			
			//Create a memory stream with the data
			MemoryStream m = new MemoryStream(Convert.FromBase64String(data));
			
			//Load back the scores
			return b.Deserialize(m) as Universe;
		}
		else
		{
			return null;
		}
	}
}
