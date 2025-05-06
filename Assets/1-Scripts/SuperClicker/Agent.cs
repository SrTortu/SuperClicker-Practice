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
        if (AgentType == AgentTypeEnum.Angel)
        {
            destiny = newDestiny;
            Movement();
        }
    }

    private void Click()
    {
        if (AgentType == AgentTypeEnum.Angel)
        {
            destiny.Click((int)(GameController.Instance.ClickRatio*0.1), true);
        }

        if (AgentType == AgentTypeEnum.Demon)
        {
            destiny.Click((int)(GameController.Instance.ClickRatio*2), true);
            if (destiny.ClicksLeft < 0)
                Destroy(gameObject);
        }
    }

    #endregion


    #region Private Methods

    protected void Movement()
    {
        if (destiny != null)
            transform.DOMove(destiny.transform.position, 1);
    }

    #endregion
}