using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class for objects that are part of an object pool
public abstract class PoolableObject : MonoBehaviour
{ 
    protected ObjectPool _pool;

    public void SetPool(ObjectPool pool)
    {
        if(_pool == null)
        {
            _pool = pool;
        }
    }
    /*private PoolableObject _nextObject = null;
    public PoolableObject NextObject { get{return _nextObject;} set{_nextObject = value;}}
    [SerializeField] protected bool _freeToUse = true;
    public bool FreeToUse { get{return _freeToUse;}}*/

    //Set the variables for the object right before it's used
    public abstract void Activate(Transform parent, Vector3 position, Quaternion rotation);

    //Returns the object to its pool
    public abstract void ReturnToPool();
}
