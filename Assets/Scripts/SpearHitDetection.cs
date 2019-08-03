using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHitDetection : MonoBehaviour
{
    [SerializeField]private SpearPickup spearPickup;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            GetComponentInParent<Spear>().TurnOff();
            spearPickup.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
