using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	public float range = 100f;

	private float timer;

//	private Ray shootRay;
//	private RaycastHit shootHit;
	private int shootableMask;
//	private RaycastHit[] hits;

	private ParticleSystem gunParticles;
//	private LineRenderer gunLine;
	private AudioSource gunAudio;
	private Light gunLight;
	private float effectsDisplayTime = 0.2f;

	// Bullet
	public GameObject _bullet;

	// Weapons
	public WeaponObject[] weapons;
	public int currentWeapon = 0;

	private List<EnemyHealth> enemiesPierced;

	void Awake() {
		shootableMask = LayerMask.GetMask("Shootable");
		gunParticles = GetComponent<ParticleSystem>();
//		gunLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponent<Light>();

	}

	void Update() {
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= weapons[currentWeapon].fireRate && Time.timeScale != 0) {
			Shoot();
		}

		if (timer >= weapons[currentWeapon].fireRate * effectsDisplayTime) {
			DisableEffects();
		}
	}

	public void DisableEffects() {
//		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	void Shoot() {
			
		timer = 0f;

		gunAudio.Play();

		gunLight.enabled = true;

		gunParticles.Stop();
		gunParticles.Play();

//		gunLine.enabled = true;
//		gunLine.SetPosition(0, transform.position);

//		shootRay.origin = transform.position;
//		shootRay.direction = transform.forward;

		// Bullet
		GameObject bullet = (GameObject)Instantiate(_bullet, transform.position, transform.rotation);


		// Shoot raycast
//		if (Physics.Raycast(shootRay, out shootHit, weapons[currentWeapon].range, shootableMask)) {
//			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
////			enemiesPierced.Add(enemyHealth);
//
////			for (int enemy = 0; enemy < enemiesPierced.Count; enemy++) {
//			
//				if (enemyHealth != null) {
//					enemyHealth.TakeDamage(weapons[currentWeapon].damage, shootHit.point);
//				}
//
////			}
//
//////			int lastEnemy = enemiesPierced.LastIndexOf();
//////			enemiesPierced[lastEnemy]
//	
//			gunLine.SetPosition(1, shootHit.point);

			

//		// PROBLEMS: You can shoot through enviornment, for loop too slow for smg, and gunline does not go through multiple people
//		// Add enemies to list
//		enemiesPierced = new List<EnemyHealth>(weapons[currentWeapon].piercing);
//
//		hits = Physics.RaycastAll(shootRay, weapons[currentWeapon].range, shootableMask);
//		for (int i = 0; i < hits.Length; i++) {
//			
//			Debug.Log(hits[i].collider.tag);
//			if (hits[i].collider.tag == "Enemy") {
//				if (enemiesPierced.Count < weapons[currentWeapon].piercing) {
//					enemiesPierced.Add(hits[i].collider.GetComponent<EnemyHealth>());
////					gunLine.SetPosition(1, hits[i].point);
//					for (int n = 1; n < weapons[currentWeapon].piercing; n++) {
//						gunLine.SetPosition(n, hits[i].point);
//					}
////					shootHit.point = hits[i].collider.transform.position;
//					Debug.DrawLine(hits[i].point, hits[i].point + Vector3.up * 15, Color.green);
//				}
//			} else {
//				gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weapons[currentWeapon].range);
//				Debug.DrawLine(hits[i].point, hits[i].point + Vector3.up * 15, Color.red);
//			}
//
//		}
//
//		// Deal damage to each enemy in list
//		for (int i = 0; i < enemiesPierced.Count; i++) {
//			enemiesPierced[i].TakeDamage(weapons[currentWeapon].damage, shootHit.point);
//		}
			
//		} else {
//			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weapons[currentWeapon].range);
//		}
	}

}
