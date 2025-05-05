using UnityEngine;
using System;
using DG.Tweening;

public class Agent : MonoBehaviour
{
    #region Enum

    public enum AgentTypeEnum
    {
        Angel,
        Demon,
        Wizzard,
        Fairy
    }

    #endregion

    #region Properties

    public SlotButtonUI destiny { get; set; }
    [field: SerializeField] public AgentTypeEnum AgentType { get; set; }
    [field: SerializeField] public float RepeatRate { get; set; }

    #endregion

    #region Fields

    #endregion

    #region Unity Callbacks

    // Start is called before the first frame update
    void Start()
    {
        Movement();
        InvokeRepeating(nameof(Click), 1, RepeatRate);
        SlotButtonUI.OnSlotClicked += SetDestiny;
    }

    private void OnDestroy()
    {
        SlotButtonUI.OnSlotClicked -= SetDestiny;
    }

    private void SetDestiny(SlotButtonUI newDestiny)
    {
        destiny = newDestiny;
        Movement();
    }

    private void Click()
    {
        destiny.Click(1, true);

        if (AgentType == AgentTypeEnum.Angel)
        {
            if (destiny.ClicksLeft < 0)
                Destroy(gameObject);
        }
    }

    #endregion
    

    #region Private Methods
    protected void Movement()
    {
        if(destiny != null)
        transform.DOMove(destiny.transform.position, 1);
    }

    #endregion
}