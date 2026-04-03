using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Colorizer : MonoBehaviour
{
    private Renderer _renderer;
    private Color _baseColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;
    }

    public void Colorize()
    {
        _renderer.material.color = GetRandomColor();
    }

    public void ColorizeInBase()
    {
        _renderer.material.color = _baseColor;
    }

    private Color GetRandomColor()
    {
        return Random.ColorHSV();
    }
}
