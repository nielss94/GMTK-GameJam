using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 4f;
  [SerializeField] private float groundDetectionDistance = 2f;
  [SerializeField] private float wallDetectionDistance = 0.2f;
  [SerializeField] private Transform detection = null;
  [SerializeField] private float playerDetectionDistance = 3f;

  private Transform target;
  private bool movingRight = false;

  private Animator animator;

  private void Start()
  {
    animator = GetComponentInChildren<Animator>();

    animator.SetTrigger("Walking");
  }

  private void Update()
  {
    RaycastHit2D playerHit = Physics2D.Raycast(detection.position, Vector2.right, playerDetectionDistance);
    if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
    {
      target = playerHit.collider.transform;
    }

    if (target == null)
    {
      RaycastHit2D groundHit = Physics2D.Raycast(detection.position, Vector2.down, groundDetectionDistance);
      RaycastHit2D wallHit = Physics2D.Raycast(detection.position, Vector2.right, wallDetectionDistance);
      if (groundHit.collider == null || (wallHit.collider != null && wallHit.collider.CompareTag("Level")))
      {
        if (movingRight)
        {
          transform.eulerAngles = new Vector3(0, -180f, 0);
          movingRight = false;
        }
        else
        {
          transform.eulerAngles = new Vector3(0, 0, 0);
          movingRight = true;
        }
      }
    }
    else
    {
      if (target.position.x < transform.position.x)
      {
        if (movingRight)
        {
          transform.eulerAngles = new Vector3(0, -180f, 0);
          movingRight = false;
        }
      }

      if (target.position.x > transform.position.x)
      {
        transform.eulerAngles = new Vector3(0, 0, 0);
        movingRight = true;
      }
    }


    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
  }
}
