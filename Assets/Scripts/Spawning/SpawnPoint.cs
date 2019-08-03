using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnLevel
{
    Easy,
    Medium,
    Hard
}

public class SpawnPoint : MonoBehaviour
{
    public SpawnLevel spawnLevel;
    public bool available;

    public void Spawn(Enemy enemy)
    {
        available = false;
        StartCoroutine(WaitAndSpawn(enemy));
    }

    private IEnumerator WaitAndSpawn(Enemy enemy)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(enemy, transform.position, Quaternion.identity);
        available = true;
    }
}
