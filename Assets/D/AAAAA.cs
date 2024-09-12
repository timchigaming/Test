using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAAAA : MonoBehaviour
{
    AudioSource aud;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        aud.Play();
    }
}
