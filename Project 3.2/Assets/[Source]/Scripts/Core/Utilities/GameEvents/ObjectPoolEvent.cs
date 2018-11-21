using UnityEngine;

public class ObjectPoolEvent : MonoBehaviour {
    [SerializeField] string poolName;

    public void GetFromPool(){
        ObjectPooler.instance.GetFromPool(poolName,transform.position,transform.rotation);
    }
}
