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
	private string jumpButton = "Jump_P1";
	[SerializeField]
	private string horizontalCtrl = "Horizontal_P1";
	[SerializeField]
	private string fireButton = "Fire_P1";
	[SerializeField]
	private string crouchButton = "Vertical_P1";

    public AudioClip cannonSound;
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
		float Horizontal = Input.GetAxisRaw(horizontalCtrl);

		grounded = IsGrounded();
		PlayerInput();
		Movement(Horizontal);
		Flip(Horizontal);
	}

	private void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Contains("Player")) {
			Physics2D.IgnoreCollision(col.collider, this.gameObject.GetComponent<Collider2D>());
		}
	}

	private void Movement (float Horizontal) {
		// Set player x velocity
		crazyCannon.velocity = new Vector2(MovementSpeed * Horizontal, crazyCannon.velocity.y);
		crazyCannon.velocity.Normalize();
		// Set player velocity to zero if crouched
		if (grounded && crouch) {
			crazyCannon.velocity = new Vector2(0, crazyCannon.velocity.y);
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
		if (Input.GetButtonDown(jumpButton)) {
			jump = true;
			cannonAnimator.SetBool("Jump", true);
		}

		// Crouch Input (S Key, Down Arrow Key, or Left Joystick Down)
		crouch = Input.GetAxisRaw(crouchButton) < -0.5;
		if (crouch) {
			cannonAnimator.SetBool("Crouch", true);
		} else {
			cannonAnimator.SetBool("Crouch", false);
		}

		// Fire Input (F Key or X Button on Xbox Controllers)
		if (Input.GetButtonDown(fireButton)) {
			if (hasProjectile) {
				projectile = Instantiate(projectile, forward.transform.position, Quaternion.identity);

				Vector2 shift = forward.transform.position;
				if (projectile.gameObject.name.Contains("Large Cannon Ball")) {
					shift.y += 0.05f;
					projectile.transform.position = shift;
                    SoundManager.instance.playClip(cannonSound, 1f);
                } else if(projectile.gameObject.name.Contains("Mid Cannon Ball")) {
                    
                    SoundManager.instance.playClip(cannonSound, 2f);

                } else if(projectile.gameObject.name.Contains("Small Cannon Ball")) {
                    
                    SoundManager.instance.playClip(cannonSound, 3f);

                }

				projectile.GetComponent<Projectile>().SetWhoFired(this.gameObject);
				hasProjectile = false;
				//Debug.Log("Projectile Fired!");
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
				Collider2D[] col = Physics2D.OverlapCircleAll(Point.position, GroundRadius, WhatIsGround);
				for (int i = 0; i < col.Length; i++) {
					if (col[i].gameObject != gameObject) {
						jump = false;
						cannonAnimator.SetBool("Jump", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	public void SetProjectile (GameObject prefab) {
		hasProjectile = true;
		projectile = prefab;
	}

	public bool GetProjectileBool (){
		return hasProjectile;
	}

	public bool GetDirection () {
		return facingRight;
	}

}