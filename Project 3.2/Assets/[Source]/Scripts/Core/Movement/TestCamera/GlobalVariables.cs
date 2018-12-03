using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
    private static Vector2 MouseInput
    {
        get
        {
            return Input.mousePosition;
        }
    }

    /// <summary> 
    /// Gets worldspace position of the cursor.
    /// </summary>
    public static Vector3 MouseWorldPosition()
    {
        return MouseWorldPosition(Camera.main);
    }

    /// <summary> 
    /// Gets worldspace position of the cursor.
    /// </summary>
    public static Vector3 MouseWorldPosition(Camera camera)
    {
        return MouseWorldPosition(camera, 0);
    }

    /// <summary> 
    /// Gets worldspace position of the cursor.
    /// </summary>
    public static Vector3 MouseWorldPosition(LayerMask groundMask)
    {
        return MouseWorldPosition(Camera.main, groundMask);
    }

    /// <summary> 
    /// Gets worldspace position of the cursor.
    /// </summary>
    public static Vector3 MouseWorldPosition(Camera camera, LayerMask groundMask)
    {
        Ray ray = camera.ScreenPointToRay(MouseInput);

        if (Physics.Raycast(ray, out RaycastHit hit, camera.farClipPlane, groundMask))
        {
            return (hit.point);
        }
        else
        {
            Debug.Log("No Ground");

            return Vector3.zero;
        }
    }

}