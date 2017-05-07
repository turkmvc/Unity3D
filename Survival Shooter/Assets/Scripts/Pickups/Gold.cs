using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

	[SerializeField]
	private int amount;
	private GameObject player;
	private PlayerHealth playerHealth;
	private float despawnTimer;

	void Awake() {
		player = GameObject.Find("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		int rndAmount = Random.Range(3,13);
		amount = rndAmount;
		despawnTimer = 40f;
	}
		
	void OnTriggerEnter(Collider other) {
		// If player collides with gold
		if (other.gameObject == player) {
			PickupGold();
		} 
	}

	void PickupGold() {
		Destroy(this.gameObject);
		playerHealth.money += amount;;
	}

	void Update() {
		despawnTimer -= Time.deltaTime;
		if (despawnTimer <= 0) {
			Destroy(this.gameObject);
//			Debug.Log("Destroy gold");
		}
	}
		
}
