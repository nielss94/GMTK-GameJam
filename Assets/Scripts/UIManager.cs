using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  [SerializeField] private GameObject gameOverPanel;
  [SerializeField] private CanvasGroup gameOverTitleGroup;
  [SerializeField] private CanvasGroup gameOverCountGroup;
  [SerializeField] private CanvasGroup gameOverInputGroup;
  [SerializeField] private TextMeshProUGUI gameOverKillCounter;

  [SerializeField] private GameObject gamePanel;
  [SerializeField] private TextMeshProUGUI gameKillCounter;
  [SerializeField] private Image killIcon;

  private GameManager gameManager;
  private PlayerDeath playerDeath;

  private bool isGameOver = false;

  private void Awake()
  {
    playerDeath = FindObjectOfType<PlayerDeath>();
  }

  private void Update()
  {
    if (isGameOver)
    {
      if (Input.GetKeyDown(KeyCode.R))
      {
        LevelLoadManager.Instance.LoadGame(gameManager.gameMode);
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
        LevelLoadManager.Instance.LoadMainMenu();
      }

    }
  }

  private void Start()
  {
    gameManager = GameManager.Instance;
    gameManager.OnScoreChanged += ScoreChanged;
    playerDeath.OnPlayerDeath += PlayGameOver;
  }

  private void ScoreChanged(int score)
  {
    gameOverKillCounter.text = score + "";
    gameKillCounter.text = score + "";


    Sequence sequence = DOTween.Sequence();
    sequence.Append(killIcon.rectTransform.DOScale(new Vector2(1.2f, 1.2f), 0.5f));
    sequence.Append(killIcon.rectTransform.DOScale(new Vector2(1f, 1f), 0.5f));
    sequence.Play();
  }

  private void PlayGameOver()
  {
    isGameOver = true;
    StartCoroutine(ShowGameOver());
  }

  private IEnumerator ShowGameOver()
  {
    TextMeshProUGUI gameOverText = gameOverTitleGroup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    gameOverText.fontSize = 0;
    gameOverTitleGroup.alpha = 0;
    gameOverInputGroup.alpha = 0;
    gameOverCountGroup.alpha = 0;
    gameOverPanel.SetActive(true);
    gamePanel.SetActive(false);

    yield return new WaitForSeconds(1.25f);

    Sequence sequence = DOTween.Sequence();
    sequence.Append(gameOverTitleGroup.DOFade(1, 1.25f));
    sequence.Join(gameOverText.DOFontSize(110, 1.25f));
    sequence.Join(gameOverCountGroup.DOFade(1, 2.5f));
    sequence.Append(gameOverInputGroup.DOFade(1, 1.5f));
    sequence.Play();
  }
}
