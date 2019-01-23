using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEvents : MonoBehaviour
{
    public static void SpecialEventsEffects(int trigger)
    {
        switch (trigger)
        {
            case 1:
            GameManager.instance.GameWon();
            break;
        }
    }
}
