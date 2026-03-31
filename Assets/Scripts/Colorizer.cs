using UnityEngine;

public class Colorizer : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private Platform[] _platforms;
    [SerializeField] private Timer _timer;

    private void OnEnable()
    {
        foreach (var platform in _platforms)
        {
            platform.OnFell += Colorize;
        }

        _timer.TimeUp += Colorize;
    }

    private void OnDisable()
    {
        foreach (var platform in _platforms)
        {
            platform.OnFell -= Colorize;
        }

        _timer.TimeUp -= Colorize;
    }

    private void Colorize(Cube cube)
    {
        if (cube.TryGetComponent(out Renderer renderer))
        {
            if (!cube.IsFell)
                renderer.material = _material;
            else
                renderer.material = _baseMaterial;
        }
    }
}
