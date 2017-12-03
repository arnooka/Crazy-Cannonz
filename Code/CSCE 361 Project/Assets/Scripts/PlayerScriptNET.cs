using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.Networking;

public class PlayerScriptNET : NetworkBehaviour {
	
	private Rigidbody2D crazyCannon;
	private Animator cannonAnimator;
	private GameObject projectile;

	[SerializeField]
	private int playerNumber = 1;
	[SerializeField]
	private GameObject forward;
	[SerializeField]
	private GameObject waterEffect;
	
	[SerializeField]
	private float groundRadius, movementSpeed, jumpForce;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private AudioClip cannonSound;

	[SerializeField]
	private string jumpButton = "Jump_P1";
	[SerializeField]
	private string horizontalCtrl = "Horizontal_P1";
	[SerializeField]
	private string fireButton = "Fire_P1";
	[SerializeField]
	private string crouchButton = "Vertical_P1";

	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]
	private Transform[] spawnLocation, groundPoints;

	private int score, lastLocation = -1, respawnDelay = 2;
	private bool hasProjectile, facingRight, grounded, crouch, jump, respawning, pause;

	private Vector2 playerVelocity = Vector2.zero;

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)	{
			Destroy(this);
			return;
		}

		facingRight = true;
		hasProjectile = false;
		respawning = false;
		pause = false;
		projectile = null;
		score = 0;
		crazyCannon = GetComponent<Rigidbody2D>();
		cannonAnimator = GetComponent<Animator>();

	}

	[ServerCallback]
	void Update () {
		if (!isLocalPlayer)
			return;

		// Set player score based on player number
		//MatchManagerNET.SetPlayerScore(playerNumber, score);
		
		//scoreText.text = "P" + playerNumber.ToString() + ": " + score.ToString();
		
		// Check if game is paused or is in the initial countdown
		if (MatchManagerNET.GetIsActive() && !MatchManagerNET.GetIsCountdown()) {
			// Allow player movement
			if (pause) {
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				crazyCannon.velocity = playerVelocity;
				pause = false;
			}
			float Horizontal = Input.GetAxisRaw(horizontalCtrl);
			grounded = IsGrounded();
			CmdPlayerInput();
			Movement(Horizontal);
			Flip(Horizontal);
		} else {
			// Prevent/pause player movement
			if (!pause) {
				playerVelocity = crazyCannon.velocity;
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				pause = true;
			}
			crazyCannon.velocity = Vector2.zero;
		}
	}

	private void OnCollisionEnter2D(Collision2D col) {
		// Ignore collisions with other players
		if (col.gameObject.tag.Contains("Player") && isServer) {
			Physics2D.IgnoreCollision(col.collider, this.gameObject.GetComponent<Collider2D>());
		}
		
		// Collided with projectile that another player fired
		if (col.gameObject.tag.Contains("Projectile") && col.gameObject != projectile && isServer) {
			StartCoroutine(Respawn());
		}

		if (col.gameObject.tag.Contains ("Floor") && isServer) {
			StartCoroutine (Respawn ());
		}

		if (col.gameObject.tag.Contains ("Water") && isServer) {
			waterEffect = Instantiate (waterEffect, transform.position, transform.rotation);
			StartCoroutine (Respawn ());
		}

	}
	
	private void Movement (float Horizontal) {
		// Set player x velocity
		crazyCannon.velocity = new Vector2(movementSpeed * Horizontal, crazyCannon.velocity.y);
		
		// Set player velocity to zero if crouched
		if (grounded && crouch) {
			crazyCannon.velocity = new Vector2(0, crazyCannon.velocity.y);
		}

		// Set player y velocity (jumping)
		if (grounded && jump) {
			grounded = false;
			crazyCannon.AddForce(new Vector2(0, jumpForce));
		}

		// Set animator float to begin walk animation
		cannonAnimator.SetFloat("Speed", Mathf.Abs(Horizontal));
	}

	[Command]
	private void CmdPlayerInput () {
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
			if (hasProjectile && !respawning) {
				projectile = Instantiate(projectile, forward.transform.position, Quaternion.identity);
				
				Vector2 shift = forward.transform.position;
				if (projectile.gameObject.name.Contains("Large Cannon Ball")) {
					shift.y += 0.05f;
					projectile.transform.position = shift;
					SoundManager.getInstance().playClip(cannonSound, 1f);

				} else if (projectile.gameObject.name.Contains("Mid Cannon Ball")) {
					SoundManager.getInstance().playClip(cannonSound, 2f);

				} else if (projectile.gameObject.name.Contains("Small Cannon Ball")) {
					SoundManager.getInstance().playClip(cannonSound, 3f);

				}
				
				projectile.GetComponent<Projectile>().SetWhoFired(this.gameObject);
				hasProjectile = false;
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

	// Checks if player is touching the ground
	private bool IsGrounded () {
		if (crazyCannon.velocity.y <= 0) {
			foreach (Transform Point in groundPoints) {
				Collider2D[] col = Physics2D.OverlapCircleAll(Point.position, groundRadius, whatIsGround);
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

	// Player respawn coroutine
	IEnumerator Respawn() {
		if (!isServer) {
			if (score != 0) {
				score--;
			}
			EnableGameobject(false);
			respawning = true;
			
			// Wait for "respawnDelay" seconds then respawn character
			yield return new WaitForSeconds(respawnDelay);

			int location = Random.Range(0, spawnLocation.Length);
			while (location == lastLocation) {
				location = Random.Range(0, spawnLocation.Length);
			}
			lastLocation = location;

			respawning = false;
			EnableGameobject(true);
			gameObject.transform.position = spawnLocation[location].position;
		}
	}

	// Selfmade gameobject enable and disable function
	private void EnableGameobject(bool condition){
		this.gameObject.GetComponent<PolygonCollider2D>().enabled = condition;
		this.gameObject.GetComponent<SpriteRenderer>().enabled = condition;
		this.gameObject.GetComponent<Rigidbody2D>().isKinematic = !condition;
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

	public void AddScore(int value) {
		score += value;
	}

}