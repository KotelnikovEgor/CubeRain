using System;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CollisionDetector : MonoBehaviour
{
    private Cube _cube;

    public event Action<Cube> Fell;

    private void Start()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_cube.IsFell)
        {
            if (collision.gameObject.TryGetComponent<Platform>(out _))
            {
                Fell?.Invoke(_cube);
                _cube.SetIsFell(true);
            }
        }
    }
}
