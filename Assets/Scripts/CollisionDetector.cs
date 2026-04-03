using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action Fell;

    public bool IsFell { get; private set; } = false;

    public void SetIsFell(bool value)
    {
        IsFell = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsFell)
        {
            if (collision.gameObject.TryGetComponent<Platform>(out _))
            {
                Fell?.Invoke();
                IsFell = true;
            }
        }
    }
}
