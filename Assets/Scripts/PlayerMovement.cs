using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Animator playerAnimator;
	private float moveHorizontal;
	private float moveVertical;
	private Vector3 movement;
	private float turningSpeed = 20f;
	private Rigidbody playerRigibbody;
	[SerializeField]
	private RandomSoundPlayer playerFootsteps;

	// Use this for initialization
	void Start () {

		// Gather component from te Player GameObject
		playerAnimator = GetComponent<Animator> ();
		playerRigibbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		moveHorizontal = Input.GetAxisRaw ("Horizontal");
		moveVertical = Input.GetAxisRaw ("Vertical");

		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
	}

	void FixedUpdate(){

		//if the player's movement vector does not equal Zero...
		if (movement != Vector3.zero) {


			//...then create a target rotation base on the movement vertoc...
			Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);

			//...and create another rotation that move from current location to target location
			Quaternion newRotation = Quaternion.Lerp(playerRigibbody.rotation, targetRotation, turningSpeed * Time.deltaTime);

			//...and change the player's rotation to the new incremental rotation...
			playerRigibbody.MoveRotation(newRotation);

			//...then play the jump animation
			playerAnimator.SetFloat ("Speed", 3f);

			//...play footstep sounds.
			playerFootsteps.enabled = true;
		} else {
			//otherwise, dont jump
			playerAnimator.SetFloat("Speed", 0f);
			//don't play footstep sound.
			playerFootsteps.enabled = false;
		}
	}
}
