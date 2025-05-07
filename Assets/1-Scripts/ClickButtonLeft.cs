using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButtonLeft : MonoBehaviour, IPointerClickHandler
{
    #region Propherties
    
    public static event Action<SlotButtonUI> OnRightClick;

    #endregion
    #region Fields

    private Button _button;
    private SlotButtonUI _slotButton;

    #endregion

    #region Unity Methods

    void Start()
    {
        _button = GetComponent<Button>();
        _slotButton = GetComponent<SlotButtonUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick?.Invoke(_slotButton);
        }
    }

    #endregion
}