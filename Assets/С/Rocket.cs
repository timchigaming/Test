using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private ParticleSystem ps;
    private Rigidbody rb;
    private MeshRenderer mr;
    private bool move = true;
    public float explosionForce = 10f;
    public float explosionRadius = 1f;
    public float destroyDelay = 2f; // Задержка перед уничтожением

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (move)
        {
            rb.MovePosition(rb.position + transform.forward * 10 * Time.deltaTime);
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            if (!hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("Projectile"))
                Explode();
    }

    private void Explode()
    {
        move = false;
        rb.velocity = Vector3.zero; // Останавливаем ракету
        GetComponent<Collider>().enabled = false; // Отключаем коллайдер
        
        ps.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in colliders)
        {
            if(c.CompareTag("Projectile"))
                continue;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.01f, ForceMode.Impulse);
        }
        mr.enabled = false;
        Destroy(gameObject, destroyDelay); // Уничтожаем ракету с задержкой
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.DrawRay(transform.position, transform.forward * 1f);
    }
}
