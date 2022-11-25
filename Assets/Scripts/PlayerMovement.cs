using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vehicle vehicle;

    float maxSpeed;
    float acceleration;

    float currentSpeed;
    float turn;

    int health;

    public FireGun firegun;
    public GunRotation gunrotation;
    public TurretRotation rotation;

    public AudioClip engineStill;
    public AudioClip engineMild;
    public AudioClip engineHard;
    public AudioClip engineFull;
    AudioSource Engine;


    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance.PlayerVehicleToSpawn != null)
        {
            var Player = GameObject.Instantiate(GameData.Instance.PlayerVehicleToSpawn, this.transform);
            vehicle = Player.GetComponent<Vehicle>();
        }


        firegun = GameObject.FindGameObjectWithTag("Gun").GetComponent<FireGun>();
        gunrotation = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotation>();
        rotation = GameObject.FindGameObjectWithTag("Turret").GetComponent<TurretRotation>();

        maxSpeed = vehicle.MaxSpeed;
        acceleration = vehicle.Acceleration;
        health = vehicle.Health;

        Engine = GetComponent<AudioSource>();

        turn = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Drive();
        MakeSounds();

        //Feeding data to GameData
        GameData.Instance.Turn = turn;
        GameData.Instance.Speed = currentSpeed;

        health = vehicle.Health;
        GameData.Instance.PlayerHealth = health;

        //The Movement being done
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        if (currentSpeed != 0)
        {
            transform.Rotate(new Vector3(0, turn * (currentSpeed) * Time.deltaTime, 0));
        }

        if (health == 0)
        {
            Die();
        }

    }
    public void Turn()
    {
        //Turning
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            turn -= 5 * Time.deltaTime;
            if (turn < -5)
            {
                turn = -5;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            turn += 5 * Time.deltaTime;
            if (turn > 5)
            {
                turn = 5;
            }
        }
        else
        {
            if (turn > 0.2f)
            {
                turn -= 4 * Time.deltaTime;

            }
            else if (turn < 0.2f)
            {
                turn += 4 * Time.deltaTime;
            }
            else turn = 0;
        }
    }
    public void Drive()
    {
        //Forward and Backwards
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (currentSpeed < 0)
            {
                currentSpeed += 10 * Time.deltaTime;
            }
            else
            {
                currentSpeed += acceleration * Time.deltaTime;

                if (currentSpeed > maxSpeed)
                {
                    currentSpeed = maxSpeed;
                }
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= 10 * Time.deltaTime;
            }
            else
            {
                currentSpeed -= (acceleration / 2) * Time.deltaTime;

                if (currentSpeed < -maxSpeed)
                {
                    currentSpeed = -maxSpeed;
                }
            }
        }
        else if (Input.GetKey(KeyCode.B))
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= 10 * Time.deltaTime;
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += 10 * Time.deltaTime;
            }
            else currentSpeed = 0;
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= Time.deltaTime;
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += Time.deltaTime;
            }
            else currentSpeed = 0;
        }
        
    }

    void MakeSounds()
    {
        AudioClip current = Engine.clip;
        if (Mathf.Abs(currentSpeed) <= 0.1f)
        {
            Engine.clip = engineStill;
        }
        else if (Mathf.Abs(currentSpeed) <= maxSpeed / 3)
        {
            Engine.clip = engineMild;
        }
        else if (Mathf.Abs(currentSpeed) <= maxSpeed * 2 / 3)
        {
            Engine.clip = engineHard;
        }
        else
        {
            Engine.clip = engineFull;
        }
        if (current != Engine.clip)
        {
            Engine.Play();
        }
    }


    void Die()
    {
        Destroy(rotation);
        Destroy(firegun);
        Destroy(gunrotation);
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            currentSpeed = 0;
        }
    }

    public void TakeDamage(int Damage)
    {
        vehicle.TakeDamage(Damage);
    }
}
