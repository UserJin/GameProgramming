using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallCtrl : MonoBehaviour
{
    private Renderer rd;
    private Rigidbody rb;

    private bool isActivate;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        isActivate = false;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("_Granade") || other.gameObject.CompareTag("_Bullet"))
        {
            rd.material.color = Color.red;
            rb.isKinematic = false;
            isActivate = true;
        }
        else if (other.gameObject.CompareTag("_Enemy") && isActivate)
        {
            Destroy(other.gameObject);
        }
    }
}
