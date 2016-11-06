using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Events;
using Bolo.DataClasses;

namespace Bolo
{
	public class ResourcePickup : NetworkSpawnable
	{
		[SerializeField]
		private Rigidbody2D _body;

		[SerializeField]
		private float forcePushPower = 4f;

		[SyncVar]
		private ResourceType _type;
		[SyncVar]
		private int _resourceCount;

		private SpawnPool<ResourcePickup> _spawnPool;

		public override void SetPool<T>(SpawnPool<T> pool) 
		{
			_spawnPool = pool as SpawnPool<ResourcePickup>;
		}

		public void Init(ResourceData resource)
		{
			_type = resource.type;
			_resourceCount = resource.resourceCount;

			var force = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * forcePushPower; 
			_body.AddForce(force, ForceMode2D.Impulse);	
		}


		[ServerCallback]
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				var vehicle = other.GetComponent<PlayerVehicle>();
				vehicle.CmdPickUpResource(_type, _resourceCount);
				_spawnPool.Destroy(this);
			}			
		}
	}
}