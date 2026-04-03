using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _minPosition;
    [SerializeField] private float _maxPosition;
    [SerializeField] private float _height;
    [SerializeField] private float _repeatRate;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        CreatePool();
    }

    private void Start()
    {
        StartCoroutine(GetCubeCoroutine());
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => PerformActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false)
            );
    }

    private IEnumerator GetCubeCoroutine()
    {
        WaitForSeconds seconds = new(_repeatRate);

        while (enabled)
        {
            yield return seconds;
            GetCube();
        }
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void PerformActionOnGet(Cube cube)
    {
        cube.transform.SetPositionAndRotation(GetRandomPosition(), Quaternion.identity);
        cube.gameObject.SetActive(true);
        cube.LifeExpired += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
        cube.LifeExpired -= ReleaseCube;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(_minPosition, _maxPosition), _height,
            Random.Range(_minPosition, _maxPosition));
    }
}
