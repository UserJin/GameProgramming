using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMobe : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform p_tr;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = p_tr.position - transform.position;
        transform.position += dir * Time.deltaTime;
        transform.LookAt(new Vector3(p_tr.position.x, transform.position.y, p_tr.position.z));
    }
}
