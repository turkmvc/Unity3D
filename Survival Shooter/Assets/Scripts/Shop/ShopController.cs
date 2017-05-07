using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

	public GameObject HUD;
	public GameObject shopPanel;
	public GameObject gunCanvas;
	public GameObject upgradesCanvas;
	public GameObject skillsCanvas;
	public PlayerHealth playerHealth;
	public Text moneyText;

	void Awake() {
//		playerHealth = GetComponent<PlayerHealth>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			OpenShop();
		}
	}

	void OpenShop() {
//		Debug.Log("Open Shop");
		HUD.SetActive(false);
		shopPanel.SetActive(true);

		// Defualt tab - Guns
		DisableTabs();
		gunCanvas.SetActive(true);

		// Pause Game
		Time.timeScale = 0;
	}
		
	public void CloseShop() {
//		Debug.Log("Close Shop");
		HUD.SetActive(true);
		shopPanel.SetActive(false);
		gunCanvas.SetActive(false);

		// Resume Game
		Time.timeScale = 1;
	}

	public void GunsTab() {
		DisableTabs();
		gunCanvas.SetActive(true);
	}

	public void UpgradesTab() {
		DisableTabs();	
		upgradesCanvas.SetActive(true);
	}

	public void SkillsTab() {
		DisableTabs();
		skillsCanvas.SetActive(true);
	}

	void DisableTabs() {
		gunCanvas.SetActive(false);
		upgradesCanvas.SetActive(false);
		skillsCanvas.SetActive(false);
	}

	void Update() {
		if (shopPanel.activeSelf) {
			moneyText.text = "Money: $" + playerHealth.money;
		}
	}

}
