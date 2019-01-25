using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool {
    [HideInInspector] public Queue<GameObject> myQueue;
    public string poolTag;
    public GameObject prefab;
    public bool autoExpand;
    public int startSize;
}

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler instance;

    [SerializeField] List<Pool> Pools = new List<Pool>();
    Dictionary<string, int> poolDictionary;

    //This Creates The Pools And Creates The Objects That You Can Pool
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }

        poolDictionary = new Dictionary<string, int>();

        for (int i = 0; i < Pools.Count; i++) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int ii = 0; ii < Pools[i].startSize; ii++) {
                GameObject prefab = Instantiate(Pools[i].prefab);
                prefab.SetActive(false);
                objectPool.Enqueue(prefab);
            }

            Pools[i].myQueue = objectPool;
            poolDictionary.Add(Pools[i].poolTag, i);
        }
    }

    //call This Void To Get A Object From The Desired Pool
    public GameObject GetFromPool(string poolTag, Vector3 position, Quaternion rotation) {
        return (TakeFromPool(poolTag, position, rotation));
    }

    public GameObject GetFromPool(string poolTag, Vector3 position) {
        return (TakeFromPool(poolTag, position, new Quaternion()));
    }

    //call This Void To Get A Object From The Desired Pool And To The Desired Parent
    public GameObject GetFromPool(string poolTag, Vector3 position, Quaternion rotation, Transform parent) {
        GameObject fromPool = TakeFromPool(poolTag, position, rotation);
        fromPool.transform.SetParent(parent);
        return (fromPool);
    }

    GameObject TakeFromPool(string poolTag, Vector3 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(poolTag)) {
            Debug.LogError("Dictionary does not contain: " + tag);
            return (null);
        }

        Pool currentPool = Pools[poolDictionary[poolTag]];
        GameObject objectToGet = null;

        if (currentPool.myQueue.Count == 0) {
            if (currentPool.autoExpand == true) {
                objectToGet = Instantiate(currentPool.prefab);
                currentPool.startSize++;
                Debug.Log("ObjectPooler: Pool Expanded(" + poolTag + ")");
            } else {
                objectToGet = currentPool.myQueue.Dequeue();
                currentPool.myQueue.Enqueue(objectToGet);
            }
        } else {
            objectToGet = currentPool.myQueue.Dequeue();
        }

        objectToGet.transform.SetPositionAndRotation(position, rotation);
        objectToGet.SetActive(true);

        return (objectToGet);
    }

    //call This Void To Retrieve A Object To The Desired Pool
    public void AddToPool(string poolTag, GameObject ObjectToAdd) {

        if (!poolDictionary.ContainsKey(poolTag)) {
            Debug.LogError("Dictionary does not contain: " + tag);
            return;
        } else if (ObjectToAdd == null) {
            Debug.LogError("Object Is (Null): " + tag);
            return;
        }

        Pool currentPool = Pools[poolDictionary[poolTag]];
        ObjectToAdd.SetActive(false);

        currentPool.myQueue.Enqueue(ObjectToAdd);
    }
}