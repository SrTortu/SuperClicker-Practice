using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    private float radius = 150f;
    private float speed = 4f;
    private float angle = 0;
    private bool _isOrbiting = true;
    private bool _isClicked = false;
    private bool _isWizardMove = true;
    private WizardClickController _wizardClickController;
    private Vector3 _mousePos;

    #endregion

    #region Unity Callbacks

    // Start is called before the first frame update
    void Start()
    {
        Movement();
        InvokeRepeating(nameof(Click), 1, RepeatRate);
        SlotButtonUI.OnSlotClicked += SetDestiny;
        ClickButtonLeft.OnRightClick += RightClick;
        FairyResetButton.OnSlotClicked += FairyReturn;
        WizardInitalite();
    }

    private void Update()
    {
        if (AgentType == AgentTypeEnum.Fairy)
        {
            if (_isOrbiting)
                OrbitAroundMouse();
        }
    }

    private void OnDestroy()
    {
        SlotButtonUI.OnSlotClicked -= SetDestiny;
    }

    #endregion

    #region Private Methods

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
            destiny.Click((int)(GameController.Instance.ClickRatio * 0.1), true);
        }

        if (AgentType == AgentTypeEnum.Demon)
        {
            destiny.Click((int)(GameController.Instance.ClickRatio * 2), true);
            if (destiny.ClicksLeft < 0)
                Destroy(gameObject);
        }

        if (AgentType == AgentTypeEnum.Fairy)
        {
            if (!_isOrbiting)
                destiny.Click((int)(GameController.Instance.ClickRatio * 2), true);
        }

        if (AgentType == AgentTypeEnum.Wizzard)
        {
            if (_isClicked)
            {
                if (_isWizardMove)
                {
                    destiny = GameController.Instance.GetLowSlotButtonUI();
                    Movement();
                    _isWizardMove = false;
                }

                destiny.Click((int)(GameController.Instance.ClickRatio * 2), true);
                if (!destiny.IsUsable || destiny == null)
                    Destroy(gameObject);
            }
        }
    }

    private void RightClick(SlotButtonUI slotButtonUI)
    {
        if (AgentType == AgentTypeEnum.Fairy)
        {
            if (_isOrbiting)
            {
                _isOrbiting = false;
                destiny = slotButtonUI;
                Movement();
            }
        }
    }


    protected void Movement()
    {
        if (destiny != null)
            transform.DOMove(destiny.transform.position, 1);
    }

    private void OrbitAroundMouse()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Incrementar el ángulo para el movimiento circular
        angle += Random.Range(0, speed) *
                 Time.deltaTime;

        // Calcular la nueva posición del agente en un círculo alrededor del mouse
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(_mousePos.x + x, _mousePos.y + y, 0f);
    }

    private void FairyReturn()
    {
        _isOrbiting = true;
    }

    private void WizardInitalite()
    {
        if (AgentType == AgentTypeEnum.Wizzard)
        {
            _wizardClickController = GetComponent<WizardClickController>();
            _wizardClickController.OnClick += () =>
            {
                _isClicked = true;
                Click();
            };
        }
    }

    #endregion
}