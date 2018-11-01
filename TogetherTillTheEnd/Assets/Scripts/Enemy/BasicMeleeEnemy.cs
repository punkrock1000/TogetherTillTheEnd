using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemy : MonoBehaviour {
    bool mBusy = false;
    [SerializeField]
    Transform mMage;
    [SerializeField]
    Transform mWarrior;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float mAttackDistance;
    Transform mTarget;
    bool mTargettingMage = false;
    Animator mAnimator;
    bool visible = false;
    Vector2 direction;
    bool jumping = false;
    float jumpForce = 7;
    // Use this for initialization
    void Start () {
        mAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!mBusy && visible)
        {
            if (targetChoice())
            {
                mTarget = mMage;
            }
            else {
                mTarget = mWarrior;
            }
            direction = (mTarget.position - transform.position);
            if (Math.Abs(direction.x) < mAttackDistance)
            {
                mBusy = true;
                attack(direction);
            }
            else
            {
                direction = (direction.x < 0) ? Vector2.left : Vector2.right;
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
                FaceDirection(direction);
            }

        }
	}

    bool targetChoice()
    {
        Vector2 mageDiff = mMage.transform.position - transform.position;
        Vector2 warrDiff = mWarrior.transform.position - transform.position;

        if(mageDiff.magnitude < warrDiff.magnitude)
        {
            return true;
        }
        else {
            return false;
        }
    }

    void attack(Vector2 direction)
    {
        mAnimator.SetBool("isAttacking", true);
    }

    void attackEnd()
    {
        mBusy = false;
        mAnimator.SetBool("isAttacking", false);
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("EditorOnly") && !mBusy && !jumping)
        {
            if((transform.position.y - mTarget.transform.position.y) < -.3 && direction.Equals(collision.gameObject.GetComponent<JumpBox>().GetDirection()))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumping = true;
                mAnimator.SetBool("isJumping", true);
            }
        }
    }

    void jumpEnd()
    {
        jumping = false;
        mAnimator.SetBool("isJumping", false);
    }

    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        this.transform.rotation = rotation3D;
    }
}
