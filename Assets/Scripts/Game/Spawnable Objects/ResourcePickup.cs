using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Events;
using Bolo.DataClasses;

namespace Bolo
{
	public class ResourcePickup : NetworkSpawnable
	{
		[SerializeField] private Rigidbody2D _body;

		private SpawnPool<ResourcePickup> _spawnPool;

		[SyncVar] private ResourceType _type;
		[SyncVar] private int _resourceCount;


		public override void SetPool<T>(SpawnPool<T> pool) 
		{
			_spawnPool = pool as SpawnPool<ResourcePickup>;
		}

		public void Init(ResourceData resource)
		{
			_type = resource.type;
			_resourceCount = resource.resourceCount;

			var force = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 4f;  //TODO: make constants somewhere?		
			_body.AddForce(force, ForceMode2D.Impulse);	
			//RpcInit(force, resource.type, resource.resourceCount);
		}


		//[ClientRpc]
		//private void RpcInit(Vector2 force, ResourceType type, int resourceCount)
		//{
		//	//_type = type;
		//	//_resourceCount = resourceCount;

		//	//Set sprite
		//	//TODO

		//	_body.AddForce(force, ForceMode2D.Impulse);	
		//}

		[ServerCallback]
		private void OnTriggerEnter2D(Collider2D other)
		{
			//D.Log("OnTriggerEnter2D - other: " + other);

			//TODO
			if (other.CompareTag("Player"))
			{

				var vehicle = other.GetComponent<PlayerVehicle>();
				vehicle.CmdPickUpResource(_type, _resourceCount);
				_spawnPool.Destroy(this);
			}			
		}
	}
}