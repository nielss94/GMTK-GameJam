using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

  [SerializeField] private float maxSpeed = 7;
  [SerializeField] private float jumpTakeOffSpeed = 7;
  [SerializeField] private float dashSpeed = 7f;
  [SerializeField] private float dashTime = 0.5f;
  [SerializeField] private Color dashColor = Color.yellow;

  private Animator animator;
  private new Rigidbody2D rigidbody;
  private EchoEffect echoEffect;

  private bool flipped = false;
  private bool dashing = false;
  private bool canDash = true;
  private float dashingCounter = 0f;

  private void Awake()
  {
    animator = GetComponent<Animator>();
    rigidbody = GetComponent<Rigidbody2D>();
    echoEffect = GetComponent<EchoEffect>();
  }

  override protected void Update()
  {
    base.Update();

    if (dashingCounter > 0)
    {
      dashingCounter = dashingCounter - Time.deltaTime;
    }

    foreach (SpriteRenderer spriteRender in GetComponentsInChildren<SpriteRenderer>())
    {
      if (dashing)
      {
        spriteRender.color = dashColor;
      }
      else
      {
        spriteRender.color = Color.white;
      }
    }
  }

  protected override void ComputeVelocity()
  {
    if (dashing && dashingCounter <= 0)
    {
      rigidbody.velocity /= 2;
      dashing = false;
      echoEffect.StopEffect();
    }

    if (!dashing)
    {
      if (grounded && !canDash && !dashing)
      {
        canDash = true;
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
      }

      Vector2 move = Vector2.zero;

      move.x = Input.GetAxis("Horizontal");

      if (Input.GetButtonDown("Jump") && grounded)
      {
        animator.SetTrigger("takeOff");
        velocity.y = jumpTakeOffSpeed;
      }
      else if (Input.GetButtonUp("Jump"))
      {
        if (velocity.y > 0)
        {
          velocity.y = velocity.y * 0.5f;
        }
      }

      if (move.x < -0.01f && !flipped)
      {
        FlipLeft();
      }
      else if (move.x > 0.01f && flipped)
      {
        FliptRight();
      }

      if (canDash && Input.GetButtonDown("Dash"))
      {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDir = mousePos - gameObject.transform.position;
        mouseDir.z = 0.0f;

        if (mousePos.x < transform.position.x && !flipped)
        {
          FlipLeft();
        }

        if (mousePos.x > transform.position.x && flipped)
        {
          FliptRight();
        }

        mouseDir = mouseDir.normalized;


        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(mouseDir * dashSpeed, ForceMode2D.Impulse);

        dashingCounter = dashTime;
        echoEffect.StartEffect();
        dashing = true;
        canDash = false;
      }

      animator.SetBool("grounded", grounded);
      animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
      animator.SetBool("dashing", dashing);

      targetVelocity = move * maxSpeed;
    }
  }

  private void FlipLeft()
  {
    transform.rotation = Quaternion.Euler(0, 180f, 0);
    flipped = true;
  }

  private void FliptRight()
  {
    transform.rotation = Quaternion.Euler(0, 0, 0);
    flipped = false;
  }
}