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

    private IEnumerator GetCubeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_repeatRate);
            GetCube();
        }
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (@object) => PerformActionOnGet(@object),
            actionOnRelease: (@object) => @object.gameObject.SetActive(false)
            );
    }

    private void PerformActionOnGet(Cube cube)
    {
        cube.transform.SetPositionAndRotation(GetRandomPosition(), Quaternion.identity);
        cube.gameObject.SetActive(true);
        cube.GetComponent<Timer>().TimeUp += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        cube.GetComponent<Timer>().TimeUp -= ReleaseCube;
        _pool.Release(cube);
        cube.SetIsFell(false);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(_minPosition, _maxPosition), _height,
            Random.Range(_minPosition, _maxPosition));
    }
}
