using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GenLevelCellular : MonoBehaviour 
{
	[System.Serializable]
	public class CMap
	{
		public int[] sermap;
	}

	public GameObject[] floor;
	public GameObject[] wall;
	public GameObject[] treasure;
	public GameObject[] debris;
	public GameObject redPlayerBase;
	public GameObject bluePlayerBase;

	public GameObject textCoords;

	public Material floorMaterial;
//	public List<int[,]> maps=new List<int[,]>();
	
	public int[,] map;

	bool created=false;

	// Use this for initialization
	void Start () 
	{
	
	}

	public void newMapGenerated()
	{
//		changeMap(map);
//		changeMapStr("dssdds");

		byte[] mmm=ZipUtil.Zip(stringify());
		Debug.Log("map str lenght:"+mmm.Length);
		networkView.RPC("changeMapStr", RPCMode.OthersBuffered, mmm);
	}
	
//	[RPC] void changeMap(int[,] mapRef)
//	{
//		if (networkView.isMine)
//		{
//			networkView.RPC("changeMap", RPCMode.OthersBuffered, mapRef);
//		}
//	}

	[RPC] void changeMapStr(byte [] strbytes)
	{
		string mapRef=ZipUtil.Unzip(strbytes);
		if (Network.isClient)
		{
			destringify(mapRef);
			createLevel(false);
		}
	}

	
	private string stringify()
	{
		//Get a binary formatter
		BinaryFormatter b = new BinaryFormatter();
		
		//Create an in memory stream
		MemoryStream m = new MemoryStream();
		
		//Save the scores
		b.Serialize(m, map);
		
		// return the string
		return System.Convert.ToBase64String(m.GetBuffer());
	}

	private void destringify(string worldSerialisedString)
	{
		string data = worldSerialisedString;
		
		//If not blank then load it
		if(!System.String.IsNullOrEmpty(data))
		{
			//Binary formatter for loading back
			BinaryFormatter b = new BinaryFormatter();
			
			//Create a memory stream with the data
			MemoryStream m = new MemoryStream(System.Convert.FromBase64String(data));
			
			//Load back the scores
			map= b.Deserialize(m) as int[,];
		}
	}


		// Update is called once per frame
		void Update () 
		{
			if (!created && Network.isServer)
		{
			createLevel(true);
			created=true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			//createLevel();
		}
	}

	public void createLevel(bool generateNew)
	{
//		int childs = transform.childCount;
//		for (int i = 0; i > childs; i++	)
		{
//			GameObject theFloor=transform.GetChild(i).gameObject;
			GameObject theFloor=gameObject;

			if (generateNew)
			{
				CellularAutomata ca=new CellularAutomata(32,32);
			
				map= ca.generateMap();
				ca.simplePlaceTreasure(map,6);
				ca.zeroLimits(map);
				ca.placeBase(map);
				ca.simplePlaceObject(map,.01f);
			}


			
			for (int x = 0; x < map.GetLength(0); x++)
			{
				for (int y = 0; y < map.GetLength(1); y++)
				{
					GameObject go=null;
					if (map[x,y]==0) 
					{
//						go=(GameObject)GameObject.Instantiate(floor[Random.Range(0,floor.Length-1)]);
//						go.transform.Find("Cube").GetComponent<MeshFilter>().mesh.uv
//							mainTextureOffset.x=x/width;
//						go.transform.Find("Cube").renderer.material.mainTextureOffset.y=y/width;


//						if (x==0||y==0||x==31||y==31)
//						{
//							go=(GameObject)GameObject.Instantiate(textCoords);
//							go.transform.GetChild(0).GetComponent<TextMesh>().text=transform.gameObject.name.Substring(4)+":"+x+","+y;
//						}
					}
					else if (map[x,y]==1) 
					{
						go=(GameObject)GameObject.Instantiate(wall[Random.Range(0,wall.Length-1)]);
					}
					else if (map[x,y]==2) 
					{
//						go=(GameObject)GameObject.Instantiate(floor[Random.Range(0,floor.Length-1)]);
//						go.transform.parent=theFloor.transform;
//						go.transform.localPosition=new Vector3(x- map.GetLength(0)/2,0,y-map.GetLength(1)/2);
//						go.transform.localRotation=Quaternion.identity;

							go=(GameObject)GameObject.Instantiate(treasure[Random.Range(0,treasure.Length-1)]);
					}
					
					else if (map[x,y]==3) 
					{
						go=(GameObject)GameObject.Instantiate(floor[Random.Range(0,floor.Length-1)]);
						go.transform.parent=theFloor.transform;
						go.transform.localPosition=new Vector3(x- map.GetLength(0)/2,0,y-map.GetLength(1)/2);
						go.transform.localRotation=Quaternion.identity;

						if (debris!=null && debris.Length>0)
							go=(GameObject)GameObject.Instantiate(debris[Random.Range(0,debris.Length-1)]);
					}
					else if (map[x,y]==7) 
					{
						go=(GameObject)GameObject.Instantiate(int.Parse(transform.name.Substring(4))>3?bluePlayerBase:redPlayerBase);
					}

					if (go!=null)
					{
						go.transform.parent=theFloor.transform;
						go.transform.localPosition=new Vector3(x- map.GetLength(0)/2,0,y-map.GetLength(1)/2);
						go.transform.localRotation=Quaternion.identity;
						go.name="LVL"+x+","+y;
					}
				}
			}

		}


	}

}
