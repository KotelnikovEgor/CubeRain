using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public event Action<Cube> OnFell;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (!cube.IsFell)
            {
                OnFell?.Invoke(cube);
                cube.SetIsFell(true);
            }
        }
    }
}
