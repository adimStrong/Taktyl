 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

	private Transform firstPerson_View;
	private Transform firstPerson_Camera;
	private Vector3 firstPerson_View_Rotation = Vector3.zero;

	public float walkSpeed = 6.75f;

	public float runSpeed = 10f;
	public float crouchSpeed = 4f;
	public float jumpSpeed = 8f;
	public float gravirty = 20f;

	private float speed;

	private bool is_Moving, is_Grounded, is_Crouching;

	private float inputX, inputY;


	private float inputX_Set, inputY_Set;

	private float inputModifyFactor;

	private bool limitDiagonalSpeed = true;

	private float antiBumpFactor = 0.75f;

	private CharacterController charController;

	private Vector3 moveDirection = Vector3.zero;

	private FPSPlayerAnimation playerAnimation;


	public LayerMask groundLayer;
	private float rayDistance;
	private float default_ControllerHeight;
	private Vector3 default_CamPos;
	private float camHeight;

	void Start(){
		firstPerson_View = transform.Find ("FPS View").transform;
		charController = GetComponent<CharacterController> ();


		speed = walkSpeed;
		is_Moving = false;

		rayDistance = charController.height * 0.5f + charController.radius;
		default_ControllerHeight = charController.height;
		default_CamPos = firstPerson_View.localPosition;

		playerAnimation = GetComponent<FPSPlayerAnimation> ();
	}
	void Update(){
		PlayerMovement ();


	}

	void PlayerMovement(){

		// we got the input here if w or s input Y is Set move forward and backward
		if (Input.GetKey (KeyCode.W) | Input.GetKey (KeyCode.S)) {
			if (Input.GetKey (KeyCode.W)) {
				inputY_Set = 1f;
			} else {
				inputY_Set = -1f;
			}
		} else {
			// pag hindi na pindot ang W or S inputY is 0 means that no vertical movement
			inputY_Set = 0f;
			}

		// get the input if player press A or D and set the inputX value to move left and right
		if (Input.GetKey (KeyCode.A) | Input.GetKey (KeyCode.D)) {
			if (Input.GetKey (KeyCode.A)) {
				inputX_Set = -1f;
			} else {
				inputX_Set = 1f;
			}
		} else {
			// if neither A or D is pressed no horizontal movement
			inputX_Set = 0f;
		}
		 // lerp the value of inputX and inputY
		inputY = Mathf.Lerp (inputY, inputY_Set, Time.fixedDeltaTime * 19f);
		inputX = Mathf.Lerp (inputX, inputX_Set, Time.fixedDeltaTime * 19f);

		// modify the lerp factor if inputX or inputY is != 0 means that we cannot go max speed if
		// player move diagonally
		inputModifyFactor = Mathf.Lerp (inputModifyFactor,(inputY_Set != 0 && 
			inputX_Set != 0 && limitDiagonalSpeed) ? 0.75f : 1.0f
			,Time.fixedDeltaTime * 19f);
		// in this part controls the player FPS view  along with the player ( the parent object)
		firstPerson_View_Rotation = Vector3.Lerp (firstPerson_View_Rotation, Vector3.zero, 
			Time.deltaTime * 5f); 
		// this is relative to parent para sa rotation
		firstPerson_View.localEulerAngles = firstPerson_View_Rotation;

		if (is_Grounded) {
			// we will call run and sprint here 
			PlayerCroushingAngSprinting();

			// if on ground we calculate  moveDir meron din antibump factor para pag bumanga sa ibang object or sa ground
			moveDirection = new Vector3 (inputX * inputModifyFactor, -antiBumpFactor,
				inputY * inputModifyFactor);
			// converting the direction from local to world space and multiply by speed to smooth the movement
			moveDirection = transform.TransformDirection (moveDirection) * speed;

			// here we are going to call jump
			PlayerJump();
		}
		// here we apply the gravity
		moveDirection.y -= gravirty * Time.deltaTime;

		// if on ground the character to ground by checking the collision flag
		is_Grounded = (charController.Move(moveDirection * Time.deltaTime) 
			& CollisionFlags.Below ) != 0;

		// test the magnite of the character calculate the momentum by 0.15  and check is moving
		is_Moving = charController.velocity.magnitude > 0.15f;
		HandleAnimation ();
	}

	void PlayerCroushingAngSprinting(){
		if (Input.GetKeyDown (KeyCode.C)) {
			// check if is not crouching then player can crouch
			if (!is_Crouching) {
				is_Crouching = true;
			} else {
				// if player is on crouch position player will stand up
				if (CanGetUp ()) {
					is_Crouching = false;
				}
			}
			// this will get player to crouch or stand up
			StopCoroutine(MoveCameraCrouch());
			StartCoroutine(MoveCameraCrouch());

		}

		if (is_Crouching) {
			speed = crouchSpeed;
		} else {
			// set player to run speed
			if (Input.GetKey (KeyCode.LeftShift)) {
				speed = runSpeed;
			} else {
				// if not pressed the run btn go back to normal speed
				speed = walkSpeed;
			}

		}
		playerAnimation.PlayerCrouch (is_Crouching);
	}
	 
	bool CanGetUp(){
		// hold the origin when we can cast the ray cast from transform up direction
		Ray groundRay = new Ray (transform.position, transform.up);

		// information we extract from raycast
		RaycastHit groundHit; 

		// we use sphere ray cast so if we collide on the groundlayer
		if (Physics.SphereCast (groundRay, charController.radius + 0.05f, 
			out groundHit, rayDistance, groundLayer)) {
			if(Vector3.Distance(transform.position, groundHit.point) <2.3f){
				//on crouch pos player will stand
				return false;
			}
		
		}
		// player can translate to crouch position
		return true;
	}

	IEnumerator MoveCameraCrouch(){
		charController.height = is_Crouching ? default_ControllerHeight / 1.5f : 
			default_ControllerHeight;


		charController.center = new Vector3 (0f, charController.height / 2f, 0f);

		camHeight = is_Crouching ? default_CamPos.y / 1.5f : default_CamPos.y;

		while (Mathf.Abs (camHeight - firstPerson_View.localPosition.y) > 0.01f) {
			firstPerson_View.localPosition = Vector3.Lerp(firstPerson_View.localPosition,
				new Vector3(default_CamPos.x, camHeight, default_CamPos.z), Time.deltaTime * 11f);
			yield return null;
		}
	}

	void PlayerJump(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (is_Crouching) {
				if (CanGetUp ()) {
					// if we are on crouch position we will stand up when jump is pressed
					is_Crouching = false;

					playerAnimation.PlayerCrouch (is_Crouching);
					StopCoroutine (MoveCameraCrouch ());
					StartCoroutine (MoveCameraCrouch ());
				}
				 
			} else {
				// player will jump vertically
				moveDirection.y = jumpSpeed;
			}
		}

	}

	void HandleAnimation(){
		playerAnimation.Movement (charController.velocity.magnitude);
		playerAnimation.PlayerJump (charController.velocity.y);

		if (is_Crouching && charController.velocity.magnitude > 0f) {
			playerAnimation.PlayerCrouchWalk(charController.velocity.magnitude);  

		}
	}
}

	



