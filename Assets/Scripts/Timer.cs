using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private readonly float _minTime = 2f;
    private readonly float _maxTime = 5f;

    public event Action TimeUp;

    public void StartTimer()
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(GetRandomTime());
        TimeUp?.Invoke();
    }

    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(_minTime, _maxTime);
    }
}
