using UnityEngine;

[CreateAssetMenu(fileName = "SpawnOptions", menuName = "GMTK-GameJam/SpawnOptions", order = 0)]
public class SpawnOptions : ScriptableObject {
    
    public float minSpawnCooldown = 2f;
    public float maxSpawnCooldown = 10f;

    public float minEnemiesToSpawn = 1f;
    public float maxEnemiesToSpawn = 10f;

    public float difficultyMultiplier;

    public GameObject[] enemies;
}