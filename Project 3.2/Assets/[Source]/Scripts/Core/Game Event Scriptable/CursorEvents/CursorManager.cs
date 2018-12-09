using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    /// <summary>
    /// This Will Change the cursor.lockstate to locked confined or none.
    /// </summary>
    /// <param name="Lockstate">0 = None,1 = Locked, 2 = Confined </param>
    public void CurcorLockState(int Lockstate) {
        Cursor.lockState = (CursorLockMode)Lockstate;
    }

    /// <summary>
    /// This Will Change The Visibility Of The Curcor.
    /// </summary>
    /// <param name="visibility">True = Visible, False = Invisible</param>
    public void CursorVisibility(bool visibility) {
        Cursor.visible = visibility;
    }
}
