using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _timeCount;
    [SerializeField] private GameObject _endGameCanvas;

    private float targetValue = 0f;

    private void Start()
    {
    }
    private void Update()
    {
        if (_slider.value != targetValue)
        {
            _slider.value -= Time.deltaTime;
            _timeCount.text = Math.Round(_slider.value).ToString() + " c.";
        }
        else
        {
            _endGameCanvas.SetActive(true);
        }
    }
}
