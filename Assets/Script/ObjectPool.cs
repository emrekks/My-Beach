using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private List<T> pool;
    private Transform parentTransform;

    public ObjectPool(T prefab, int initialSize, Transform parentTransform = null)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;
        pool = new List<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        T obj = Object.Instantiate(prefab);
        obj.gameObject.SetActive(false);

        if (parentTransform != null)
        {
            obj.transform.parent = parentTransform;
        }

        pool.Add(obj);
    }

    public T GetObject()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        // If no inactive objects are found, create a new one and return it
        CreateObject();
        T newObj = pool[pool.Count - 1];
        newObj.gameObject.SetActive(true);
        return newObj;
    }
}