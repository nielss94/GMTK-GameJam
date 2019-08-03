using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Spear spear;
    public GameObject gearedSpear;

    [SerializeField]private Transform spearSpawnpoint;

    [SerializeField]private bool wearingSpear;
    public bool chargingSpear;
    
    [SerializeField]private float chargedForce;
    [SerializeField]private float timeToMaxCharge;
    [SerializeField]private float throwForce = 30;

    private PlayerPlatformerController playerController;

    [SerializeField]private SpriteRenderer throwingHand;
    [SerializeField]private SpriteRenderer leftHand;

    private Spear currentSpear;

    private void Awake() {
        playerController = GetComponent<PlayerPlatformerController>();
    }

    private void Update() {
        if(!chargingSpear && wearingSpear)
        {
            chargingSpear = Input.GetButton("Fire1");
        }

        if(wearingSpear && chargingSpear)
        {
            currentSpear = Instantiate(spear, transform.position, Quaternion.identity);
            wearingSpear = false;
            gearedSpear.SetActive(false);
        }
        

        if(!wearingSpear && chargingSpear && currentSpear != null)
        {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            currentSpear.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            currentSpear.transform.position = spearSpawnpoint.position;
            HideLeftHand();
            if(Input.GetButtonUp("Fire1"))
            {
                currentSpear.Throw(dir, throwForce);
                currentSpear = null;
                chargingSpear = false;
                ShowLeftHand();
            }
        }

        if(Input.GetButton("Fire1"))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x < transform.position.x && !playerController.flipped && chargingSpear)
            {
                playerController.FlipLeft();
            }
            else if (mousePos.x > transform.position.x && playerController.flipped && chargingSpear)
            {
                playerController.FliptRight();
            }
        }   
    }

    public void ShowLeftHand()
    {
        throwingHand.enabled = false;
        leftHand.enabled = true;
    }

    public void HideLeftHand(){
        throwingHand.enabled = true;
        leftHand.enabled = false;
    }

    public void PickUpSpear()
    {
        gearedSpear.SetActive(true);
        wearingSpear = true;
    }
}
