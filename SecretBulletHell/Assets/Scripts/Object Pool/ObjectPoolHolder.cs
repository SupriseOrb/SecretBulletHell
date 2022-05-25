using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolHolder : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private Transform _objectParent;

    private void Start()
    {
        if(_objectParent == null)
        {
            _objectParent = _pool.transform;
        }
        _pool.CreatePool(_objectParent);
    }
}
