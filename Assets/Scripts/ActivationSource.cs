using System;
using UnityEngine;

public class ActivationSource : MonoBehaviour {
    public static Action<ActivationSource, ActivationInstigator> OnEnterFocus;
    public static Action<ActivationSource, ActivationInstigator> OnExitFocus;
    
    [SerializeField] private GameObject activationIndicator;
    [SerializeField] private GameObject dataSource;

    public string DisplayName => dataSource.name;
    
    private void Awake()
    {
        HideIndicator();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ActivationInstigator instigator))
        {
            OnEnterFocus?.Invoke(this, instigator);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ActivationInstigator instigator))
        {
            OnExitFocus?.Invoke(this, instigator);
        }
    }
    
    public void ShowFocusIndicator()
    {
        activationIndicator.SetActive(true);
    }
    
    public void ShowActivationIndicator()
    {
        activationIndicator.SetActive(true);
    }
    
    public void HideIndicator()
    {
        activationIndicator.SetActive(false);
    }
}