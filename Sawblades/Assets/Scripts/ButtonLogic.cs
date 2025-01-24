using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLogic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text buttonText;

    void Start (){
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        buttonText.text = "> " + buttonText.text + " <";
        buttonText.color = Color.black;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        buttonText.text = buttonText.text.Substring(2, buttonText.text.Length - 4);
        buttonText.color = new Color(50f/255f, 50f/255f, 50/255f, 1);
    }
}
