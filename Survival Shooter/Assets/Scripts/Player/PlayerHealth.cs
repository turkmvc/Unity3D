using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth;
	public int currentHealth;
	public int baseDamage;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
	public Text moneyText;

	// money
	public int money;
	private float dropRate;

	private Animator anim;
	private AudioSource playerAudio;
	private PlayerMovement playerMovement;
	private PlayerShooting playerShooting;
	private bool isDead;
	private bool damaged;


	void Awake() {
		anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();
		playerShooting = GetComponentInChildren<PlayerShooting>();
		startingHealth = 100;
		currentHealth = startingHealth;
		healthSlider.maxValue = currentHealth;
		healthSlider.value = currentHealth;
		baseDamage = 5;
		// Money
		money = 9999;
		dropRate = 40;
	}

	void Update() {
		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		moneyText.text = "Money: $" + money;
	}

	public void TakeDamage(int amount) {
		damaged = true;
		currentHealth -= amount;

		healthSlider.value = currentHealth;
		playerAudio.Play();

		if (currentHealth <= 0 && !isDead) {
			Death();
		}
	}

	void Death() {
		isDead = true;

		playerShooting.DisableEffects();

		anim.SetTrigger("Die");
		playerAudio.clip = deathClip;
		playerAudio.Play();

		playerMovement.enabled = false;
		playerShooting.enabled = false;
	}

	public void RestartLevel() {
		SceneManager.LoadScene("Shooter");
	}


	// Get
	public float getDropRate() {
		return dropRate;
	}

	// Set
	public void addDropRate(float val) {
		dropRate += val;
	}

}
