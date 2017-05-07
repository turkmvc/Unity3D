using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {

	public int playerDamage;

	private Animator animator;
	// Store player's position
	private Transform target;
	private bool skipMove;

	// Audio
	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;

	// Use this for initialization
	protected override void Start () {
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}

	protected override void AttemptMove <T> (int xDir, int yDir) {
		if (skipMove) {
			skipMove = false;
			return;
		}
		base.AttemptMove<T> (xDir, yDir);
		skipMove = true;
	}

	public void MoveEnemy() {
		int xDir = 0;
		int yDir = 0;

		// If enemy and player are in the same column
		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon) {
			// If enemy is above the player, then move down, else move up
			yDir = target.position.y > transform.position.y ? 1 : -1;
		} else {
			// If ebeny is to the right of the player, then move right, else move left
			xDir = target.position.x > transform.position.x ? 1 : -1;
		}
		AttemptMove<Player> (xDir, yDir);
	}

	protected override void OnCantMove<T>(T component) {
		Player hitPlayer = component as Player;
		animator.SetTrigger ("enemyAttack");
		SoundManager.instance.RandomizeSfx (enemyAttack1, enemyAttack2);
		hitPlayer.LoseFood (playerDamage);
	}
}
