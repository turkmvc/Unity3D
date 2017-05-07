using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private GameObject player;
	private GameObject gunBarrelEnd;
	private PlayerShooting playerShooting;
	private PlayerHealth playerHealth;
	private Rigidbody rb;

	private int piercing;
	private List<EnemyHealth> enemies;


	[SerializeField]
	private float bulletForce = 1000f;

	// Use this for initialization
	void Awake() {
		player = GameObject.Find("Player");
		gunBarrelEnd = GameObject.Find("GunBarrelEnd");
		playerShooting = gunBarrelEnd.GetComponent<PlayerShooting>();
		playerHealth = player.GetComponent<PlayerHealth>();
		rb = GetComponent<Rigidbody>();
		piercing = playerShooting.weapons[playerShooting.currentWeapon].piercing; 
		enemies = new List<EnemyHealth>(piercing);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
			enemies.Add(enemyHealth);
			if (enemyHealth != null) {
				// Remove forces
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero; 
				// Damage enemy
				int damage = playerShooting.weapons[playerShooting.currentWeapon].damage + playerHealth.baseDamage;
				// print(damage);
				enemyHealth.TakeDamage(damage, other.transform.position);
			}
		}
		if (enemies.Count >= piercing) {
//			print(enemies.Count); // Pierces [piercing, piercing + 1]
			Destroy(this.gameObject);
		}
	}
		
	// Update is called once per frame
	void Update() {
		rb.AddForce(transform.forward * bulletForce);

//		if (rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero) {
//			Destroy(this.gameObject);
//		}
	}
}
