using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControlButton : MonoBehaviour
{
  [SerializeField] private GameObject mainGameObject;
  [SerializeField] private GameObject controlsGameObject;

  public void ShowControls()
  {
    CanvasGroup mainGroup = mainGameObject.GetComponent<CanvasGroup>();
    CanvasGroup controlsGroup = controlsGameObject.GetComponent<CanvasGroup>();

    controlsGroup.alpha = 0;
    controlsGameObject.SetActive(true);

    Sequence sequence = DOTween.Sequence();
    sequence.Append(mainGroup.DOFade(0, 0.5f));
    sequence.Append(controlsGroup.DOFade(1, 0.5f));

    StartCoroutine(SetUnActiveAfter(mainGameObject, 0.5f));
  }

  private IEnumerator SetUnActiveAfter(GameObject gameObject, float seconds)
  {
    yield return new WaitForSeconds(seconds);

    gameObject.SetActive(false);
  }
}
