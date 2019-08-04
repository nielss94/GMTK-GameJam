using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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

  public bool CanMove = true;
  public bool flipped = false;
  private bool dashing = false;
  private bool canDash = true;
  private float dashingCounter = 0f;

  private PlayerCombat playerCombat;
  private CinemachineImpulseSource impulseSource;
  private PlayerDeath playerDeath;
  private PlayerSFXPlayer playerSFXPlayer;

  private void Awake()
  {
    animator = GetComponent<Animator>();
    rigidbody = GetComponent<Rigidbody2D>();
    echoEffect = GetComponent<EchoEffect>();
    playerCombat = GetComponent<PlayerCombat>();
    impulseSource = GetComponent<CinemachineImpulseSource>();
    playerDeath = GetComponent<PlayerDeath>();
    playerSFXPlayer = GetComponent<PlayerSFXPlayer>();
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
    if (CanMove)
    {
      if (dashing && dashingCounter <= 0)
      {
        rigidbody.velocity /= 2;
        dashing = false;
        echoEffect.StopEffect();
        playerDeath.CanDie = true;
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
          playerSFXPlayer.PlayJump();
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

        if (move.x < -0.01f && !flipped && !playerCombat.chargingSpear)
        {
          FlipLeft();
        }
        else if (move.x > 0.01f && flipped && !playerCombat.chargingSpear)
        {
          FliptRight();
        }

        if (canDash && Input.GetButtonDown("Dash"))
        {
          var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          var mouseDir = mousePos - gameObject.transform.position;
          mouseDir.z = 0.0f;

          if (mousePos.x < transform.position.x && !flipped && !playerCombat.chargingSpear)
          {
            FlipLeft();
          }

          if (mousePos.x > transform.position.x && flipped && !playerCombat.chargingSpear)
          {
            FliptRight();
          }

          mouseDir = mouseDir.normalized;

          rigidbody.velocity = Vector2.zero;
          rigidbody.AddForce(mouseDir * dashSpeed, ForceMode2D.Impulse);

          dashingCounter = dashTime;
          echoEffect.StartEffect();
          dashing = true;
          playerDeath.CanDie = false;
          canDash = false;
          impulseSource.GenerateImpulse();
          playerSFXPlayer.PlayDash();
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetBool("dashing", dashing);

        targetVelocity = move * maxSpeed;
      }
    }
  }

  public void FlipLeft()
  {
    transform.rotation = Quaternion.Euler(0, 180f, 0);
    flipped = true;
  }

  public void FliptRight()
  {
    transform.rotation = Quaternion.Euler(0, 0, 0);
    flipped = false;
  }
}