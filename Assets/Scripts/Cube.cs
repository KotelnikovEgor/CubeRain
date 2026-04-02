using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool IsFell { get; private set; } = false;

    public void SetIsFell(bool value)
    {
        IsFell = value;
    }
}
