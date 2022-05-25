using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for bullets
public class Bullet : PoolableObject
{

    public override void SetValues(ObjectPool pool, Vector3 position, Quaternion rotation)
    {
        //_freeToUse = false;
        _pool = pool;
        transform.position = position;
        transform.rotation = rotation;
    }


    public override void ReturnToPool()
    {
        _pool.ReturnObjectToPool(this);
        gameObject.SetActive(false);
    }
}
