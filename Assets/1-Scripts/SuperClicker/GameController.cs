using UnityEngine;
using System;
using System.Collections;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

public class GameController : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public float ClickRatio { get; set; }
    public static GameController Instance { get; private set; }
    [field: SerializeField] public PoolSystem Pool { get; set; }

    #endregion

    #region Fields

    [SerializeField] private Agent[] _agents;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _clicksText;
    [SerializeField] private ParticleSystem _particlesRain;
    [SerializeField] private SlotButtonUI[] _slotButtons;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SlotButtonUI.OnSlotReward += GetReward;
        StartCoroutine(InstanciateWizzard());
    }

    private void OnDestroy()
    {
        SlotButtonUI.OnSlotReward -= GetReward;
    }

    #endregion

    #region Public Methods

    public void RainParticles()
    {
        _particlesRain.Emit(Mathf.Clamp((int)ClickRatio, 0, 13));
    }

    public SlotButtonUI GetLowSlotButtonUI()
    {
        Debug.Log(_slotButtons.Length);
        SlotButtonUI slotButtonUI = null;
        for (int i = 0; i < _slotButtons.Length; i++)
        {
            if (_slotButtons[i].IsUsable)
            {
                slotButtonUI = _slotButtons[i];
                Debug.Log(slotButtonUI);
                break;
            }
        }

        return slotButtonUI;
    }

    #endregion

    #region Private Methods

    /***
     *       ____    U _____ u                 _       ____     ____    _
     *    U |  _"\ u \| ___"|/__        __ U  /"\  uU |  _"\ u |  _"\ U|"|u
     *     \| |_) |/  |  _|"  \"\      /"/  \/ _ \/  \| |_) |//| | | |\| |/
     *      |  _ <    | |___  /\ \ /\ / /\  / ___ \   |  _ <  U| |_| |\|_|
     *      |_| \_\   |_____|U  \ V  V /  U/_/   \_\  |_| \_\  |____/ u(_)
     *      //   \\_  <<   >>.-,_\ /\ /_,-. \\    >>  //   \\_  |||_   |||_
     *     (__)  (__)(__) (__)\_)-'  '-(_/ (__)  (__)(__)  (__)(__)_) (__)_)
     */
    private void GetReward(Reward reward)
    {
        ShowReward(reward);

        //Apply rewards
        if (reward.RewardType == RewardType.Plus)
        {
            ClickRatio += reward.Value;
            _clicksText.text = "x" + ClickRatio;
            return;
        }

        if (reward.RewardType == RewardType.Multi)
        {
            ClickRatio *= reward.Value;
            _clicksText.text = "x" + ClickRatio;
            return;
        }

        if (reward.RewardType == RewardType.Agent)
        {
            Agent newAgent = Instantiate(_agents[(int)reward.Value], transform.position, Quaternion.identity);
            newAgent.destiny = reward.ObjectReward;

            return;
        }
    }

    private void ShowReward(Reward reward)
    {
        //Initialziation
        if (!_rewardText.gameObject.activeSelf)
        {
            _rewardText.gameObject.SetActive(true);
            _rewardText.transform.localScale = Vector3.zero;
        }

        //Update text
        _rewardText.text = "REWARD\n " + reward.RewardType + reward.Value + " Clicks";

        // Crear una secuencia
        Sequence mySequence = DOTween.Sequence();

        // A�adir el primer efecto de escala
        mySequence.Append(_rewardText.transform.DOScale(1, 1));

        // A�adir el efecto de sacudida en la rotaci�n
        mySequence.Append(_rewardText.transform.DOShakeRotation(1, new Vector3(0, 0, 30)));

        // A�adir el segundo efecto de escala
        mySequence.Append(_rewardText.transform.DOScale(0, 1));

        // Iniciar la secuencia
        mySequence.Play();
    }

    IEnumerator InstanciateWizzard()
    {
        while(true)
        {
            Instantiate(_agents[3], GetRandomPosition(), Quaternion.identity); 
            yield return new WaitForSeconds(2f);
        }
            
        
    }
    private Vector2 GetRandomPosition()
    {
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float minX = Camera.main.transform.position.x - cameraWidth / 2f;
        float maxX = Camera.main.transform.position.x + cameraWidth / 2f;
        float minY = Camera.main.transform.position.y - cameraHeight / 2f;
        float maxY = Camera.main.transform.position.y + cameraHeight / 2f;

        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        gameObject.transform.position = randomPosition;
        
        return randomPosition;
    }

    #endregion
}