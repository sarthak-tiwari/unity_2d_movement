using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public string objectColor;
    public bool canBeDestroyed;
    public bool canBeMoved;

    public string GetObjectColor() => objectColor;

    public bool CanBeDestroyed() => canBeDestroyed;

    public bool CanBeMoved() => canBeMoved;
}
