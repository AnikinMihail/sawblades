using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    [SerializeField] private TMP_Text scoreText;

    private int _score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = _score.ToString();
    }

    // Update is called once per frame
    public void AddPoint()
    {
        _score += 1;
        scoreText.text = _score.ToString();
    }
}
