using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BasePlayer // WARRIOR
{
    //Gestion Jump
    float secondJumpTimer;
    public bool hasShield = false;
    bool shieldActive = false;
    public GameObject shield;

    //Animation
    Animator animator;
    bool IsRunning, Sword, Bow, IsFalling, IsJumping;
    float Oldyposition;
 

    //Gestion Atk
    float TIME_BETWEEN_SHOTS = 1f;
    float shotTimer = 0.0f;

    //Abilities
    public static bool HasDoubleJumpAbility;

    public override void Start()
    {
        base.Start();
        //spriteChild = transform.Find("Player1");
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        shotTimer -= Time.deltaTime;
        base.Update();
        if(!isDead)
        {
            if (CanJump == 1 && HasDoubleJumpAbility)
                secondJumpTimer -= Time.deltaTime;
            else
                secondJumpTimer = 0.2f;
        }

        UpdateAnimator();
        Oldyposition = transform.position.y;

    }

    public override void GestionInput()
    {
        if (Input.GetButton("LeftPlay1") && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            IsRunning = true;
            transform.Translate(-Vector2.left * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.left);
        }

        if (Input.GetButtonUp("LeftPlay1")) {

            IsRunning = false;
        }

        if (Input.GetButton("RightPlay1") && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            IsRunning = true;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.right);
        }
        
        if (Input.GetButtonUp("RightPlay1"))
        {
            IsRunning = false;
        }

        if (Input.GetAxis("HorizontalJoystick1") > 0 && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            transform.Translate(Vector2.left * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.left);
        }

        if (Input.GetAxis("HorizontalJoystick1") < 0 && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.right);
        }

        if (Input.GetButtonDown("JumpPlay1") && CanJump >= 1)
        {

            IsJumping = true;


            if(HasDoubleJumpAbility && CanJump == 1)
            {
               //Animation for double jump here
                if(secondJumpTimer <= 0)
                {
                    CanJump--;
                    rigidBody2D.AddForce(Vector2.up * jumpForce);
                }
            }
            else
            {
                //put anim for normal jump Here
                CanJump--;
                rigidBody2D.AddForce(Vector2.up * jumpForce);
            }
            
        }

        if (Input.GetButtonDown("TPAbilityPlay1") && hasSpecialAbility)
        {
            Berserk();
        }

        if (Input.GetButtonDown("TestingButton1"))
        {
            TakeDamage(1, true);
        }

        if (Input.GetButtonDown("WarriorShield") && !shieldActive && hasShield)
        {
            StartCoroutine(SpawnShield());
        }

        if (Input.GetButton("WarriorRangeAtk") && hasRangeAtk && shotTimer <= 0)
        {
            shotTimer = TIME_BETWEEN_SHOTS;
            Bow = true;     
          
                if (isLookingRight)
                    Instantiate(RngAtk, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z), transform.rotation);
                else
                    Instantiate(RngAtk, new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z), transform.rotation); 

        }

        if (Input.GetButtonDown("WarriorMeleeAtk") && hasMeleeAtk && shotTimer <= 0)
        {
            shotTimer = TIME_BETWEEN_SHOTS;
            Sword = true;
            if (isLookingRight)
                Instantiate(MeleeAtk, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z), transform.rotation);
            else
                Instantiate(MeleeAtk, new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z), transform.rotation);
         

        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "PlayerTwo")
        {
             IsFalling = false;
  
            if (HasDoubleJumpAbility)
                CanJump = 2;
            else
                CanJump = 1;
        }
    }

    void TakeDamage(int nbOfDmg, bool IsPhysical)
    {
        if (invincibilityFrames <= 0)
        {
            if (!(shieldActive && IsPhysical))    //If the atk IS physical and the shild is active, then dmg block
            {
                StartCoroutine(SetInvicibility(2.0f));

                health -= nbOfDmg;
                if (health <= 0)
                {
                    health = 0;
                    isDead = true;
                }
            }
        }
    }


    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        this.transform.rotation = rotation3D;
    }

    IEnumerator SpawnShield()
    {
        shieldActive = true;
        GameObject tempShield = Instantiate(shield, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(2.0f);
        shieldActive = false;
        Destroy(tempShield);
    }


    void UpdateAnimator() {

        animator.SetBool("MeleeAtk", Sword);
        animator.SetBool("RangeAtk", Bow);
        animator.SetBool("IsRunning", IsRunning);
        animator.SetBool("IsFalling", IsFalling);
        animator.SetBool("IsJumping", IsJumping);


        Sword = false;
        Bow = false;
        IsJumping = false;

        if (transform.position.y - Oldyposition < deltaTolerance)
        {
            IsFalling = true;
        }
    }

    void Berserk() 
    {
        
    }

    void Die() {

        Destroy(gameObject);

    }
}
