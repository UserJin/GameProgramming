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

    // 총알 피해 및 파괴
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("_Enemy") && gameObject.CompareTag("_Bullet"))
        {
            other.gameObject.GetComponent<Enemy>().HitProjectile(1);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("_Player") && gameObject.CompareTag("_BulletE"))
        {
            other.gameObject.GetComponent<PlayerCtrl>().PlayerHit(1);
            Destroy(gameObject);
        }
        else if(!other.gameObject.CompareTag("_Player") && !other.gameObject.CompareTag("_Enemy")) Destroy(gameObject);
    }
}
