using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DifficultyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Difficulty difficulty;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(difficulty == Difficulty.Hard)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "ARE YOU \n SURE?";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(difficulty == Difficulty.Hard)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "HARD";
        }
    }
}