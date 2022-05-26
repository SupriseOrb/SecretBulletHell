using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for bullets
public class Bullet : PoolableObject
{
    [SerializeField] [Range(0.1f,10f)] private float _speed = 1f;
    [SerializeField] [Range(1f, 40f)] private float _lifetime;
    private bool _active = false;    

    //Moves the bullet in a certain direction at a certain speed every frame
    private void Update()
    {
        if(_active)
        {
            transform.position += transform.right * _speed * Time.deltaTime;
        } 
    }

    //Set the values of the bullet prefab
    public override void Activate(Transform parent, Vector3 position, Quaternion rotation)
    {
        if(parent != null)
        {
            transform.parent = parent;
        }
        transform.position = position;
        transform.rotation = rotation;
        _active = true;
        gameObject.SetActive(true);
        
    }

    //Return the bullet back to whence it came from
    public override void ReturnToPool()
    {
        
        _pool.ReturnObjectToPool(this);
        StopAllCoroutines();
        _active = false;
        gameObject.SetActive(false);
    }

    //When the bullet is born, it's life shall start to tick down
    private void OnEnable()
    {
        StartCoroutine(DeathTimer());
    }

    //After the bullet has lived out it's life, it'll be returned to the pool
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(_lifetime);
        ReturnToPool();
    }

    
}
