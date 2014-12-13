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
		GameObject player = (GameObject) Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
		player.name = "Me";
		player.transform.parent=GameObject.Find("Face1").transform;
		player.transform.eulerAngles=Vector3.zero;
		player.transform.localPosition=Vector3.zero;

	}
}
