using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.LWRP;


public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class SpawnPoint : MonoBehaviour
{
    public Difficulty spawnLevel;
    public bool available;

    private Transform portal;
    private new Light2D light;

    private void Awake() {
        portal.localScale = Vector3.zero;
        light.intensity = 0;
    }

    private void OnValidate() {
        portal = transform.GetChild(0);
        portal.GetComponent<MeshRenderer>().sortingLayerName = "Portal";
        light = portal.GetChild(0).GetComponent<Light2D>();
    }

    private void Update() {
        if(light.intensity > 0 && available)
        {
            light.intensity = Mathf.Lerp(light.intensity, 0, 2 * Time.deltaTime);
        }

        if(light.intensity < 1 && !available)
        {
            light.intensity = Mathf.Lerp(light.intensity, 1, 2 * Time.deltaTime);
        }
    }

    public void Spawn(Enemy enemy)
    {
        available = false;
        StartCoroutine(WaitAndSpawn(enemy));
    }

    private IEnumerator WaitAndSpawn(Enemy enemy)
    {
        portal.DOScale(new Vector3(2,2,2), .7f);
        yield return new WaitForSeconds(1f);
        Instantiate(enemy, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        portal.DOScale(new Vector3(0,0,0), .7f);
        available = true;
    }
}
