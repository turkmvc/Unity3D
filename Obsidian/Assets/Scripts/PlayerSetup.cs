using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup: NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "Remote Player";
		
	[SerializeField]
	GameObject playerUIPrefab;
	private GameObject playerUIInstance;

	Camera sceneCamera;

	// Use this for initialization
	void Start() {
		if (!isLocalPlayer) {
			DisableComponents();
			AssignRemoteLayer();
		} else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				// Debug.Log("sceneCamera found");
				// Disable scene camera
				sceneCamera.gameObject.SetActive(false);
			}

			// Create PlayerUI
			playerUIInstance = Instantiate(playerUIPrefab);
			playerUIInstance.name = playerUIPrefab.name;
		}

		GetComponent<Player>().Setup();
	}

	// Called when client is set up locally
	public override void OnStartClient() {
		base.OnStartClient();	

		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		Player _player = GetComponent<Player>();

		GameManager.RegisterPlayer(_netID, _player);
	}

	void AssignRemoteLayer() {
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void DisableComponents() {
		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable[i].enabled = false;
		}
	}

	// When destroyed
	void OnDisable() {

		Destroy(playerUIInstance);

		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive(true);
		}

		GameManager.UnregisterPlayer(transform.name);
	}

}
