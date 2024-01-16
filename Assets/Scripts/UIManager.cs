using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to Text
    [SerializeField] private Text _scoreText;
    private float _scoreCount;
    private int _score;
    
    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _score = 0;
        {
            _scoreText.text = "Score: " + _score;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreTotal(int killPoint)
    {
        _score += killPoint;
        _scoreText.text = "Score: " + _score.ToString();
    }


}
