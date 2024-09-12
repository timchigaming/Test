using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField]private GameObject rocket;
    [SerializeField]private Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Shoot();
    }
    void Shoot()
    {
        Instantiate(rocket, firePoint.position, firePoint.rotation);
    }
}
