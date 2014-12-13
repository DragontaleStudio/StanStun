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

	void Awake()
	{
		model = new Player();
		transform.Find("RangeToAttack").GetComponent<SphereCollider>().radius = model.stunRange;

		GameObject.Find("UiManager").GetComponent<CtrlUiManager>().model = model;

		InputColorChange();
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
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
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
			syncStartPosition = rigidbody.position;
		}
	}

	private void InputMovement()
	{
		if (Input.GetKey(KeyCode.W))
			rigidbody.MovePosition(rigidbody.position + Vector3.forward * model.speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.S))
			rigidbody.MovePosition(rigidbody.position - Vector3.forward * model.speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.D))
			rigidbody.MovePosition(rigidbody.position + Vector3.right * model.speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.A))
			rigidbody.MovePosition(rigidbody.position - Vector3.right * model.speed * Time.deltaTime);
	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	private void InputColorChange()
	{
		ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
	}
	
	[RPC] void ChangeColorTo(Vector3 color)
	{
		renderer.material.color = new Color(color.x, color.y, color.z, 1f);
		
		if (networkView.isMine)
		{
			networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
		}
	}

	private bool isStunned()
	{
		return model.stunnedFor > 0;
	}

	private bool hasResources()
	{
		return model.carryResources > 0;
	}

	private bool canStun()
	{
		return !hasResources();
	}

	public void setStunned(int stunnedFor)
	{
		model.stunnedFor = stunnedFor;
		EventManager.onGotStunned("test");
	}
}
