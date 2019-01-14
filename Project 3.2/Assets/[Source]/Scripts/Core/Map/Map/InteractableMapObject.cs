using UnityEngine;

public class InteractableMapObject : MonoBehaviour {
    [SerializeField] GameObject spottedObject;
    [SerializeField] BoxCollider waypointCheckCollider;
    [SerializeField] LayerMask myLayerMask;

    void Start() {
        if (spottedObject != null) {
            spottedObject.SetActive(false);
            Collider[] detectedColliders = Physics.OverlapBox(waypointCheckCollider.center + transform.position, waypointCheckCollider.bounds.extents * 2 + new Vector3(0,50,0), spottedObject.transform.rotation, myLayerMask);

            Debug.Log(detectedColliders.Length);

            for (int i = 0; i < detectedColliders.Length; i++) {
                detectedColliders[i].gameObject.SetActive(false);
            } 
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(waypointCheckCollider.center + transform.position, waypointCheckCollider.bounds.extents * 2);
    }
}