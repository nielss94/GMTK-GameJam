using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Spear spear;
    public GameObject gearedSpear;

    [SerializeField] private Transform spearSpawnpoint = null;

    [SerializeField] private bool wearingSpear = true;
    public bool chargingSpear;

    [SerializeField] private float chargedForce;
    [SerializeField] private float timeToMaxCharge;
    [SerializeField] private float throwForce = 30;

    private PlayerPlatformerController playerController = null;

    [SerializeField] private SpriteRenderer throwingHand = null;
    [SerializeField] private SpriteRenderer leftHand = null;

    private Spear currentSpear;

    private Vector3 throwDirection;

    public bool CanThrow = true;

    private void Awake()
    {
        playerController = GetComponent<PlayerPlatformerController>();
    }

    private void Update()
    {
        if (CanThrow)
        {
            if (!chargingSpear && wearingSpear)
            {
                chargingSpear = Input.GetButton("Fire1");
            }

            if (wearingSpear && chargingSpear)
            {
                currentSpear = Instantiate(spear, transform.position, Quaternion.identity);
                wearingSpear = false;
                gearedSpear.SetActive(false);
            }

            if (!wearingSpear && chargingSpear && currentSpear != null)
            {
                throwDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                var angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg - 90;
                currentSpear.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                currentSpear.transform.position = spearSpawnpoint.position;
                HideLeftHand();
                if (Input.GetButtonUp("Fire1"))
                {
                    currentSpear.Throw(throwDirection, throwForce);
                    currentSpear = null;
                    chargingSpear = false;
                    ShowLeftHand();
                }
            }

            if (Input.GetButton("Fire1"))
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
    }

    public void Throw()
    {
        currentSpear.Throw(throwDirection, throwForce);
        currentSpear = null;
        chargingSpear = false;
        ShowLeftHand();
    }

    public void ShowLeftHand()
    {
        throwingHand.enabled = false;
        leftHand.enabled = true;
    }

    public void HideLeftHand()
    {
        throwingHand.enabled = true;
        leftHand.enabled = false;
    }

    public void PickUpSpear()
    {
        gearedSpear.SetActive(true);
        wearingSpear = true;
    }
}