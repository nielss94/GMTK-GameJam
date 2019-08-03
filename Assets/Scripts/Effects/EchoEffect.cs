using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
  [SerializeField] private float timeBetweenSpawns = 0.25f;
  [SerializeField] private float startTimeBetweenSpawns = 0.25f;

  [SerializeField] private GameObject echo = null;

  private bool doEffect = false;

  private void Start()
  {
    timeBetweenSpawns = 0;
  }

  private void Update()
  {
    if (doEffect)
    {
      if (timeBetweenSpawns <= 0)
      {
        Instantiate(echo, transform.position, Quaternion.identity);
        timeBetweenSpawns = startTimeBetweenSpawns;
      }
      else
      {
        timeBetweenSpawns -= Time.deltaTime;
      }
    }
  }

  public void StartEffect()
  {
    doEffect = true;
  }

  public void StopEffect()
  {
    doEffect = false;
  }
}
