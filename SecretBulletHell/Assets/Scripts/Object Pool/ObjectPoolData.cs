using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object Pool/Data", fileName = "New Object Pool Data")]
public class ObjectPoolData : ScriptableObject
{
    [SerializeField] private PoolableObject _objectPrefab;
    public PoolableObject Prefab { get{return _objectPrefab;}}
    [SerializeField] private int _initialCapacity;
    public int InitialCapacity { get{return _initialCapacity;}}
    [SerializeField] private int _increaseAmount; //Determines how much to increase the pool size
    public int IncreaseAmount{ get{return _increaseAmount;}}
    [SerializeField] private int _maxCapacity; //The soft upper limit of how much the object pool size can increase to
    public int MaxCapacity { get{return _maxCapacity;}}
    [SerializeField] private ObjectPool.Behavior _maxCapacityBehavior; //Determines what happens when count == capacity during GetObject()
    public ObjectPool.Behavior MaxCapacityBehavior {get{return _maxCapacityBehavior;}}
}
