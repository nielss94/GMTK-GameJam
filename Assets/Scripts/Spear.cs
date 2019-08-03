using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float gravityScale = 3;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Throw(Vector3 dir, float force)
    {
        GetComponent<PolygonCollider2D>().enabled = true;
        transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        rigidbody.gravityScale = gravityScale;
        rigidbody.AddForce(dir.normalized * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Collision with monsters
    }

    public void TurnOff()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        rigidbody.gravityScale = 0;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;
    }
}
