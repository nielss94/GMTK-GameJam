using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityForce : MonoBehaviour
{
    public float waitTime = 2;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(EnableAfterSeconds(waitTime));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            GetComponent<AreaEffector2D>().enabled = false;
        }
    }

    IEnumerator EnableAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<AreaEffector2D>().enabled = true;
    }
}
