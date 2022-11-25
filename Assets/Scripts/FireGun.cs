using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    //is it an automatic weapon
    public bool IsAutoCannon;
    //how fast does it fire in rounds per minute
    public int Firerate;
    //to check when to fire next
    float nextTimeToFire;
    //what's the max amount of ammo in the belt/mag
    public int AmmoMax;
    //current ammo
    int CurrentAmmo;
    //how long it takes to reload
    public int ReloadTime;
    //how much damage it makes
    public int Damage;

    //Camera from where shot comes
    public Camera ScopeCam;

    //checking if can fire or not
    bool CanFire;

    //UNITY PARTICLE SYSTEM AAAAAAAAA
    public GameObject FireLocation;
    public GameObject MuzzleFlash;
    public GameObject ImpactEffect;

    //SFX
    public GameObject Shot;
    public GameObject LastShot;
    public GameObject Hit;

    // Start is called before the first frame update
    void Start()
    {
        CanFire = true;
        CurrentAmmo = AmmoMax;
        GameData.Instance.Ammo = CurrentAmmo + "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && IsAutoCannon && CanFire)
        {
            nextTimeToFire = Time.time + (60f / Firerate);
            CurrentAmmo--;
            GameData.Instance.Ammo = CurrentAmmo + "";
            Shoot();
        }
        if (Input.GetButtonUp("Fire1") && IsAutoCannon && CanFire && LastShot != null)
        {
            var LASTshot = Instantiate(LastShot);
            Destroy(LASTshot, 2f);
        }
        if (CurrentAmmo <= 0 && CanFire)
        {
            StartCoroutine(Reload());
        }


        if (Input.GetButton("Fire1") && !IsAutoCannon && CanFire)
        {
            Shoot();
            StartCoroutine(Reload());
        }



    }

    void Shoot()
    {
        //AAAAAAAAAAAAA
        var Flash = Instantiate(MuzzleFlash, FireLocation.transform.position, FireLocation.transform.rotation);
        Destroy(Flash, (60f / Firerate));
        //SFX
        var shot = Instantiate(Shot);
        Destroy(shot, (60f / Firerate));

        //Actual firing
        StartCoroutine(Firing());
    }

    IEnumerator Firing()
    {
        RaycastHit hit;
        if (Physics.Raycast(ScopeCam.transform.position, ScopeCam.transform.forward, out hit))
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log(hit.transform.name);

            var HIT = Instantiate(Hit);
            Destroy(HIT, 2f);

            Vehicle target = hit.transform.GetComponent<Vehicle>();
            if (target != null)
            {
                target.TakeDamage(Damage);
            }

            Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        yield return null;
    }

    IEnumerator Reload()
    {
        CanFire = false;
        for (int i = 0; i < ReloadTime; i++)
        {
            GameData.Instance.Ammo = (ReloadTime - i) + "s";
            yield return new WaitForSeconds(1);
        }
        CanFire = true;
        CurrentAmmo = AmmoMax;
        GameData.Instance.Ammo = CurrentAmmo + "";
        yield return null;

    }


}
