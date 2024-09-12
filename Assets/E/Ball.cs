using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    ParticleSystem ps;
    TrailRenderer tr;
    Rigidbody rb;
    public AudioClip[] Sfx;
    AudioSource aS;
    Collider cl;

    // Start is called before the first frame update
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        tr = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
        rb.useGravity = false;
        aS.PlayOneShot(Sfx[0]);
        cl = GetComponent<Collider>();
        Physics.IgnoreCollision(cl, GameObject.Find("Cube").GetComponent<Collider>());
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        rb.useGravity = true;
        ps.Play();
        aS.PlayOneShot(Sfx[1]);
    }
}
