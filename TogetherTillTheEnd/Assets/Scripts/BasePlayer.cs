using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class BasePlayer : MonoBehaviour
{
    //Gestion Movement
    protected float moveSpeed = 5.5f;
    protected bool isLookingRight = true;

    //Gestion Abilities
    public GameObject RngAtk;
    public GameObject MeleeAtk;
    public bool hasSpecialAbility = false;
    public bool hasRangeAtk = false;
    public bool hasMeleeAtk = false;

    //Gestion Life
    [SerializeField]
    public int health = 3;
    protected bool isDead = false;
    protected float invincibilityFrames = 0.0f;

    //Gestion Jump
    [SerializeField]
    protected float jumpForce = 550.0f;
    protected int CanJump = 1;

    //Animation Transition
    protected float deltaTolerance = -0.01f;

    //External Refrences
    protected Rigidbody2D rigidBody2D;
    protected Transform spriteChild;
    protected SpriteRenderer spriteRenderer;

    protected SharedCamera sharedCam;

    public virtual void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        sharedCam = FindObjectOfType<SharedCamera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        invincibilityFrames -= Time.deltaTime;
        if (!isDead)
        {
            GestionInput();
        }
    }

    protected IEnumerator SetInvicibility(float time)
    {
        invincibilityFrames = time;
        spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(time);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    

    public abstract void GestionInput();
}
