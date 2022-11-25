using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    float rotationY;
    float mouseInput;

    // Update is called once per frame
    void Update()
    {
        mouseInput = Input.GetAxis("Mouse X") * 1f;
        rotationY += mouseInput;

        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
        if (GameData.Instance.RealisticTurret) transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f * Time.deltaTime);
        else transform.rotation = rotation;
    }
}