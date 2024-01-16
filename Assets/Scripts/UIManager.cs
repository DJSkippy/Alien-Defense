using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private Text _scoreText;
    private int _score;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _score = 0;
        {
            _scoreText.text = "Score: " + 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + _score.ToString();
    }

}
