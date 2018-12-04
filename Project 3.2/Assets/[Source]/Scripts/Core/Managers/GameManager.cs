using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("The time in minutes it takes to go from sunrise to sunrise")]
    public int lengthOfDay;
    public static GameManager instance;

    #region TickVars
    float shortTick;
    float dayTick;
    float shortTime;
    float longTime;

    public delegate void Tick();
    public Tick shortGameplayTick;
    public Tick longGameplayTick;
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

    void Update() 
    {
        GameTicks();
    }

    public void GameTicks()
    {
        shortTime += Time.deltaTime;
        longTime += Time.deltaTime;

        if (shortTime >= shortTick)
        {
            shortTime = 0;
            shortGameplayTick();
        }
        if (longTime >= dayTick)
        {
            longTime = 0;
            longGameplayTick();
        }
    }
}
