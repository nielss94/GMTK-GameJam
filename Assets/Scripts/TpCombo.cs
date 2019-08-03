using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpCombo : MonoBehaviour
{
    private Transform destination;

    private void Awake() {
        destination = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Spear"))
        {
            other.transform.position = destination.position;
            other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}
