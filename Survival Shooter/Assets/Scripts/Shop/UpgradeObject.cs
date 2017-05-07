using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObject : MonoBehaviour {

	private string upgradeName;
	private int cost;
	private string description;
	private int level;
	private float value;

	public UpgradeObject(string setUpgradeName, int setCost, string setDescription, 
	                     int setLevel, float setValue) {
		upgradeName = setUpgradeName;
		cost = setCost;
		description = setDescription;
		level = setLevel;
		value = setValue;
	}

	// Get
	public string getName() {
		return upgradeName;
	}

	public int getCost() {
		return cost;
	}

	public string getDescription() {
		return description;
	}

	public int getLevel() {
		return level;
	}

	public float getValue() {
		return value;
	}


	// Set
	public int setCost(int newCost) {
		cost = newCost;
		return cost;
	}

	public int setLevel(int newLevel) {
		level = newLevel;
		return level;
	}

	public float setValue(float newValue) {
		value = newValue;
		return value;
	}

	public void addLevel() {
		level++;
	}

	public void addValue(float val) {
		value += val;
	}
}