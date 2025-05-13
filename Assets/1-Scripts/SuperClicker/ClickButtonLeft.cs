using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButtonLeft : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Events

    public static event Action<SlotButtonUI> OnRightClick;

    #endregion

    #region Fields

    private SlotButtonUI _slotButton;

    private bool isPointerDown = false;
    private float pointerDownTime = 0f;
    private float holdThreshold = .5f; // 1 segundo para pulsación larga

    #endregion

    #region Unity Methods

    void Start()
    {
        _slotButton = GetComponent<SlotButtonUI>();
    }

    void Update()
    {
        if (Application.isMobilePlatform && isPointerDown)
        {
            if (Time.time - pointerDownTime >= holdThreshold)
            {
                isPointerDown = false; // evita múltiples activaciones
                OnRightClick?.Invoke(_slotButton);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Solo para PC con botón derecho
        if (eventData.button == PointerEventData.InputButton.Right && !Application.isMobilePlatform)
        {
            OnRightClick?.Invoke(_slotButton);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Application.isMobilePlatform)
        {
            isPointerDown = true;
            pointerDownTime = Time.time;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Application.isMobilePlatform)
        {
            isPointerDown = false;
        }
    }

    #endregion
}