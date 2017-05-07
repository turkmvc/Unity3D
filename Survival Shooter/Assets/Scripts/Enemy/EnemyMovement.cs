using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	private Transform player;
	private Transform core;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;
	private EnemyAttack enemyAttack;
	private CoreHealth coreHealth;
//	private EnemyManager enemyManager;
	private NavMeshAgent nav;

	void Awake() {
		player = GameObject.Find("Player").transform;
		core = GameObject.Find("Core").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
		enemyAttack = GetComponent<EnemyAttack>();
		coreHealth = core.GetComponent<CoreHealth>();
		nav = GetComponent<NavMeshAgent>();
	}

	void Update() {
		if (enemyHealth.currentHealth > 0 && coreHealth.currentHP > 0 && enemyAttack.canAttackCore) {
			nav.SetDestination(core.position);
		} else if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			nav.SetDestination(player.position);
		} else {
			nav.enabled = false;
		}
	}
}
