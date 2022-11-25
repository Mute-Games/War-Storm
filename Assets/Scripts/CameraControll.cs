using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public GameObject player;
    public float distance = 15f;
    public float height = 2f;
    public float angle = 35f;
    float rotationY;
    float rotationX;
    Vector3 offset = new Vector3(0, 0, 0);
    float mouseInput;
    public float MaxDepression;
    public float MaxOppression;

    private void Start()
    {
        GunRotation gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotation>();
        MaxDepression = gun.MaxDepression;
        MaxOppression = gun.MaxOppression;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset.y = height;

        mouseInput = Input.GetAxis("Mouse X") * 1f;
        rotationY += mouseInput;

        transform.position = player.transform.position - transform.forward * distance + offset;

        mouseInput = Input.GetAxis("Mouse Y") * 1f;
        rotationX -= mouseInput;
        if (rotationX < -MaxOppression - angle)
        {
            rotationX = -MaxOppression - angle;
        }
        if (rotationX > MaxDepression - angle)
        {
            rotationX = MaxDepression - angle;
        }

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
