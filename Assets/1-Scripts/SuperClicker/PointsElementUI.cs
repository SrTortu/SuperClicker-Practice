using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class PointsElementUI : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private TextMeshProUGUI _clicksText;
	[SerializeField] private float _duration = 3;
	
	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
		DoEffect();
    }

	private void DoEffect()
	{
		_clicksText.transform.Translate(Vector3.back);
		//Set Clicks text
		_clicksText.text = "+" + GameController.Instance.ClickRatio.ToString();

		//Movement
		transform.DOMoveY(transform.position.y + UnityEngine.Random.Range(100, 500), _duration);

		//Color Fade Out
		_clicksText.DOColor(new Color(0, 0, 0, 0), _duration);

		// Parámetros del movimiento sinusoidal
		GameController.Instance.Pool.AddToPool(this, _duration);
	}

	// Update is called once per frame
	public void Initialize(Transform transformClick)
    {
		_clicksText.color = Color.white;
		gameObject.SetActive(true);
		transform.parent = transformClick;
		transform.localPosition = Vector3.zero;
		DOTween.Kill(transform.position);
		DoEffect();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
