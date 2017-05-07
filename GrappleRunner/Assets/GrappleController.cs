using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour {

	public GameObject grappleShooter;
	private SpringJoint2D grapple;

	[SerializeField]
	public LineRenderer LR;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Fire();
		} 
	}

	void LateUpdate() {
		if (grapple != null) {
			LR.enabled = true;
			LR.numPositions = 2;
			LR.SetPosition(0, grappleShooter.transform.position);
			LR.SetPosition(1, grapple.connectedAnchor);
		} else {
			LR.enabled = false;
		}
	}

	void FixedUpdate() {
		if (grapple != null) {
			if (Input.GetMouseButtonUp(0)) {
				GameObject.DestroyImmediate(grapple);
			}
		}
	}

	void Fire() {
//		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 position = grappleShooter.transform.position;
//		Vector3 direction = mousePosition - position;


		// 45 - right, 135 - left
		float angle = 45;
		Vector3 direction = GetDirectionVector2D(angle);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);

		if (hit.collider != null) {
			SpringJoint2D newGrapple = grappleShooter.AddComponent<SpringJoint2D>();
			newGrapple.enableCollision = false;
			newGrapple.frequency = 10f;
			newGrapple.connectedAnchor = hit.point;
			newGrapple.enabled = true;

			GameObject.DestroyImmediate(grapple);
			grapple = newGrapple;
		}
	}

	public Vector2 GetDirectionVector2D(float angle) {
		return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
	}
}
