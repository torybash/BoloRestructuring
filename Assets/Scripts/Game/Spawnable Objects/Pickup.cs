using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Bolo.Events;

namespace Bolo
{
	public class Pickup : NetworkSpawnable
	{
		[SerializeField] private Rigidbody2D _body;

		//Make type TODO

		private int _resourceCount;


		public void Init()
		{
			//Set values
			_resourceCount = 5; //TODO

			//Set sprite
			//TODO

			//Add force
			_body.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
		}
	}
}