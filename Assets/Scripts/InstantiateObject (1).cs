using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToInstantiate;
    private List<GameObject> _instantiatedObjects = new List<GameObject>();

    public void Instantiate(Transform target)
    {
        GameObject obj = GetObject();
        obj.transform.SetPositionAndRotation(target.position, target.rotation);
        obj.SetActive(true);
    }

    public void Instantiate(Vector3 position)
    {
        GameObject obj = GetObject();
        obj.transform.position = position;
        obj.SetActive(true);
    }

    private GameObject GetObject()
    {
        GameObject obj = null;
        if (_instantiatedObjects.Count > 0)
        {
            obj = _instantiatedObjects.Find(o => !o.activeInHierarchy);
        }

        if (obj == null)
        {
            obj = Instantiate(_objectToInstantiate);
            _instantiatedObjects.Add(obj);
        }
        obj.SetActive(false);
        return obj;
    }
}
