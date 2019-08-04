using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackButton : MonoBehaviour
{
  [SerializeField] private GameObject mainGameObject;
  [SerializeField] private GameObject controlsGameObject;

  public void ShowMain()
  {
    CanvasGroup mainGroup = mainGameObject.GetComponent<CanvasGroup>();
    CanvasGroup controlsGroup = controlsGameObject.GetComponent<CanvasGroup>();

    mainGroup.alpha = 0;
    mainGameObject.SetActive(true);

    Sequence sequence = DOTween.Sequence();
    sequence.Append(controlsGroup.DOFade(0, 0.5f));
    sequence.Append(mainGroup.DOFade(1, 0.5f));

    StartCoroutine(SetUnActiveAfter(controlsGameObject, 0.5f));
  }

  private IEnumerator SetUnActiveAfter(GameObject gameObject, float seconds)
  {
    yield return new WaitForSeconds(seconds);

    gameObject.SetActive(false);
  }
}
