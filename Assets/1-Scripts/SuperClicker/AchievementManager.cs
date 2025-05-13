using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    #region Propherties

    public static AchievementManager Instance { get; set; }
    public static event Action<string> OnAchievementUnlocked;

    #endregion

    #region Fields

    [SerializeField] private List<Achivement> _achievementsPool;
    private AudioSource _audioSource;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region Public Methods

    public void UnlockAchievement(int id)
    {
        Achivement achivement = GetAchievement(id);
        OnAchievementUnlocked?.Invoke(achivement.title);
        _audioSource.Play();
    }

    #endregion

    #region Private Methods

    private Achivement GetAchievement(int id)
    {
        Achivement achivement = _achievementsPool.Find(a => a.id == id);
        return achivement;
    }

    #endregion
}