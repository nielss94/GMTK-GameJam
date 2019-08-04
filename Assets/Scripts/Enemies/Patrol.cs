using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Patrol : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 4f;
  [SerializeField] private float groundDetectionDistance = 2f;
  [SerializeField] private float wallDetectionDistance = 0.2f;
  [SerializeField] private Transform detection = null;
  [SerializeField] private Vector2 playerDetectionDistance = new Vector2(4f, 4f);
  [SerializeField] private float playerDetectionRange = 5f;
  [SerializeField] private float timeUntilNewPatrol = 2f;
  [SerializeField, Range(0, 1)] private float seePlayerSpeedIncrease;

  private Transform target;
  private bool movingRight = false;
  private float timeUntilNewPatrolCounter = 0f;

  private Animator animator;

  private bool spawning = true;

  private void Start()
  {
    animator = GetComponentInChildren<Animator>();
    StartCoroutine(SetupBeforeSpawning());
  }

  private void OnDrawGizmos()
  {
    //Debug.DrawRay(detection.position, movingRight ? Vector2.right * playerDetectionRange : Vector2.left * playerDetectionRange, Color.green);
  }

  private void Update()
  {
    if(!spawning)
    {
      RaycastHit2D playerHit = Physics2D.Raycast(detection.position, movingRight ? Vector2.right : Vector2.left, playerDetectionRange);
      if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
      {
        target = playerHit.collider.transform;
        timeUntilNewPatrolCounter = timeUntilNewPatrol;
      }
      else
      {
        if (target != null)
        {
          if (timeUntilNewPatrolCounter <= 0)
          {
            target = null;
          }
          else
          {
            timeUntilNewPatrolCounter -= Time.deltaTime;
          }
        }
      }

      RaycastHit2D wallHit = Physics2D.Raycast(detection.position, movingRight ? Vector2.right : Vector2.left, wallDetectionDistance);


      if (target == null)
      {
        RaycastHit2D groundHit = Physics2D.Raycast(detection.position, Vector2.down, groundDetectionDistance);
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

      if (!(target != null && (wallHit.collider != null && wallHit.collider.CompareTag("Level"))))
      {
        transform.Translate(Vector2.right * moveSpeed * (target != null ? 1 + seePlayerSpeedIncrease : 1) * Time.deltaTime);
        animator.SetTrigger("Walking");
      }
      else
      {
        animator.SetTrigger("Idle");
      }
    }
  }

  private IEnumerator SetupBeforeSpawning()
  {
    Collider2D collider = GetComponent<Collider2D>();
    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

    float oldGravityScale = rigidbody.gravityScale;
    collider.enabled = false;
    rigidbody.gravityScale = 0;

    Sequence scaleSequence = DOTween.Sequence();

    scaleSequence.Append(transform.DOScale(1.2f, .6f))
      .Append(transform.DOScale(1f, .4f));

    scaleSequence.Play();

    GetComponentInChildren<SpriteRenderer>().DOFade(1, 1f);
    
    yield return scaleSequence.WaitForKill();

    animator.SetTrigger("Walking");
    collider.enabled = true;
    rigidbody.gravityScale = oldGravityScale;
    spawning = false;
  }
}
