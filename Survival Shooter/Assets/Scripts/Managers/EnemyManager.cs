using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public Transform core;
	public PlayerHealth playerHealth;
	public CoreHealth coreHealth;
	public GameObject enemy;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	public bool canSpawn;

	void Awake() {
		core = GameObject.Find("Core").transform;
		coreHealth = core.GetComponent<CoreHealth>();
		canSpawn = true;
	}

	void Start() {
		InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

	void Spawn() {
		if (playerHealth.currentHealth <= 0f || coreHealth.currentHP <= 0f) {
			return;
		}

		if (canSpawn) {
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);

			Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		}
	}
}
