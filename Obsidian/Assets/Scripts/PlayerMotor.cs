using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 thrusterForce = Vector3.zero;

	// X-rotation
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;
	[SerializeField]
	private float cameraRotationLimit = 85f;

	private Rigidbody rb;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	// Gets a movement vector
	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	// Gets a rotational vector
	public void Rotate(Vector3 _rotation) {
		rotation = _rotation;
	}

	// Get thruster force
	public void ApplyThruster(Vector3 _thrusterForce) {
		thrusterForce = _thrusterForce;
	}

	// Gets a rotational vector for the camera
	public void RotateCamera(float _cameraRotationX) {
		cameraRotationX = _cameraRotationX;
	}

	// Run every physics iteration
	void FixedUpdate() {
		PerformMovement();
		PerformRotation();
	}

	// Perform movement based on velocity
	void PerformMovement() {
		// Moving
		if (velocity != Vector3.zero) {
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}

		// Jetpack
		if (thrusterForce != Vector3.zero) {
			rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}
	}

	// Perform rotation
	void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

		if (cam != null) {
			// Set rotation and clamp it
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			// Apply rotation to the transform of camera
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}

}
