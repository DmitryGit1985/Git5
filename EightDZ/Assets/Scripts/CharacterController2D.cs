using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private ParticleSystem damageParticleSystem;
    [SerializeField] private ParticleSystem healParticleSystem;
    private Rigidbody2D body2D;
    private BoxCollider2D boxColl2D;
    private Vector2 direction;
    private Vector2 vecGravity;
    private const float moveSpeed = 3.0f;
    private const float jumpSpeed = 6.0f;
    private const float groundOffset = 0.1f;//doubleJump 1.3
    private const float fallMultiplyer = 3.0f;
    private int speedY;
    private const int characterController2DHealth = 5;
    private bool isJumping = false;
    private bool isAttacking = false;
    private AnimationClip animationClipAttack;
    private SpriteRenderer spriteRenderer; 
    [field:NonSerialized] public HealthSystem HealthSystem { get; private set; }
    [field: NonSerialized] public bool isVulnerable { get; private set; } = true;
    [field: NonSerialized] public Animator CharacterAnimator { get; private set; }
    [field: NonSerialized] public bool IsDeath { get; set; }

    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();//Interpolate or not?
        CharacterAnimator = GetComponent<Animator>();
        boxColl2D = GetComponent<BoxCollider2D>();
        vecGravity = new Vector2(0, Physics2D.gravity.y);
        HealthSystem = new HealthSystem(characterController2DHealth);
        HealthSystem.OnDead += HealthSystem_OnDead;
        HealthSystem.OnDamaged += HealthSystem_OnDamaged;
        HealthSystem.OnHealed += HealthSystem_OnHealed;
        #region UnUsed
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //animationClipAttack = FindAnimation(characterAnimator, "NinjaAttack");
        //animationClipAttack=characterAnimator.runtimeAnimatorController.animationClips[3];
        //animationClipAttack.AddEvent(new AnimationEvent(){ time = animationClipAttack.length, functionName = "OnCompletedAttackedAnimation" });
        #endregion
    }
    private void Update()
    {
        if (IsDeath==false)
        {
            Jumping();
            Moving();
            Attacking();         
        }
    }
    private void FixedUpdate()
    {
        if (IsDeath == false)
        {
            body2D.velocity = new Vector2(direction.x * moveSpeed, body2D.velocity.y);
        }
    }
    private void Jumping()
    {
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            isJumping = true;
            CharacterAnimator.SetTrigger("Jumping");
            body2D.velocity = Vector2.up * jumpSpeed;
            CharacterAnimator.SetBool("IsGrounded", false);
        }
        speedY = (int)Mathf.Abs(body2D.velocity.normalized.y);

        if (isJumping == true && speedY == 0)
        {
            if (Physics2D.BoxCast(boxColl2D.bounds.center, boxColl2D.bounds.size, 0f, Vector2.down, groundOffset, groundLayerMask))
            {
                isJumping = false;
                CharacterAnimator.SetBool("IsGrounded", true);
            }
        }
        if (body2D.velocity.y < 0)
        {
            body2D.velocity += vecGravity * fallMultiplyer * Time.deltaTime;
        }
        //Дергание на платформе с lerp движением вверх вниз
    }
    private void Moving()
    {
        float moveX = Input.GetAxis("Horizontal");
        direction = new Vector2(moveX, 0).normalized;
        CharacterAnimator.SetInteger("Speed", (int)Mathf.Abs(direction.x));
        if (direction.x > 0)
        {
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);//Как правильно отражать и Sprite и collider2D? Если объект состоит из много коллайдеров? Rigidbody simulated что это?
        }
        if (direction.x < 0)
        {
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
    }
    private void Attacking()
    {
        if (Input.GetKey(KeyCode.F) && isAttacking == false && isJumping == false)
        {
            isAttacking = true;
            CharacterAnimator.SetTrigger("Attack");
        }
    }
    private void OnCompletedAttackedAnimation()
    {
        isAttacking = false;
    }
    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        if (IsDeath == false)
        {
            CharacterAnimator.SetTrigger("Death");
            body2D.velocity = Vector2.zero;
            IsDeath = true;
        }
    }
    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        if (IsDeath == false&& isVulnerable==true)
        {
            damageParticleSystem.Play();
            StartCoroutine(Vulnerable());
        }
    }
    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        if (IsDeath == false)
        {
            healParticleSystem.Play();
        }
    }
    private IEnumerator Vulnerable()
    {
        isVulnerable = false;
        yield return new WaitForSeconds(2f);
        isVulnerable = true;
    }
    #region UnUsed
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColl2D.bounds.center, boxColl2D.bounds.size, 0f, Vector2.down , groundOffset, groundLayerMask);
        return raycastHit2D.collider!=null;
    }
    private AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }
    #endregion
}
