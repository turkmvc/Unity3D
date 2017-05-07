using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hook : MonoBehaviour {

	[SerializeField]
	private Transform cam;

	private Vector3 hookLoc;
	private RaycastHit hit;
	private Rigidbody rb;

	public LayerMask cullingMask;

	private Text cooldownText;

	[SerializeField]
	private bool attached = false;

	[SerializeField]
	private float maxDistance;
	[SerializeField]
	private float momentum;

	[SerializeField]
	private float speed;

	[SerializeField]
	private float step;

	[SerializeField]
	private LineRenderer LR;

	[SerializeField]
	private Transform hand;

	[SerializeField]
	private float cd;
	private bool canGrapple = true;


	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
		cooldownText = GameObject.Find("CooldownText").GetComponent<Text>();
		cooldownText.text = "Grapple Enabled";
	}
	
	// Update is called once per frame
	void Update () {

		if (cd > 0) {
			cooldown();
		}

		// Attach
		if (Input.GetKey(KeyCode.E) && !attached && canGrapple == true) {
			if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, cullingMask)) {
				attach();
				cd = 5f;
				canGrapple = false;
//				Debug.Log("Grapple Disabled");
			} 
		}

		// Detach
		if (Input.GetKey(KeyCode.Space) && attached) {
			detach();
		}

		// Increase momentum as time passes (ATTACHED)
		if (attached) {
			fly();
		} 

		// Decrease momentum as time passes (NOT ATTACHED)
		if (!attached && momentum >= 0) {
			decreaseMomentum();
		}
			
	}

	// Attach
	public void attach() {
		attached = true;
		rb.isKinematic = true;
		LR.enabled = true;
		LR.SetPosition(1, hit.point);
		// Save 3D location of hook point
		hookLoc = cam.forward;
	}

	// Detach
	public void detach() {
		attached = false;
		rb.isKinematic = false;
		LR.enabled = false;
		rb.velocity = hookLoc * speed;
	}

	// Increase momentum while attached 
	public void fly() {
		LR.SetPosition (0, hand.position);
		momentum += Time.deltaTime * speed;
		step = momentum * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, hit.point, step);

		// If the user reaches the end of the grappling hook, then detach
		if (Vector3.Distance (transform.position, hit.point) < 1f) {
			attached = false;
			rb.isKinematic = false;
			LR.enabled = false;
		}
	}

	// Decrease momentum while not attached
	public void decreaseMomentum() {
		if (!attached && momentum >= 0) {
			attached = false;
			momentum -= Time.deltaTime * 10;
			step = 0;
		}
	}

	// Cooldown
	public void cooldown() {
		cd -= Time.deltaTime;
		cooldownText.text = ((int)(cd)).ToString();
		if (cd <= 0) {
			canGrapple = true;
			cooldownText.text = "Grapple Enabled";
//			Debug.Log("Grapple Enabled");
		}

	}

}
