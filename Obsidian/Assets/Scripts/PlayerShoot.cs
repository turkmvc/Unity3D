using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	[SerializeField]
	private PlayerWeapon weapon;
	[SerializeField]
	private GameObject weaponGFX;
	[SerializeField]
	private string weaponLayerName = "Weapon";

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			Debug.Log("PlayerShoot: No camera referenced");
			this.enabled = false;
		}

		weaponGFX.layer = LayerMask.NameToLayer(weaponLayerName);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Shoot();
		}
	}

	// Raycast shoot
	// CHANGE TO SERVER, CLIENT = HACKERS
	[Client]
	void Shoot() {
		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask)) {
			if (_hit.collider.tag == PLAYER_TAG) {
				CmdPlayerShot(_hit.collider.name, weapon.damage);
			}
		}	
	}

	// Run on client
	[Command]
	void CmdPlayerShot(string _playerID, int _damage) {
		Debug.Log(_playerID + " has been shot.");
		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage(_damage);
	}
		    
}
