using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolHolder : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private Transform _rotatorObject;
    [SerializeField] private Transform _bulletParent;

    [Header("Pattern Generator Vars")]

    [SerializeField] [Range(0,100)] private float _rotateSpeed = 5f;
    [SerializeField] [Range(0,1)] private float _shooterWaitTime = 0.2f;
    [SerializeField] [Range(1,10)] private int _spawnPointCount = 4;
    [SerializeField] [Range(0.1f,2f)] private float _radius = 1f;
    private List<Transform> _spawnPoints;

    private void Start()
    {
        _spawnPoints = new List<Transform>();

        float step = 2 * Mathf.PI / _spawnPointCount;
        for(int i = 0; i<_spawnPointCount; i++)
        {
            GameObject spawnPoint = new GameObject("Spawn Point "+ i);
            Vector3 pos = new Vector3(_radius*Mathf.Cos(step*i) + _rotatorObject.transform.position.x,
                                        _radius*Mathf.Sin(step*i) + _rotatorObject.transform.position.y,
                                        0f); 
            spawnPoint.transform.position = pos;

            float ang = (step*i) * (180/Mathf.PI);
            spawnPoint.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,ang));
            
            spawnPoint.transform.SetParent(_rotatorObject);
            _spawnPoints.Add(spawnPoint.transform);
        }


        _pool.CreatePool(_bulletParent);


        
        StartCoroutine(ShootTimer());
    }

    private void Update()
    {
        _rotatorObject.transform.Rotate(new Vector3(0f,0f,_rotateSpeed*Time.deltaTime));
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(_shooterWaitTime);

        for(int i = 0; i < _spawnPointCount; i++)
        {
            PoolableObject bullet;
            if(_pool.GetObject(out bullet))
            {
                
                bullet.Activate(null, _spawnPoints[i].position, _spawnPoints[i].rotation);
            }
        }
        StartCoroutine(ShootTimer());
    }
}
