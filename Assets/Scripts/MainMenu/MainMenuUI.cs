using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;


public class MainMenuUI : MonoBehaviour {

	[SerializeField] InputField ipField;

	public void Click_HostGame()
	{
		NetworkManager.singleton.StartHost();
	}

	public void Click_JoinGame()
	{
		NetworkManager.singleton.networkAddress = ipField.text;
		NetworkManager.singleton.StartClient();
	}
}
