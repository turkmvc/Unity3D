using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour {

	public PlayerShooting playerShooting;
	public PlayerHealth playerHealth;
	public int weaponNumber;
	public List<int> unlockedWeapons;

	public Text name;
	public Text cost;
	public Text description;
	public Text unlocked;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		unlockedWeapons.Add(0);
		SetButton();
	}

	void SetButton() {
		name.text = playerShooting.weapons[weaponNumber].name;
		cost.text = "$" + playerShooting.weapons[weaponNumber].cost;
		description.text = playerShooting.weapons[weaponNumber].description;
		unlocked.text = "Locked";
	}

	public void OnClick() {
		if (!unlockedWeapons.Contains(weaponNumber)) {
			if (playerHealth.money >= playerShooting.weapons[weaponNumber].cost) {
				playerHealth.money -= playerShooting.weapons[weaponNumber].cost;
				playerShooting.currentWeapon = weaponNumber;
				unlockedWeapons.Add(weaponNumber);
//				Debug.Log("Bought " + playerShooting.weapons[weaponNumber].name);
			} else {
				source.Play();
//				Debug.Log("You cannot afford " + playerShooting.weapons[weaponNumber].name);
			}
		} else {
			playerShooting.currentWeapon = weaponNumber;
//			Debug.Log("Switch Weapon");
		}
	}
		
	void Update() {
		if (weaponNumber == playerShooting.currentWeapon) {
			unlocked.text = "Equipped";
//			Debug.Log(playerShooting.weapons[weaponNumber].name + " Equipped");
		} else if (unlockedWeapons.Contains(weaponNumber)) {
			unlocked.text = "Unlocked";
//			Debug.Log(playerShooting.weapons[weaponNumber].name + " Unlocked");
		}
	}

}
