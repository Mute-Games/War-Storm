using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;
    public int Health;
    public GameObject KABOOM;
    public bool IsDummy;
    public bool IsPlayer;
    public bool IsPlane;

    bool played;



    void Update()
    {
        if (Health <= 0 && !played)
        {
            Health = 0;
            Debug.Log("Epic");
            Instantiate(KABOOM, transform.position, transform.rotation);
            if (!IsPlayer) Destroy(gameObject);

            played = true;
        }
        if (IsDummy)
        {
            Move();
        }
        if (IsPlane)
        {
            Fly();
        }
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsDummy && collision.transform.tag == "Player")
        {
            Health = 0;
        }
        if (IsPlane && collision.transform.tag == "Wall")
        {
            Health = 0;
        }
    }

    void Move()
    {
        if (MaxSpeed > 0)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(this.transform.position, player.transform.position) < 30f) 
            {
                transform.LookAt(player.transform);
                transform.Translate(new Vector3(0, 0, -MaxSpeed * Time.deltaTime));
            }
        }

    }

    void Fly()
    {
        transform.Translate(new Vector3(0, 0, MaxSpeed * Time.deltaTime));
    }
}
