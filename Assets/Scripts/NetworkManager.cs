using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	private const string typeName = "BurntDragonGame";
	private const string gameName = "Ampipolis";

	private HostData[] hostList;
	
	public GameObject playerPrefab;

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
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	private void SpawnPlayer()
	{
		GameObject Player = (GameObject) Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
		Player.name = "Me";
	}
}
