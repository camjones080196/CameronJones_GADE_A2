using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IListener {
    #region Variables
    private static GameManager instance = null;
    public Hero hero = new Hero();
    public Enemy enemy = new Enemy();
    #endregion

    #region Get+Set
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    #region Methods
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        EventManager.Instance.AddListener(Events.DEADENEMY, this);  //Listener for when an enemy is dead;
        EventManager.Instance.AddListener(Events.DEADHERO, this);  //Listener for when the hero is dead;
        EventManager.Instance.AddListener(Events.NEWLEVEL, this);  //Listener for when all enemies are dead to load new level
    }

    void Update()
    {

    }

    public void Kill(Object go)
    {
        Destroy(go); //Destroys the game oject that is sent to it
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void OnEvent(Events Event, Component Sender, Object param = null)
    {
        switch (Event)
        {
            case Events.DEADENEMY:
                {
                    Kill(Sender.gameObject); //Calls the kill method to destroy the sender
                };
                break;

            case Events.DEADHERO:
                {
                    Kill(Sender.gameObject); //Calls the kill method to destroy the sender
                };
                break;

            case Events.NEWLEVEL:
                {
                    NextLevel(); //Calls the next level method to load the next level.
                };
                break;

        }
    }

    private void OnApplicationFocus(bool focus) //Stops the game time if the user clicks off of the game
    {
        if (!focus)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    #endregion
}
