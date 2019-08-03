using UnityEngine;

public class SpearPickup : MonoBehaviour {

    public GameObject forceFieldInstance;

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerCombat>().PickUpSpear();
            Destroy(forceFieldInstance.gameObject);
            Destroy(transform.root.gameObject);
        }
    }
}