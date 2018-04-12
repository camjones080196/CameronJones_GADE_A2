using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    #region Variables
    private float startTime;
    private float t;
    public Text timerText;
    #endregion

    #region Get+Set
    public float StartTime
    {
        get
        {
            return startTime;
        }

        set
        {
            startTime = value;
        }
    }

    public float T
    {
        get
        {
            return t;
        }

        set
        {
            t = value;
        }
    }
    #endregion

    #region Methods
    void Start()
    {
        StartTime = Time.time;
    }

    void Update()
    {
       t = Time.time - StartTime;

        string minutes = ((int)t / 60).ToString();  //Creates a string to store the amount of minutes gone by in the game
        string seconds = (T % 60).ToString("f2");   //Creates a string to store the amount of seconds gone by in the game

        timerText.text = minutes + ":" + seconds;   //Sets the timer text on the game stage
    }


    #endregion
}
