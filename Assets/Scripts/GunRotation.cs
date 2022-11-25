using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    float rotationX;
    float mouseInput;
    public float MaxDepression;
    public float MaxOppression;

    // Update is called once per frame
    void Update()
    {
        mouseInput = Input.GetAxis("Mouse Y") * 1f;
        rotationX -= mouseInput;
        if (rotationX < -MaxOppression)
        {
            rotationX = -MaxOppression;
        }
        if (rotationX > MaxDepression)
        {
            rotationX = MaxDepression;
        }

        transform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }
}
