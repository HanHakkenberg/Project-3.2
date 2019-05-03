using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float attitude;

    void Start() 
    {
        UpdateIsland();
    }

    void UpdateIsland()
    {
        if (attitude > 0)
        {
            float value = Mathf.Log(attitude, 2);
            print(value);
            attitude -= value;
            if (attitude < 0)
            {
                attitude = 0;
            }
        }
        else if(attitude < 0)
        {
            attitude = Mathf.Abs(attitude);
            float value = Mathf.Log(attitude, 2);
            print(value);
            attitude *= -1;
            attitude += value;
            if (attitude > 0)
            {
                attitude = 0;
            }
        }
    }
}
