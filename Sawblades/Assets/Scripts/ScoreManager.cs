using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    private int _score = 0;
    private float _time = 60f;

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
    }

    // Update is called once per frame
    public void AddPoint()
    {
        _score += 1;
        scoreText.text = _score.ToString();
    }public void AddTime()
    {
        _time += 1f;
    }
}
