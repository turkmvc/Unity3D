using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoreHealth : MonoBehaviour {

	public int startingHP;
	public int currentHP;
	public Slider HPSlider;
	public AudioClip deathClip;

	// Replace with Core death audio and animation
//	private Animator anim;
	private AudioSource coreAudio;
//	private PlayerMovement playerMovement;
//	private PlayerShooting playerShooting;
	private bool isDead;

	void Awake() {
//		anim = GetComponent<Animator>();
		coreAudio = GetComponent<AudioSource>();
//		playerMovement = GetComponent<PlayerMovement>();
//		playerShooting = GetComponent<PlayerShooting>();
		startingHP = 200;
		currentHP = startingHP;
		HPSlider.maxValue = currentHP;
		HPSlider.value = currentHP;
	}

	public void TakeDamage(int amount) {
		currentHP -= amount;

		HPSlider.value = currentHP;

		if (currentHP <= 0 && !isDead) {
			CoreDeath();
		}
	}

	void CoreDeath() {
		isDead = true;
		RestartLevel();
	}

	public void RestartLevel() {
		SceneManager.LoadScene("Shooter");
	}

}
