using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
	private const string typeName = "BurntDragonGame";
	private const string gameName = "Ampipolis";

	private HostData[] hostList;
	
	public GameObject playerPrefab;
	public GameObject button;
	public Canvas ui;

	int playerCount=1;

	private void StartServer()
	{
//		MasterServer.ipAddress = "127.0.0.1";
//		MasterServer.port = 23466;
//		Network.natFacilitatorIP = "127.0.0.1";
//		Network.natFacilitatorPort = 50005;

		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
		SpawnPlayer();
	}

	void OnPlayerConnected()
	{
		Debug.Log("Client connected");
		GameObject world=GameObject.Find("World");
		for (int i=0;i<world.transform.childCount;i++)
		{
			world.transform.GetChild(i).GetComponent<GenLevelCellular>().newMapGenerated();
		}
		playerCount++;
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList = MasterServer.PollHostList();
		}
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		SpawnPlayer();

	}

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
		}
	}

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);

		if (hostList != null)
		{
			for (int i = 0; i < hostList.Length; i++)
			{
				foreach (HostData d in hostList)
				{
					GameObject go = (GameObject)Instantiate(button);
					
					go.transform.SetParent(ui.transform);
					go.transform.localScale = new Vector3(1, 1, 1);
					Button b = go.GetComponent<Button>();
					b.onClick.AddListener(() => JoinServer(d));

					go.transform.Find("Text").GetComponent<Text>().text = d.gameName;
				}
			}
		}

	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	private void SpawnPlayer()
	{
		GameObject player = (GameObject) Network.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, 0);
		player.name = "Me"+playerCount;
		player.transform.parent=GameObject.Find("Face1").transform;
		int [,] map=GameObject.Find("Face1").GetComponent<GenLevelCellular>().map;
		if (map!=null)
		{
			int[] spawnPoint=CellularAutomata.getSpawnPoint(map);
			player.transform.localPosition=new Vector3(spawnPoint[0],0,spawnPoint[1]);
			Debug.Log("Spawned player at "+spawnPoint[0]+","+spawnPoint[1]);
		}
		else
		{
			player.transform.localPosition=Vector3.zero;
			Debug.Log("Spawned player at 0,0... map not found");
		}
		player.transform.localPosition=Vector3.zero;
		player.transform.eulerAngles=Vector3.zero;

	}
}
