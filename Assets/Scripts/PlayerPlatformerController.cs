using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 7;

  private Animator animator;

  private bool flipped = false;

  // Use this for initialization
  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  protected override void ComputeVelocity()
  {
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
      transform.localScale = new Vector3(-1, 1, 1);
      flipped = true;
    }
    else if (move.x > 0.01f && flipped)
    {
      transform.localScale = new Vector3(1, 1, 1);
      flipped = false;
    }

    animator.SetBool("grounded", grounded);
    animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

    targetVelocity = move * maxSpeed;
  }
}