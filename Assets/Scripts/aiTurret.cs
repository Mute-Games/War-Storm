using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiTurret : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion Lookat = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0.5f, player.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Lookat, 1 * Time.deltaTime);
    }
}
