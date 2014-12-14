using UnityEngine;
using System.Collections;

public class CtrlPlayer : MonoBehaviour 
{
	public Player model;

	//Sync values
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	public int speed=1;
	public int turnSpeed=1;
	public float damping=0.5f;

	TheCube cube;
	GameObject daPlayer;
	public Vector3 curSpeed=Vector3.zero;

	void Awake()
	{
		model = new Player();
		transform.Find("RangeToAttack").GetComponent<SphereCollider>().radius = model.stunRange;

		if (networkView.isMine)
		{
			GameObject.Find("UiManager").GetComponent<CtrlUiManager>().model = model;
			GameObject.Find("CameraMan").GetComponent<CameraRail>().target=this.gameObject;
		}

		InputColorChange();
	}

	void Start()
	{
		cube=new TheCube();
		daPlayer=gameObject;
	}


	void Update()
	{
		if (networkView.isMine && !isStunned())
		{
			InputMovement();
		}
		else
		{
			SyncedMovement();
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;

		if (stream.isWriting)
		{
			syncPosition = transform.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = curSpeed;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = transform.position;
		}
	}

	private void InputMovement()
	{
		Vector3 vec=transform.localPosition;
		bool moved=false;
		if (Input.GetKey(KeyCode.W))
		{
			//			transform.Translate(0,0,speed*Time.deltaTime);
			//			transform.Translate(Vector3.forward*speed*Time.deltaTime);
			curSpeed.z=speed;
			moved=true;
		}
		
		if (Input.GetKey(KeyCode.S))
		{
			//			transform.Translate(0,0,-speed*Time.deltaTime);
			//			transform.Translate(Vector3.back*speed*Time.deltaTime);
			curSpeed.z=-speed;
			moved=true;
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			//			transform.Translate(-speed*Time.deltaTime,0,0);
			//			transform.Translate(Vector3.left*speed*Time.deltaTime);
			curSpeed.x=-speed;
			moved=true;
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			//			transform.Translate(speed*Time.deltaTime,0,0);
			//			transform.Translate(Vector3.right*speed*Time.deltaTime);
			curSpeed.x=speed;
			moved=true;
		}
		
		curSpeed=curSpeed*damping;
		transform.Translate(curSpeed*Time.deltaTime);
		
		
		//		if (moved)
		{
			int[] mapPos=getMapPos();
			TheCube.Direction dir= cube.getOverflowDirection(mapPos);
			if (dir!=TheCube.Direction.ANY_FUCKIN_WHERE)
			{
				Debug.Log("Overflowed!!!");
				int targetSide=cube.getNeighbour( dir);
				Debug.Log("Overflowed to "+dir+ " to target side "+targetSide );
				GameObject theFloor=GameObject.Find("Face"+targetSide);
				GameObject curFloor=gameObject.transform.parent.gameObject;

				daPlayer.transform.parent=theFloor.transform;
				Camera.main.transform.parent.parent=theFloor.transform;
				Camera.main.transform.parent.localRotation=Quaternion.identity;
				Camera.main.transform.localRotation=curFloor.transform.localRotation;
				daPlayer.transform.localRotation=Quaternion.identity;
				
				
				cube.currentSide=targetSide;
				
				int[] overflowedClampedPos=cube.getOverFlowClampedPos(mapPos);
				Debug.Log("ClampedPos:"+overflowedClampedPos[0]+","+overflowedClampedPos[1]);
				transform.localPosition=new Vector3(overflowedClampedPos[0]-16,0,overflowedClampedPos[1]-16);
				
				mapPos=getMapPos();
			}
			
			int xCoord=mapPos[0];
			int yCoord=mapPos[1];
			//			Debug.Log("Player map coord:" + xCoord+","+yCoord);

			if ( getMap()!=null && getMap()[xCoord,yCoord]==1)
			{
				transform.localPosition=vec;
			}
		}
	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	private void InputColorChange()
	{
		ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
	}
	
	[RPC] void ChangeColorTo(Vector3 color)
	{
//		renderer.material.color = new Color(color.x, color.y, color.z, 1f);
//		
//		if (networkView.isMine)
//		{
//			networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
//		}
	}

	public bool hasResources()
	{
		return model.carryResources > 0;
	}

	public bool canCollect()
	{
		return model.carryResources < model.maxCarryResources;
	}

	public bool isStunned()
	{
		return model.stunnedFor > 0;
	}

	public bool canStun()
	{
		return !hasResources();
	}

	public void setStunned(int stunnedFor)
	{
		model.stunnedFor = stunnedFor;
		EventManager.onGotStunned("test");
	}

	public void onStuffPickup()
	{
		model.carryResources++;
		EventManager.onStuffPickup();

		//full loaded
		if (!canCollect())
		{
			EventManager.onStuffFullyLoaded();
		}
	}

	public void onStuffDepositTobase()
	{
		model.carryResources = 0;
		EventManager.onStuffDepositTobase();
	}


	public int[,] getMap()
	{
		return gameObject.transform.parent.GetComponent<GenLevelCellular>().map;
	}
	
	public int[] getMapPos()
	{
		int xCoord=Mathf.CeilToInt(transform.localPosition.x)+16;
		int yCoord=Mathf.CeilToInt(transform.localPosition.z)+16;
		
		return new int[]{xCoord,yCoord};
	}



}
