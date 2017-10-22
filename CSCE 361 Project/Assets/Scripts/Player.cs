using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D player;
    private Animator animator;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private Transform[] GroundPoints;
    [SerializeField]
    private float GroundRadius;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private LayerMask WhatIsGround;

    private bool FacingRight;
    private bool HasProjectile;

    private bool Grounded;
    private bool Jump;

    // Use this for initialization
    void Start () {
        FacingRight = true;
        HasProjectile = false;
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate () {
        float Horizontal = Input.GetAxis("Horizontal");

        Grounded = IsGrounded();
        PlayerInput();
        Movement(Horizontal);
        Flip(Horizontal);
        ChangeAnimationLayer();

        ResetValues();
    }
    
    private void Movement (float Horizontal) {
        // Set player x velocity
        player.velocity = new Vector2(MovementSpeed * Horizontal, player.velocity.y);
        
        // Set player y velocity (jumping)
        if (Grounded && Jump) {
            Grounded = false;
            player.AddForce(new Vector2(0, JumpForce));
        }

        // Set animator float to begin walk animation
        animator.SetFloat("Speed", Mathf.Abs(Horizontal));
    }

    private void PlayerInput () {
        // Jump Input (Spacebar or A Button on Xbox Controllers)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) {
            Jump = true;
            animator.SetBool("Jump", true);
        }

        // Fire Input (Left Shift or X Button on Xbox Controllers)
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown("joystick button 2")) {
            if (HasProjectile) {
                // TODO: Fire projectile in forward direction
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

    private void ChangeAnimationLayer() {
        if (!Grounded) {
            animator.SetLayerWeight(1, 1);
        } else {
            animator.SetLayerWeight(1, 0);
        }
    }

    private bool IsGrounded () {
        if (player.velocity.y <= 0) {
            foreach (Transform Point in GroundPoints) {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(Point.position, GroundRadius, WhatIsGround);
                for (int i = 0; i < colliders.Length; i++) {
                    if (colliders[i].gameObject != gameObject) {
                        animator.SetBool("Jump", false);
                        return true;
                    }
                }
            }

        }
        return false;
    }

    private void ResetValues () {
        Jump = false;
    }
}
