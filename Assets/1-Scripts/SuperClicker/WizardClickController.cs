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
    private static bool _AchievementMagicLife = true;
    private AudioSource _audioSource;

    #endregion

    #region Unity Callbacks

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        
        OnClick?.Invoke();
        _audioSource.Play();
        Destroy(_boxCollider2D);
        if (_AchievementMagicLife)
        {
            _AchievementMagicLife = false;
            AchievementManager.Instance.UnlockAchievement((int)AchivementId.magicLife);
        }
    }

    #endregion
}