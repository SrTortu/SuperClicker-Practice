using UnityEngine;
using System;
using DG.Tweening;
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
    private bool isOrbiting = true;
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
    }

    private void Update()
    {
        if (AgentType == AgentTypeEnum.Fairy)
        {
            if (isOrbiting)
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
            if(!isOrbiting)
            destiny.Click((int)(GameController.Instance.ClickRatio * 2), true);
        }
    }

    private void RightClick(SlotButtonUI slotButtonUI)
    {
        if (AgentType == AgentTypeEnum.Fairy)
        { 
            if (isOrbiting)
            {
                isOrbiting = false;
                destiny = slotButtonUI;
                Movement();
                Debug.Log("RightClick");
                
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
        // Obtener la posición del mouse en el mundo 2D
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0f; // Asegurarnos de que la coordenada Z sea 0 para 2D

        // Incrementar el ángulo para el movimiento circular
        angle += Random.Range(0, speed) *
                 Time.deltaTime; // `Time.deltaTime` para un movimiento consistente en el tiempo

        // Calcular la nueva posición del agente en un círculo alrededor del mouse
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Actualizar la posición del agente (en un círculo alrededor del mouse)
        transform.position = new Vector3(_mousePos.x + x, _mousePos.y + y, 0f);
    }

    private void FairyReturn()
    {
        isOrbiting = true;
    }
    #endregion
}