using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

	public PlayerHealth playerHealth;
	public PlayerMovement playerMovement;
	public CoreHealth coreHealth;
	public HealthPack healthPack;
	public int upgradeNumber;

	public Text name;
	public Text cost;
	public Text description;
	public Text level;
	public Text value;

	private AudioSource source;

	public List<UpgradeObject> upgrades;

	// Use this for initialization
	void Start () {
		SetupUpgrades();
		source = GetComponent<AudioSource>();
		SetButton();
	}

	void SetupUpgrades() {
		upgrades = new List<UpgradeObject>(4);

		UpgradeObject health = new UpgradeObject("Health", 50, "Gain additional HP", 1, 25);
		UpgradeObject coreHealth = new UpgradeObject("Core Health", 50, "Gain additional Core HP", 1, 25);
		UpgradeObject damage = new UpgradeObject("Damage", 50, "Increase your damage", 1, 5);
		UpgradeObject speed = new UpgradeObject("Speed", 50, "Run faster", 1, 0.2f);
		UpgradeObject goldDrop = new UpgradeObject("Gold Rate", 50, "Increase gold drop", 1, 5);
		UpgradeObject healthPack = new UpgradeObject("Health Pack", 50, "Increase health pack HP", 1, 20);

		upgrades.Add(health);
		upgrades.Add(coreHealth);
		upgrades.Add(damage);
		upgrades.Add(speed);
		upgrades.Add(goldDrop);
		upgrades.Add(healthPack);

		//		Debug.Log(upgrades);
	}

	void SetButton() {
		name.text = upgrades[upgradeNumber].getName();
		cost.text = "$" + upgrades[upgradeNumber].getCost();
		description.text = upgrades[upgradeNumber].getDescription();
		level.text = "Level " + upgrades[upgradeNumber].getLevel().ToString();
		value.text = "+ " + upgrades[upgradeNumber].getValue().ToString();
	}
	
	public void OnClick() {
		if (playerHealth.money >= upgrades[upgradeNumber].getCost()) {

			if (upgradeNumber == 0) { // Player Health
//				Debug.Log("PlayerHP");
				Buy(1.2f);
				playerHealth.startingHealth += (int)upgrades[upgradeNumber].getValue();
				playerHealth.healthSlider.maxValue = playerHealth.startingHealth;
				upgrades[upgradeNumber].setValue((int)(upgrades[upgradeNumber].getValue() * 1.25));
			} else if (upgradeNumber == 1) { // Core Health
//				Debug.Log("CoreHP");
				Buy(1.2f);
				coreHealth.startingHP += (int)upgrades[upgradeNumber].getValue();
				coreHealth.HPSlider.maxValue = coreHealth.startingHP;
				upgrades[upgradeNumber].setValue((int)(upgrades[upgradeNumber].getValue() * 1.25));
			} else if (upgradeNumber == 2) { // Damage
				Buy(1.5f);
				playerHealth.baseDamage += (int)upgrades[upgradeNumber].getValue();
				upgrades[upgradeNumber].setValue((int)(upgrades[upgradeNumber].getValue() * 1.2));
			} else if (upgradeNumber == 3) { // Speed
//				Debug.Log("Speed");
				if (playerMovement.speed <= 10) {
					Buy(1.5f);
					playerMovement.speed += upgrades[upgradeNumber].getValue();
					upgrades[upgradeNumber].addValue(0.1f); 
				}
			} else if (upgradeNumber == 4) { // Gold
//				Debug.Log("Gold");
				if (playerHealth.getDropRate() < 100) {
//					Debug.Log("Bought gold rate");
					Buy(1.3f);
					playerHealth.addDropRate(upgrades[upgradeNumber].getValue());
				} 
			} else if (upgradeNumber == 5) { // Health Pack
//				Debug.Log("Health Pack");
				Buy(1.4f);
				healthPack.addHealAmount(20);
			}

		} else {
			source.Play();
//			Debug.Log("You can't afford this!");
		}
	}	

	void Buy(float costMultiplier) {
		double newCost;
		playerHealth.money -= upgrades[upgradeNumber].getCost();
		upgrades[upgradeNumber].addLevel();
		newCost = upgrades[upgradeNumber].getCost() * costMultiplier;
		upgrades[upgradeNumber].setCost((int)newCost);
	}

	void Update () {
		cost.text = "$" + upgrades[upgradeNumber].getCost();
		level.text = "Level " + upgrades[upgradeNumber].getLevel().ToString();
		value.text = "+ " + upgrades[upgradeNumber].getValue().ToString();

		// Changes all value.text
//		if (playerMovement.speed >= 7 || playerHealth.getDropRate() >= 100) {
//			upgrades[upgradeNumber].valu
//		}
	}
		

}
