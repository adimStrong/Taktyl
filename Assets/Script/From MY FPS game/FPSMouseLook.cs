using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseLook : MonoBehaviour {

	public enum RotationAxes {mouseX,mouseY}
	public RotationAxes axes = RotationAxes.mouseY;

	private float currentSensitivity_X =  1.5f;
	private float currentSensitivity_Y =  1.5f;

	private float sensitivityX = 1.5f;
	private float sensitivtyY= 1.5f;

	private float rotation_X, rotation_Y;
	private float minimum_X = -360f;
	private float maximum_X = 360f;

	private float minimum_Y = -60f;
	private float maximum_Y = 60f;

	private Quaternion originalRotaion;

	private float mouseSensitivity = 1.7f;

	void Start(){

		originalRotaion = transform.rotation;
	}

	void Update(){


	}

	void FixedUpdate(){


	}
	void LateUpdate(){
		HandleRotation ();

	}

	// method that will set the angle of the camera min and max value then clamp it
	float ClampAngle (float angle, float min, float max){
		if (angle < -360f) {
			angle += 360f;
		} 
		if (angle > 360f) {

			angle -= 360f;
		}

		return Mathf.Clamp (angle, min, max);
	}

	void HandleRotation(){

		// can use in different situation like if player is dead
		if (currentSensitivity_X != mouseSensitivity || currentSensitivity_Y != mouseSensitivity) {
			currentSensitivity_X = currentSensitivity_Y = mouseSensitivity;
		}

		sensitivityX = currentSensitivity_X;

		sensitivtyY = currentSensitivity_Y;
		// get the input from the mosue or someother other input and multiply it to sensitivity X or Sensitivity Y
		if (axes == RotationAxes.mouseX) {
			rotation_X += Input.GetAxis ("Mouse X") * sensitivityX;

			// clamp the value to the clample angle
			rotation_X = ClampAngle (rotation_X, minimum_X, maximum_X);
			Quaternion xQuaterion = Quaternion.AngleAxis (rotation_X, Vector3.up);

			// assign it to old rotation by multiplying it
			transform.localRotation = originalRotaion * xQuaterion;
	
		}
		// can change the rotaion_Y to positive to inverse the mouseMove
		if (axes == RotationAxes.mouseY) {
			rotation_Y += Input.GetAxis ("Mouse Y") * sensitivtyY;
			rotation_Y = ClampAngle (rotation_Y, minimum_Y, maximum_Y);

			// rotation in degree Y up and down movement
			Quaternion yQuaterion = Quaternion.AngleAxis (-rotation_Y, Vector3.right);

			// rotation in respect to the parent
			transform.localRotation = originalRotaion * yQuaterion;

		}

	}
}
