using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private ObjectPool _pewpewHolder;
    [SerializeField] private Transform _pewpewParent;

    private void Start()
    {
        _pewpewHolder.CreatePool(_pewpewParent);
    }
}
