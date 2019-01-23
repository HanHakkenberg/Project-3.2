using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region GameSpeed
    float gameSpeed = 1;
    public static bool paused = false;
    #endregion

    [Header("Tick/Time System")]
    #region TickVars
    [Tooltip("The time in minutes it takes to pass a entire day")]
    public int lengthOfDay;
    [Tooltip("The amount of minutes it takes for a short tick to trigger")]
    public float shortTick;
    float dayTick;
    float shortTime;
    float longTime;

    public GameObject WonLosPannel;
    public TMP_Text GameOverWonText;

    public delegate void Tick();
    public static Tick shortGameplayTick;
    public static Tick longGameplayTick;
    #endregion 

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start() 
    {
        dayTick = lengthOfDay;
    }
    
    void Update() 
    {
        GameTicks();
    }

    public void UpdateGameSpeed(float value)
    {
        gameSpeed = value;
        Time.timeScale = gameSpeed;
        print("GameSpeed" + gameSpeed);
    }
    public void TogglePauseGame()
    {
        if(paused)
        {
            paused = false;
            Time.timeScale = gameSpeed;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void GameTicks()
    {
        shortTime += Time.deltaTime;
        longTime += Time.deltaTime;

        if (shortTime >= shortTick * 60)
        {
            print("shortTick");
            shortTime = 0;
            shortGameplayTick();
        }
        if (longTime >= dayTick * 60)
        {
            print("longTick");
            longTime = 0;
            longGameplayTick();
        }
    }

    public void ChangeScene(int i)
	{
        SceneManager.LoadScene(i);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    public void GameOver()
    {
        GameOverWonText.text = "You lost the game";
    }

    public void GameWon()
    {
        GameOverWonText.text = "You won the game";
    }
}
