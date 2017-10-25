using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


	private Rigidbody2D crazyCannon;
	private Animator cannonAnimator;
	private GameObject projectile;

	[SerializeField]
	private GameObject forward;
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

	private bool hasProjectile;
	private bool facingRight;
	private bool grounded;
	private bool crouch;
	private bool jump;
	
	// Use this for initialization
	void Start () {
		facingRight = true;
		hasProjectile = false;
		projectile = null;
		crazyCannon = GetComponent<Rigidbody2D>();
		cannonAnimator = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		float Horizontal = Input.GetAxisRaw("Horizontal");

		grounded = IsGrounded();
		PlayerInput();
		Movement(Horizontal);
		Flip(Horizontal);
	}
	
	/* TODO:
	 *		- Figure out falling transition
	 */
	private void Movement (float Horizontal) {
		// Set player x velocity
		crazyCannon.velocity = new Vector2(MovementSpeed * Horizontal, crazyCannon.velocity.y);
		
		// Set player velocity to zero if crouched
		if (grounded && crouch) {
			crazyCannon.velocity = Vector2.zero;
		}
		
		// Set player y velocity (jumping)
		if (grounded && jump) {
			grounded = false;
			crazyCannon.AddForce(new Vector2(0, JumpForce));
		}

		// Set animator float to begin walk animation
		cannonAnimator.SetFloat("Speed", Mathf.Abs(Horizontal));
	}
	
	private void PlayerInput () {
		// Jump Input (Spacebar or A Button on Xbox Controllers)
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) {
			jump = true;
			cannonAnimator.SetBool("Jump", true);
		}

		// Crouch Input (S Key, Down Arrow Key, or Left Joystick Down)
		crouch = Input.GetAxisRaw("Vertical") < -0.5;
		if (crouch) {
			cannonAnimator.SetBool("Crouch", true);
		} else {
			cannonAnimator.SetBool("Crouch", false);
		}
		
		// Fire Input (F Key or X Button on Xbox Controllers)
		if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown("joystick button 2")) {
			if (hasProjectile) {
				if (crazyCannon.transform.localScale.x < 0) {
					// Instantiate projectile to the left
					Instantiate(projectile, forward.transform.position, transform.rotation);
				} else {
					// Instantiate projectile to the right
					Instantiate(projectile, forward.transform.position, transform.rotation);
				}
				hasProjectile = false;
				Debug.Log("Projectile Fired!");
			}
		}
	}
	
	// Flips player orientation along horizontal axis
	private void Flip (float Horizontal) {
		if (Horizontal > 0 && !facingRight || Horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			
			Vector2 scale = transform.localScale;
			
			scale.x *= -1;
			transform.localScale = scale;
		}
	}
	
	private bool IsGrounded () {
		if (crazyCannon.velocity.y <= 0) {
			foreach (Transform Point in GroundPoints) {
				Collider2D[] Colliders = Physics2D.OverlapCircleAll(Point.position, GroundRadius, WhatIsGround);
				for (int i = 0; i < Colliders.Length; i++) {
					if (Colliders[i].gameObject != gameObject) {
						jump = false;
						cannonAnimator.SetBool("Jump", false);
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

	public void SetProjectile (GameObject prefab) {
		hasProjectile = true;
		projectile = prefab;
	}

	public bool GetProjectileBool (){
		return hasProjectile;
	}
}
