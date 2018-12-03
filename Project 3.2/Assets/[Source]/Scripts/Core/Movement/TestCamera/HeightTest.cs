using Sirenix.OdinInspector;
using UnityEngine;

public class HeightTest : MonoBehaviour
{
    private float dTime;

    public bool limitHeight;

    //[MinMaxSlider(0, 200, true)]
    public Vector2 minMaxHeight;
    //[Wrap(0f, 360)]
    //public float minRot, maxRot;

    public LayerMask groundMask;

    private Camera cam;

    //private Vector3 adjustedMousePos;
    private Vector3 debugPos;
    private float adjustedMouseHeight;

    private Vector2 MouseInput
    {
        get
        {
            return Input.mousePosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Vector3 debugPos = new Vector3(MouseWorldPosition().x, adjustedMousePos.y + 3, MouseWorldPosition().z);

        Gizmos.DrawWireSphere(debugPos, 0.5f);
    }

    public void Start()
    {
        cam = Camera.main;
        //adjustedMousePos = MouseWorldPosition();
        adjustedMouseHeight = MouseWorldPosition().x;
        debugPos = new Vector3(MouseWorldPosition().x, adjustedMouseHeight, MouseWorldPosition().z);
    }

    public void Update()
    {
        dTime = Time.deltaTime;

        //adjustedMousePos.y = Mathf.Lerp(adjustedMousePos.y, MouseWorldPosition().y, 0.1f );
        //adjustedMouseHeight = 
        //if(debugPos = ){
    }

    /// <summary>
    /// limit camera position
    /// </summary>
    private void LimitCameraHeight()
    {
        if (!limitHeight)
        {
            return;
        }

        #region Deprecated
        /*
        Vector3 borderSize = border.lossyScale / 2;
        Vector3 borderPos = border.position;

        m_Trans.position = new Vector3(Mathf.Clamp(m_Trans.position.x, (-borderSize.x) + borderPos.x, borderSize.x + borderPos.x),
            m_Trans.position.y,
                Mathf.Clamp(m_Trans.position.z, (-borderSize.z) + borderPos.z, borderSize.z + borderPos.z));
                */
        #endregion
    }

    /// <summary> 
    /// gets position of mouse in worldspace.
    /// </summary>
    private Vector3 MouseWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(MouseInput);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, cam.farClipPlane, groundMask.value))
        {
            return (hit.point);
        }

        return Vector3.zero;
    }
}