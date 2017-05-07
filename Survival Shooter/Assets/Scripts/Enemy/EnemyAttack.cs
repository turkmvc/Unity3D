using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	public bool canAttackCore;

	private Animator anim;
	private GameObject player;
	private GameObject core;
	private CoreHealth coreHealth;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;
//	private EnemyManager enemyManager;
	private bool playerInRange;
	private bool coreInRange;
	private bool enemyAttackCore;
	private float timer;

	void Awake() {
		player = GameObject.Find("Player");
		core = GameObject.Find("Core");
		coreHealth = core.GetComponent<CoreHealth>();
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
//		enemyManager = GetComponent<EnemyManager>();
//		enemyAttackCore = GameObject.Find("EnemyManager").GetComponent<EnemyManager>().canAttackCore;
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			playerInRange = true;
		} else if (other.gameObject == core) {
			coreInRange = true;
		} else if (other.gameObject.tag == "DetectCore") {
			// Debug.Log("attack the core!!");
			canAttackCore = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			playerInRange = false;
		} else if (other.gameObject == core) {
			coreInRange = true;
		}
	}

	void Update() {
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 && !canAttackCore) {
			AttackPlayer();
		} 

		if (timer >= timeBetweenAttacks && coreInRange && enemyHealth.currentHealth > 0 && canAttackCore) {
			AttackCore();
		}

		if (playerHealth.currentHealth <= 0 || coreHealth.currentHP <= 0) {
			anim.SetTrigger("PlayerDead");
		}
	}

	void AttackPlayer() {
		timer = 0f;

		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage(attackDamage);
		}
	}

	void AttackCore() {
		timer = 0f;

		if (coreHealth.currentHP > 0) {
			coreHealth.TakeDamage(attackDamage);
		}
	}

}
