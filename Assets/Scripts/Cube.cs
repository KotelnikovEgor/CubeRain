using System;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector), typeof(Colorizer), typeof(Timer))]
public class Cube : MonoBehaviour
{
    private CollisionDetector _collisionDetector;
    private Colorizer _colorizer;
    private Timer _timer;

    public event Action<Cube> LifeExpired;

    private void Start()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
        _collisionDetector.Fell += ActWhenFalling;
        _colorizer = GetComponent<Colorizer>();
        _timer = GetComponent<Timer>();
        _timer.TimeUp += ActWhenTimeUp;
    }

    private void OnDestroy()
    {
        _collisionDetector.Fell -= ActWhenFalling;
        _timer.TimeUp -= ActWhenTimeUp;
    }

    private void ActWhenFalling()
    {
        _colorizer.Colorize();
        _timer.StartTimer();
    }

    private void ActWhenTimeUp()
    {
        LifeExpired?.Invoke(this);
        _collisionDetector.SetIsFell(false);
        _colorizer.ColorizeInBase();
    }
}
