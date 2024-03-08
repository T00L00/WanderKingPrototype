using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private UIList uiList;
    
    private List<ActivationSource> activationSources = new List<ActivationSource>();

    public void OnActivateAction()
    {
        if (activationSources.Count == 0) return;
        ActivationSource source = activationSources[0];
        uiList.SetTitle(source.DisplayName);
        
        int amount = Random.Range(3, 5);
        for (int i = 0; i < amount; i++)
        {
            uiList.AddItem($"Item {i}", () => {
                Hide();
            });
        }

        Show();
    }
    
    public void CloseList()
    {
        Hide();    
    }
    
    private void OnEnable()
    {
        ActivationSource.OnEnterFocus += ActivationSource_OnEnterFocus;
        ActivationSource.OnExitFocus += ActivationSource_OnExitFocus;
    }

    private void OnDisable()
    {
        ActivationSource.OnEnterFocus -= ActivationSource_OnEnterFocus;
        ActivationSource.OnExitFocus -= ActivationSource_OnExitFocus;
    }

    private void Awake()
    {
        Hide();    
    }

    private void ActivationSource_OnEnterFocus(ActivationSource source, ActivationInstigator instigator)
    {
        activationSources.Add(source);
        source.ShowFocusIndicator();
    }

    private void ActivationSource_OnExitFocus(ActivationSource source, ActivationInstigator instigator)
    {
        activationSources.Remove(source);
        source.HideIndicator();
    }

    private void Show()
    {
        background.gameObject.SetActive(true);    
        uiList.Show();
    }

    private void Hide()
    {
        background.gameObject.SetActive(false);
        uiList.Hide();
    }
}
