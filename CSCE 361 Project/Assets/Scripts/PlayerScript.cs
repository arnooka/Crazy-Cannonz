using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public GameObject projectile;

	private Rigidbody2D CrazyCannon;
	private Animator CannonAnimator;
	
	[SerializeField]
	private float MovementSpeed;
	[SerializeField]
	private float JumpForce;
	[SerializeField]
	private Transform[] GroundPoints;
	[SerializeField]
	private float GroundRadius;
	[SerializeField]
	private LayerMask WhatIsGround;
	[SerializeField]
	private PolygonCollider2D[] colliders;
	private int currentColliderIndex = 0;
	
	private bool FacingRight;
	public bool HasProjectile;
	
	private bool Grounded;
	private bool Crouch;
	private bool Jump;
	private bool JoystickDown;
	
	// Use this for initialization
	void Start () {
		FacingRight = true;
		HasProjectile = false;
		CrazyCannon = GetComponent<Rigidbody2D>();
		CannonAnimator = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		float Horizontal = Input.GetAxisRaw("Horizontal");
		
		Grounded = IsGrounded();
		PlayerInput();
		Movement(Horizontal);
		Flip(Horizontal);
		
		
		//ResetValues();
	}
	
	/* TODO:
	 *		- Make movement appear sharp
	 *		- Make jump less "floaty"
	 *		- Figure out falling transition
	 */
	private void Movement (float Horizontal) {
		// Set player x velocity
		CrazyCannon.velocity = new Vector2(MovementSpeed * Horizontal, CrazyCannon.velocity.y);
		
		// Set player velocity to zero if crouched
		if (Grounded && Crouch) {
			CrazyCannon.velocity = Vector2.zero;
		}
		
		// Set player y velocity (jumping)
		if (Grounded && Jump) {
			Grounded = false;
			CrazyCannon.AddForce(new Vector2(0, JumpForce));
		}

		// Set animator float to begin walk animation
		CannonAnimator.SetFloat("Speed", Mathf.Abs(Horizontal));
	}
	
	private void PlayerInput () {
		// Jump Input (Spacebar or A Button on Xbox Controllers)
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) {
			Jump = true;
			CannonAnimator.SetBool("Jump", true);
		}

		// Crouch Input (S Key, Down Arrow Key, or Left Joystick Down)
		Crouch = Input.GetAxisRaw("Vertical") < -0.5;
		if (Crouch) {
			CannonAnimator.SetBool("Crouch", true);
		} else {
			CannonAnimator.SetBool("Crouch", false);
		}
		
		// Fire Input (F Key or X Button on Xbox Controllers)
		if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown("joystick button 2")) {
			if (HasProjectile && projectile != null) {
				/* TODO: 
				 *		- Instantiate projectile from prefabs
				 *		- Fire projectile in forward direction
				 */
				if (CrazyCannon.transform.localScale.x < 0) {
					// Instantiate projectile to the left
				} else {
					// Instantiate projectile to the right
				}
				HasProjectile = false;
			}
			Debug.Log("Projectile Fired!");
		}
	}
	
	// Flips player orientation along horizontal axis
	private void Flip (float Horizontal) {
		if (Horizontal > 0 && !FacingRight || Horizontal < 0 && FacingRight) {
			FacingRight = !FacingRight;
			
			Vector2 scale = transform.localScale;
			
			scale.x *= -1;
			transform.localScale = scale;
		}
	}
	
	private bool IsGrounded () {
		if (CrazyCannon.velocity.y <= 0) {
			foreach (Transform Point in GroundPoints) {
				Collider2D[] Colliders = Physics2D.OverlapCircleAll(Point.position, GroundRadius, WhatIsGround);
				for (int i = 0; i < Colliders.Length; i++) {
					if (Colliders[i].gameObject != gameObject) {
						Jump = false;
						CannonAnimator.SetBool("Jump", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	public void SetSpriteCollider (int colliderNumber) {
		colliders[currentColliderIndex].enabled = false;
		currentColliderIndex = colliderNumber;
		colliders[currentColliderIndex].enabled = true;
	}
	
	private void ResetValues () {
		Jump = false;
	}		
}
