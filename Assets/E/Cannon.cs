using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Fire()
    {
        ps.Play();
        GameObject blt = Instantiate(bullet, transform.position, Quaternion.identity);
        blt.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        gameObject.GetComponent<Rigidbody>().AddForce (-transform.forward * 100);
    }
}
