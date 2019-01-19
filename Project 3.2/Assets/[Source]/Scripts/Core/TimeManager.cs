using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    #region Clock
    public TMP_Text timerHour;
    public TMP_Text timerMinute;
    float hour = 12;
    float minute;
    #endregion

    #region GameSpeed
    public TMP_Text speed;
    float speedValue = 1;
    public float maxSpeed;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpeed(int value)
    {
        if (!GameManager.paused)
        {
            switch (value)
            {
                case 0:
                if (speedValue - 1 > 0)
                {
                    speedValue -= 1;
                }
                break;
            
                case 1:
                if (speedValue + 1 <= maxSpeed)
                {
                    speedValue += 1;
                }
                break;
            }
            switch (speedValue)
            {
                case 1:
                speed.text = "Normal";
                GameManager.instance.UpdateGameSpeed(1);   
                break;
                
                case 2:
                speed.text = "Fast";
                GameManager.instance.UpdateGameSpeed(2);   
                break;

                case 3:
                speed.text = "Faster";
                GameManager.instance.UpdateGameSpeed(3);
                break;

                case 4:
                speed.text = "Fastest";
                GameManager.instance.UpdateGameSpeed(4);
                break;

                case 5:
                speed.text = "ludicrous";
                GameManager.instance.UpdateGameSpeed(5);
                break;
            }
        }
    }

    public void PauseGameSpeed()
    {
        GameManager.instance.TogglePauseGame();
        if (GameManager.paused)
        {
            speed.text = "Paused";
        }
        else
        {
            UpdateSpeed(-1);
        }
    }
}
