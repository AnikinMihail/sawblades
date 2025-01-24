using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image barImage;

    private int _score = 0;
    private float _time = 60f;
    private float _whiteTimer = 0f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = _score.ToString();
        timeText.text = "60";
    }

    private void Update()
    {
        if (_time > 0f)
        {
            _time -= Time.deltaTime;
        }

        timeText.text = ((int)_time).ToString();
        barImage.fillAmount = _time / 60f;

        if (_whiteTimer > 0f)
        {
            barImage.color = Color.white;
            _whiteTimer-= Time.deltaTime;
        }
        else
        {
            barImage.color = new Color(250/255f, 132/255f, 0, 1);
        }
    }

    // Update is called once per frame
    public void AddPoint()
    {
        _score += 1;
        scoreText.text = _score.ToString();
    }public void AddTime()
    {
        _time += 1f;
        _whiteTimer += .1f;
    }
}
