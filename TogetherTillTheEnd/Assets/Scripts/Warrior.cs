using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BasePlayer // WARRIOR
{
    //Gestion Jump
    float secondJumpTimer;

    //Abilities
    public bool HasDoubleJumpAbility;

    public override void Start()
    {
        base.Start();
        //spriteChild = transform.Find("Player1");
    }

    public override void Update()
    {
        base.Update();
        if(!isDead)
        {
            if (CanJump == 1 && HasDoubleJumpAbility)
                secondJumpTimer -= Time.deltaTime;
            else
                secondJumpTimer = 0.2f;
        }
    }

    public override void GestionInput()
    {
        if (Input.GetButton("LeftPlay1") && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            //FaceDirection(-Vector2.right);
        }
        if (Input.GetButton("RightPlay1") && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick1") > 0 && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick1") < 0 && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.left);
        }

        if (Input.GetButtonDown("JumpPlay1") && CanJump >= 1)
        {
            if(HasDoubleJumpAbility && CanJump == 1)
            {
                if(secondJumpTimer <= 0)
                {
                    CanJump--;
                    rigidBody2D.AddForce(Vector2.up * jumpForce);
                }
            }
            else
            {
                CanJump--;
                rigidBody2D.AddForce(Vector2.up * jumpForce);
            }
            
        }

        if (Input.GetButtonDown("TestingButton"))
        {
            TakeDamage(1, true);
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "PlayerTwo")
            if (HasDoubleJumpAbility)
                CanJump = 2;
            else
                CanJump = 1;
    }

    void TakeDamage(int nbOfDmg, bool IsPhysical)
    {
        if(invincibilityFrames <= 0 && true) // "true" will be replace by shield check later
        {
            StartCoroutine(SetInvicibility(1.5f));
            
            health -= nbOfDmg;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
            }
        }
    }
}
