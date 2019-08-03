using UnityEngine;

public class SpearPickup : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerCombat>().PickUpSpear();
            Destroy(transform.root.gameObject);
        }
    }
}