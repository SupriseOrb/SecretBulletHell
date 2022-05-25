using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The ObjectPool class contains the functionality of a basic object pool
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private ObjectPoolData _data;
    private PoolableObject _objectPrefab;
    private Stack<PoolableObject> _freeObjects;
    private Stack<PoolableObject> _usedObjects;
    private int _initialCapacity;
    private int _currentCapacity { get{return _freeObjects.Count + _usedObjects.Count;}} //The current size of the pool
    private int _increaseAmount; //Determines how much to increase the pool size
    private int _maxCapacity; //The soft upper limit of how much the object pool size can increase to
    private Behavior _maxCapacityBehavior; //Determines what happens when count == capacity during GetObject()
    public enum Behavior
    {
        Nothing = 0,
        Kill = 1,
        Increase = 2
    }

    private void Awake()
    {
        //Load in the data from the ObjectPoolData
        _objectPrefab = _data.Prefab;
        _initialCapacity = _data.InitialCapacity;
        _increaseAmount = _data.IncreaseAmount;
        _maxCapacity = _data.MaxCapacity;
        _maxCapacityBehavior = _data.MaxCapacityBehavior;
    }
    
    //Create and populate the object pool
    public void CreatePool(Transform parent = null)
    {
        _freeObjects = new Stack<PoolableObject>();
        _usedObjects = new Stack<PoolableObject>();
        for(int i = 0; i < _initialCapacity; i++)
        {
            PoolableObject temp;
            if(parent!= null)
            {
                temp = Instantiate(_objectPrefab,parent);
            }
            else
            {
                temp = Instantiate(_objectPrefab);
            }
            _objectPrefab.gameObject.SetActive(false);
            _freeObjects.Push(temp);
        }
    }

    /*
    Returns true if there is a free object to use, otherwise false
    Checks the free and/or used stacks, and returns a free object to use
    If max capacity behavior is kill, forcibly kill an existing object to use
    Else if max capacity behavior is increase, increase the size of the pool
    */
    public bool GetObject(out PoolableObject returnedObject)
    {
        if(_freeObjects != null)
        {
            if(_freeObjects.Count > 0)
            {
                returnedObject = _freeObjects.Pop();
                _usedObjects.Push(returnedObject);
                return true;
            }
            else
            {
                if(_maxCapacityBehavior == Behavior.Kill)
                {
                    returnedObject = _usedObjects.Pop();
                    _usedObjects.Push(returnedObject);
                    return true;
                }
                else if(_maxCapacityBehavior == Behavior.Increase)
                {
                    for(int i = 0; i < _increaseAmount; i++)
                    {
                        PoolableObject temp = Instantiate(_objectPrefab);
                        _freeObjects.Push(temp);
                    }
                    if(_currentCapacity >= _maxCapacity)
                    {
                        _maxCapacityBehavior = Behavior.Kill;
                    }
                    returnedObject = _freeObjects.Pop();
                    _usedObjects.Push(returnedObject);
                    return true;
                }
            }
        }
        returnedObject = null;
        return false;
    }

    public void ReturnObjectToPool(PoolableObject poolableObject)
    {
        _freeObjects.Push(poolableObject);
    }
}
