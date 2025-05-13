using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip _clickSound;
    private AudioSource _audioSource;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region Public Methods

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    #endregion
}