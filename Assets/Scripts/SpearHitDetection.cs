using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHitDetection : MonoBehaviour
{
    [SerializeField]private SpearPickup spearPickup = null;
    [SerializeField]private GameObject onHitEffect = null;

    [SerializeField]private GameObject forceField;
    [SerializeField]private GameObject forceFieldInstance;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            GetComponentInParent<Spear>().TurnOff();
            Instantiate(onHitEffect, other.GetContact(0).point, Quaternion.identity);
            spearPickup.gameObject.SetActive(true);

            forceFieldInstance = Instantiate(forceField, other.GetContact(0).point, Quaternion.identity);
            spearPickup.GetComponent<SpearPickup>().forceFieldInstance = forceFieldInstance;

            gameObject.SetActive(false);
        }
    }
}
