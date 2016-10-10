using UnityEngine;
using System.Collections;


public class Game : SingletonBehaviour<Game>
{

	//Fields
	[Header("Controllers")]
	[SerializeField]
	private CameraController _cam;
	[SerializeField]
	private GameUIController _ui;
	[SerializeField]
	private MapController _map;

	[Header("Asset References")]
	[SerializeField]
	private PrefabLibrary _prefabsLib;

	private NetPlayerController _player;
	private EventManager _events = new EventManager();

	//Properties
	public static CameraController cam { get { return I._cam; } }
	public static GameUIController ui { get { return I._ui; } }
	public static MapController map { get { return I._map; } }

	public static EventManager events { get { if (!I) return new EventManager(); return I._events; } } //TODO!!!



	public static void Init(NetPlayerController player)
	{
		//Set ref to network player
		I._player = player;

		//Setup/init other stuff

		//Init stuff
		ui.Init();
	}


}
