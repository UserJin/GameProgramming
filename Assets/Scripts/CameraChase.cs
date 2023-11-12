using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private Transform target_tr;
    private Transform Camera_tr;
    [SerializeField] private float distanceX = 1;
    [SerializeField] private float distanceY = 2.5f;
    [SerializeField] private float distanceZ = -2;
    [SerializeField] private float damping = 1.0f;

    private void Start()
    {
        Camera_tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 pos = target_tr.position + (target_tr.forward * distanceZ) + (target_tr.right * distanceX) + (Vector3.up * distanceY);
        Camera_tr.position = Vector3.Slerp(Camera_tr.position, pos, Time.deltaTime * damping);
        //Camera_tr.LookAt(new Vector3(target_tr.position.x, Camera_tr.position.y, target_tr.position.z));
    }
}
