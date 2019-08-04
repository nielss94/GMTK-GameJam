using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonMapper : MonoBehaviour
{
    private void Start() {
        foreach (var item in GetComponentsInChildren<Button>())
        {
            item.onClick.AddListener(() => {
                LevelLoadManager.Instance?.LoadGame(item.transform.GetComponent<DifficultyButton>().difficulty);
            });
        }
    }
}
