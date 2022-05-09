using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float blastRadius = 10f;
    public GameObject explosion;
    public float explosionForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        
        Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Enemy enemy = nearbyObject.transform.GetComponent<Enemy>();
            
            if (rb != null)
            {
                    enemy.TakeDamage(100);
                    rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
                    //Damage for rocketlauncher is hardcoded, should find a way to fix this eventually. Cannot access gun script from here
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
