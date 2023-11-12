using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float speed = 100.0f;

    private void Awake() {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }


    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3.0f);

        Destroy(gameObject);
    }

    // 적 또는 장애물과 충돌하면 파괴
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "_Enemy")
        {
            other.gameObject.GetComponent<Enemy>().HitProjectile(1);
        }
        Destroy(gameObject);
    }
}
