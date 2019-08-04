using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour
{
    public static LevelLoadManager Instance;

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
            return;
        }
    }

    public void LoadGame(Difficulty difficulty)
    {
        StartCoroutine(LoadGameAsync(difficulty));
    }

    private IEnumerator LoadGameAsync(Difficulty difficulty)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");

        while(!asyncLoad.isDone)
        {
            yield return null;
        }

        GameManager.Instance.SetGameMode(difficulty);
    }


}
