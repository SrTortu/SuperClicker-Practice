using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButtonLeft : MonoBehaviour, IPointerClickHandler
{
    #region Propherties
    
    public static event Action OnRightClick;

    #endregion
    #region Fields

    private Button button;

    #endregion

    #region Unity Methods

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick?.Invoke();
        }
    }

    #endregion
}