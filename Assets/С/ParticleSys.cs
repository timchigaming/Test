using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSys : MonoBehaviour
{
    [SerializeField, Tooltip("Начальный радиус")] float StartRadius;
    [SerializeField, Tooltip("Конечный радиус")] float EndRadius;
    [SerializeField, Tooltip("Скорость изменения радиуса")] float RadiusChangerSpeed;

    [SerializeField, Tooltip("Начальный угол")] float StartAngle;
    [SerializeField, Tooltip("Конечный угол")] float EndAngle;
    [SerializeField, Tooltip("Скорость изменения угла")] float AngleChangerSpeed;


    [SerializeField] ParticleSystem PSModule;
    [SerializeField] float currentRadius;
    [SerializeField] float currentAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRadius =    Mathf.Lerp(StartRadius, EndRadius, Mathf.PingPong(Time.time * RadiusChangerSpeed, 1));
        currentAngle =     Mathf.Lerp(StartAngle,  EndAngle,  Mathf.PingPong(Time.time * RadiusChangerSpeed, 1));

        var Shape = PSModule.shape;

        Shape.angle = currentAngle;
        Shape.radius = currentRadius;
    }
}
