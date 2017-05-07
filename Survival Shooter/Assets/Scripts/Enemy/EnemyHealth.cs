using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	public AudioClip deathClip;
	public Gold gold;

	private GameObject player;
	private PlayerHealth playerHealth;
	private Animator anim;
	private AudioSource enemyAudio;
	private ParticleSystem hitParticles;
	private CapsuleCollider capsuleCollider;
	private bool isDead;
	private bool isSinking;

	void Awake() {
		player = GameObject.Find("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		capsuleCollider = GetComponent<CapsuleCollider>();

		currentHealth = startingHealth;

	}

	void Update() {
		if (isSinking) {
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage (int amount, Vector3 hitPoint) {
		if (isDead) {
			return;
		}

		enemyAudio.Play();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play();

		if (currentHealth <= 0) {
			Death();
		}
	}

	void Death() {
		isDead = true;

		// Drop money based on percentage
		int drop = Random.Range(1,100);
//		Debug.Log(drop);
//		Debug.Log("droprate " + playerHealth.getDropRate());
		if (drop <= playerHealth.getDropRate()) {
			Instantiate(gold, transform.position, transform.rotation);
		}

		capsuleCollider.isTrigger = true;
		anim.SetTrigger("Dead");
		enemyAudio.clip = deathClip;
		enemyAudio.Play();
	}

	public void StartSinking() {
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		isSinking = true;
		ScoreManager.score += scoreValue;
		Destroy(gameObject, 2f);
	}
}
