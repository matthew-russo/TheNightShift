using UnityEngine;

public class PooledObject : MonoBehaviour {

	[System.NonSerialized]
	ObjectPool poolInstanceForPrefab;

	public T GetPooledInstance<T> () where T : PooledObject {
		if (!poolInstanceForPrefab) {
			poolInstanceForPrefab = ObjectPool.GetPool(this);
		}
		return (T)poolInstanceForPrefab.GetObject();
	}

	public ObjectPool Pool { get; set; }

	public void ReturnToPool () {
		if (Pool) {
            Debug.Log("Added to Pool");
			Pool.AddObject(this);
		}
		else {
			Debug.Log("Destroyed");
			Destroy(gameObject);
		}
	}
}