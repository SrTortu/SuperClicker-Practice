using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Achievements/Achievement")]
public class Achivement : ScriptableObject
{
    #region Propherties
    
    public int id;
    public string title;
    public bool isUnlocked;

    #endregion
}
