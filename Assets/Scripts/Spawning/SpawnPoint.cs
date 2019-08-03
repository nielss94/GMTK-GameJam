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

    public void Spawn(GameObject enemy)
    {
        available = false;
        StartCoroutine(WaitAndSpawn(enemy));
    }

    private IEnumerator WaitAndSpawn(GameObject enemy)
    {
        print("ASD1");
        yield return new WaitForSeconds(2f);
        print("ASD2");
        Instantiate(enemy, transform.position, Quaternion.identity);
        available = true;
    }
}
