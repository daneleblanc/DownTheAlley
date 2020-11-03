using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    public static float GetAngle(float x, float z)
    {
        float value = (float)((Mathf.Atan2(x, z) / System.Math.PI) * 180f);
        return value;
    }
}
