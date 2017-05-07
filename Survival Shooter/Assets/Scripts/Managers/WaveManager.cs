using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public int wave;
	public Text waveText;
	public HealthPack healthPack;

	private GameObject enemyManagerObject;
	private Component[] enemyManagerComponents;
	private float waveTimer;
	private float maxWaveTimer;
	private List<float> waveMultipliers;
	private bool waveOver;
	private GameObject[] enemyList;
	private GameObject[] HPPads;

	void Awake() {
		wave = 0;
		maxWaveTimer = 20f;
		waveTimer = maxWaveTimer;

		waveMultipliers = new List<float>(3);
		waveMultipliers.Add(1.2f);
		waveMultipliers.Add(1.3f);
		waveMultipliers.Add(1.4f);

		enemyManagerObject = GameObject.Find("EnemyManager");
		enemyManagerComponents = enemyManagerObject.GetComponents<EnemyManager>();

		HPPads = GameObject.FindGameObjectsWithTag("HPPad");

		StartWave();
	}

	// Update is called once per frame
	void Update() {
		waveTimer -= Time.deltaTime;
		enemyList = GameObject.FindGameObjectsWithTag("Enemy");
//		Debug.Log(enemyList.Length);
		if (waveTimer <= 0 && !waveOver) {
			EndWave();
		}

		if (enemyList.Length == 0 && waveOver) {
			waveText.fontSize = 30;
			waveText.text = "Press 'G' to start next wave";
		}

		if (waveOver && Input.GetKeyDown(KeyCode.G) && enemyList.Length == 0) {
			StartWave();
		}

	}

	// Enable spawning
	void StartWave() {
		foreach (EnemyManager component in enemyManagerComponents) {
			component.canSpawn = true;
		}

		// Health Pack
		foreach (GameObject HPPad in HPPads) {
			Instantiate(healthPack, HPPad.transform.position, HPPad.transform.rotation);
		}

		wave++;
		waveText.fontSize = 50;
		waveText.text = "Wave " + wave;
		waveOver = false;

		int multiplierPos = Random.Range(0,3);
		maxWaveTimer *= waveMultipliers[multiplierPos];
		waveTimer = maxWaveTimer;
//		Debug.Log(maxWaveTimer);
//		Debug.Log("Wave " + wave + " is starting");

	}

	// Disable spawning
	void EndWave() {
		foreach (EnemyManager component in enemyManagerComponents) {
			component.canSpawn = false;
		}
		waveOver = true;
//		Debug.Log("Wave " + wave + " is over");
	}

}
