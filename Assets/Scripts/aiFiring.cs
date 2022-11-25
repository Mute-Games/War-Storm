using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiFiring : MonoBehaviour
{
    //how long it takes to reload
    public float ReloadTime;
    //how much damage it makes
    public int Damage;

    //UNITY PARTICLE SYSTEM AAAAAAAAA
    public GameObject FireLocation;
    public GameObject MuzzleFlash;
    public GameObject ImpactEffect;

    bool ender;

    //SFX
    public GameObject Shot;
    public GameObject Impact;


    // Start is called before the first frame update
    void Start()
    {
        ender = false;
        StartCoroutine(Firing());
    }

    IEnumerator Firing()
    {
        while (ender == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(FireLocation.transform.position, FireLocation.transform.forward, out hit))
            {   
                if (hit.transform.tag == "Player")
                {
                    Debug.Log("Firing");
                    Fire();
                    yield return new WaitForSeconds(ReloadTime);
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }


    void Fire()
    {
        //AAAAAAAAAAAAA
        var flash = Instantiate(MuzzleFlash, FireLocation.transform.position, FireLocation.transform.rotation);
        Destroy(flash, ReloadTime);
        var shot = Instantiate(Shot);
        Destroy(shot, 2f);
        //Actual firing
        RaycastHit hit;
        if (Physics.Raycast(FireLocation.transform.position, FireLocation.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("ok it fired");
            var target = hit.transform.GetComponent<PlayerMovement>();
            if (target != null)
            {
                Debug.Log("Yeah it hit you");
                target.TakeDamage(Damage);
                var impact = Instantiate(Impact);
                Destroy(impact, 2f);
            }

            Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }

    }

}
