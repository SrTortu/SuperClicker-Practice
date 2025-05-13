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
    private AudioSource _audioSource;

    #endregion

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnSlotClicked?.Invoke());
        OnSlotClicked += PlayResetSound;
    }

    #region Private Methods

    private void PlayResetSound()
    {
        _audioSource.Play();
    }

    #endregion
}