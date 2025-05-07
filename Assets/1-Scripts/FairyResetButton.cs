using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FairyResetButton : MonoBehaviour
{
    #region Propherties
    
    public static event Action OnSlotClicked;
    
    #endregion
    #region Fields

    private Button _button;

    #endregion
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Click);
    }

    private void Click()
    {
        OnSlotClicked?.Invoke();
    }

}
