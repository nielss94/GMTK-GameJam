using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private void Awake() {
        portal.localScale = Vector3.zero;
    }

    private void OnValidate() {
        portal = transform.GetChild(0);
        portal.GetComponent<MeshRenderer>().sortingOrder = 15;
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
