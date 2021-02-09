using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionData
{
    public bool movementByCamera;
    public bool inverted;

    public OptionData(bool pMovementByCamera, bool pInverted)
    {
        movementByCamera = pMovementByCamera;
        inverted = pInverted;
    }
}
