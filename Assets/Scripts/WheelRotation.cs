using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public bool IsFront;
    float RotationX;
    float RotationY;

    void Update()
    {
        if (IsFront)
        {
            RotationY = GameData.Instance.Turn * 4;
        }
        RotationX += GameData.Instance.Speed * 100 * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(RotationX, RotationY, 0);
    }
}
