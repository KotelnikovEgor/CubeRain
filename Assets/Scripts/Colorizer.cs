using UnityEngine;

[RequireComponent(typeof(Cube), typeof(CollisionDetector), typeof(Timer))]
[RequireComponent(typeof(Renderer))]
public class Colorizer : MonoBehaviour
{
    private Cube _cube;
    private CollisionDetector _collisionDetector;
    private Timer _timer;
    private Renderer _renderer;
    private Color _baseColor;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _timer = GetComponent<Timer>();
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;

        _collisionDetector.Fell += Colorize;
        _timer.TimeUp += Colorize;
    }

    private void OnDestroy()
    {
        _collisionDetector.Fell -= Colorize;
    }

    private void Colorize(Cube cube)
    {
        if (!_cube.IsFell)
            _renderer.material.color = GetRandomColor();
        else
            _renderer.material.color = _baseColor;
    }

    private Color GetRandomColor()
    {
        return Random.ColorHSV();
    }
}
