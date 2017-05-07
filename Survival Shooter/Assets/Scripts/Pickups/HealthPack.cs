using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

	private int healAmount; 
	private int maxHealAmount;
	private GameObject player;
	private PlayerHealth playerHealth;

	void Awake() {
		player = GameObject.Find("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		maxHealAmount = 20;
		healAmount = maxHealAmount;
	}

	void OnTriggerEnter(Collider other) {
		// If player collides with healthpack
		if (other.tag == "Player") {
			PickupHealthPack();
		} 
	}

	void PickupHealthPack() {
		if (playerHealth.currentHealth < playerHealth.startingHealth) {
//			Debug.Log("Pickup HP");
			playerHealth.currentHealth += healAmount;
			playerHealth.healthSlider.value = playerHealth.currentHealth;
			Destroy(this.gameObject);
		}
	}

	public void setHealAmount(int value) {
		maxHealAmount = value;
		healAmount = maxHealAmount;
	}

	public void addHealAmount(int value) {
		maxHealAmount += value;
		healAmount = maxHealAmount;
		Debug.Log(healAmount);
	}

}
