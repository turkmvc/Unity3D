using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Grapplinghook : MonoBehaviour {

	public Camera cam;
	public RaycastHit hit;

	public LayerMask cullingMask;
	public int maxDistance;

	public bool isFlying;
	public Vector3 location;
	[SerializeField]
	private float speed = 10;
	public Transform hand;

	public FirstPersonController FPC;
	public LineRenderer LR;
//	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
//		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		// Put any key
		if (Input.GetKey(KeyCode.Q)) {
			Findspot();
		}

		if (isFlying) {
			Flying();
		}

		if (Input.GetKey(KeyCode.Space) && isFlying) {
			
			isFlying = false;
			FPC.CanMove = true;
			LR.enabled = false;
//			rb.isKinematic = false;
//			rb.velocity = cam.transform.forward * speed;
//			if (speed >= 0) {
//				speed -= Time.deltaTime * 5;
//			}		
		}

	}

	// Find location of hook
	public void Findspot() {
		// If location from raycast exists, start grappling hook
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, maxDistance, cullingMask)) {
			isFlying = true;
			location = hit.point;
			FPC.CanMove = false;
			LR.enabled = true;
			LR.SetPosition(1, location);
//			rb.isKinematic = true;
		}
	}

	public void Flying() {
		transform.position = Vector3.Lerp(transform.position, location, speed * Time.deltaTime / Vector3.Distance(transform.position, location));
		LR.SetPosition (0, hand.position);
		if (Vector3.Distance (transform.position, location) < 0.5f) {
			isFlying = false;
			FPC.CanMove = true;
			LR.enabled = false;
		}
	}

}
