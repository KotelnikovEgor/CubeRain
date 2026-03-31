using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;
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
        InvokeRepeating(nameof(GetCube), 0f, _repeatRate);
    }

    private void OnEnable()
    {
        _timer.TimeUp += ReleaseCube;
    }

    private void OnDisable()
    {
        _timer.TimeUp -= ReleaseCube;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (@object) => ActionOnGet(@object),
            actionOnRelease: (@object) => @object.gameObject.SetActive(false)
            );
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.SetPositionAndRotation(GetRandomPosition(), Quaternion.identity);
        cube.gameObject.SetActive(true);
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
        cube.SetIsFell(false);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(_minPosition, _maxPosition), _height,
            Random.Range(_minPosition, _maxPosition));
    }
}
