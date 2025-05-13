using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchivementsUIManager : MonoBehaviour
{
    #region Propherties

    #endregion

    #region Fields

    [SerializeField] private TMP_Text _achivementDescription;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private Image _icon;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        AchievementManager.OnAchievementUnlocked += setAchivementTitle;
    }

    #endregion

    #region Private Methods

    private void setAchivementTitle(string title)
    {
        _achivementDescription.text = title;
        AchivementAppear();
    }

    private void AchivementAppear()
    {
        float duration = 3f;
        float delay = 1f;
        _icon.DOFade(0.5f, duration);
        _achivementDescription.DOFade(1f, duration);
        _titleText.DOFade(1f, duration).OnComplete(() =>
        {
            // Esperar un momento y hacer fade out
            DOVirtual.DelayedCall(delay, () =>
            {
                _icon.DOFade(0f, duration).SetEase(Ease.Linear);
                _achivementDescription.DOFade(0f, duration).SetEase(Ease.Linear);
                _titleText.DOFade(0f, duration).SetEase(Ease.Linear);
            });
        });
    }
    

    #endregion
}