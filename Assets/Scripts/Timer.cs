using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector), typeof(Cube))]
public class Timer : MonoBehaviour
{
    private CollisionDetector _collisionDetector;
    private Cube _cube;

    private readonly float _minTime = 2f;
    private readonly float _maxTime = 5f;

    public event Action<Cube> TimeUp;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
        _cube = GetComponent<Cube>();

        _collisionDetector.Fell += StartTimer;
    }

    private void OnDestroy()
    {
        _collisionDetector.Fell -= StartTimer;
    }

    private void StartTimer(Cube _)
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(GetRandomTime());
        TimeUp?.Invoke(_cube);
    }

    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(_minTime, _maxTime);
    }
}
