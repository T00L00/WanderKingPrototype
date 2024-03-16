using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIList : MonoBehaviour
{
    [SerializeField] private UIListItem listItemPrefab;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform listContainer;
    [SerializeField] private Image background;
    
    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public void Show()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        ClearList();
    }
    
    public void AddItem(string text, System.Action onClick)
    {
        UIListItem item = Instantiate(listItemPrefab, listContainer);
        item.SetOnClick(onClick);
        item.SetDescription(text);
        item.gameObject.SetActive(true);
    }
    
    private void Awake()
    {
        if (listItemPrefab == null && listContainer.childCount > 0)
        {
            listItemPrefab = listContainer.GetChild(0).GetComponent<UIListItem>();
        }
        listItemPrefab.gameObject.SetActive(false);
        ClearList();
    }

    private void ClearList()
    {
        for (int i = listContainer.childCount - 1; i >= 0; i--)
        {
            if (listContainer.GetChild(i) == listItemPrefab.transform) continue;
            Destroy(listContainer.GetChild(i).gameObject);
        }
    }
}
