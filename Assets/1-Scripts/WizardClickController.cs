using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardClickController : MonoBehaviour
{
    #region Propherties
    
    public event Action OnClick;
    
    #endregion
    #region Fields

    private BoxCollider2D _boxCollider2D;

    #endregion

    #region Unity Callbacks

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        OnClick?.Invoke();
    }

    #endregion
}