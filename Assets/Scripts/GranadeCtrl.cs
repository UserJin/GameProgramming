using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeCtrl : MonoBehaviour
{
    public float power = 2500.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0.0f, 1.0f, 1.5f) * power);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "_Enemy")
        {
            other.gameObject.GetComponent<Enemy>().HitProjectile(3);
        }
        Destroy(gameObject);
    }
}
