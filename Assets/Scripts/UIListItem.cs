using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    public void SetOnClick(Action handleClick)
    {
        GetComponent<Button>().onClick.AddListener(() => {
            handleClick();
        });
    }
    
    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }
}
