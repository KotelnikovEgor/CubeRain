using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private Platform[] _platforms;
    [SerializeField] private Spawner _spawner;

    private readonly float _minTime = 2f;
    private readonly float _maxTime = 5f;

    public event Action<Cube> TimeUp;

    private void OnEnable()
    {
        foreach (var platform in _platforms)
        {
            platform.OnFell += StartTimer;
        }
    }

    private void OnDisable()
    {
        foreach (var platform in _platforms)
        {
            platform.OnFell -= StartTimer;
        }
    }

    private void StartTimer(Cube cube)
    {
        StartCoroutine(TimerCoroutine(cube));
    }

    private IEnumerator TimerCoroutine(Cube cube)
    {
        yield return new WaitForSeconds(GetRandomTime());
        TimeUp?.Invoke(cube);
    }

    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(_minTime, _maxTime);
    }
}
