using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public int damage = 5;
    public float speed = 5.0f;

    public float traceDist;
    public float attackDist;
    public float attackCoolTime = 1.0f;
    public float attackCoolTimeMax = 1.0f;


    public GameObject playerBase;
    public GameObject player;
    
    private Rigidbody rb;

    public GameObject bullet;
    public Transform firePoint;

    public enum State
    {
        MOVE,
        TRACE,
        ATTACK,
        DIE
    }

    [SerializeField] State state;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("_Player");
        playerBase = GameObject.FindWithTag("_Base");
        rb = this.GetComponent<Rigidbody>();
        attackCoolTime = 1.0f;
        state = State.MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        float distance;
        switch(state)
        {
            case State.MOVE: //Move
                if (playerBase != null)
                {
                    Transform tr_Base = playerBase.transform;
                    Vector3 dir = tr_Base.position - transform.position;
                    Debug.DrawRay(transform.position, dir * 2, Color.green);
                    //transform.Translate(dir.normalized * speed * Time.deltaTime);
                    transform.position += dir.normalized * speed * Time.deltaTime;
                    transform.LookAt(new Vector3(tr_Base.position.x, transform.position.y, tr_Base.position.z));
                }
                distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance <= traceDist) state = State.TRACE;
                else if (distance <= attackDist) state = State.ATTACK;
                break;
            case State.TRACE:
                if(player != null)
                {
                    Transform tr_Player = player.transform;
                    Vector3 dir = tr_Player.position - transform.position;
                    Debug.DrawRay(transform.position, dir * 3, Color.red);
                    //transform.Translate(dir.normalized * speed * Time.deltaTime);
                    transform.position += dir.normalized * speed * Time.deltaTime;
                    transform.LookAt(new Vector3(tr_Player.position.x, transform.position.y, tr_Player.position.z));
                }
                distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > traceDist) state = State.MOVE;
                else if (distance <= attackDist) state = State.ATTACK;
                break;
            case State.ATTACK:
                if (player != null)
                {
                    Transform tr_Player = player.transform;
                    Vector3 dir = tr_Player.position - transform.position;
                    //transform.Translate(dir.normalized * speed * Time.deltaTime);
                    transform.position += dir.normalized * speed * Time.deltaTime;
                    attackCoolTime += Time.deltaTime;
                    if (attackCoolTime >= attackCoolTimeMax) Attack();
                    transform.LookAt(new Vector3(tr_Player.position.x, transform.position.y, tr_Player.position.z));
                }
                distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > traceDist) state = State.MOVE;
                else if (distance > attackDist) state = State.TRACE;
                break;
            case State.DIE:
                GameManager.instance.enemyCount += 1;
                DestroyEnemy();
                break;


        }
        if (hp <= 0) state = State.DIE;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, traceDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    public void Attack()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        attackCoolTime = 0;
    }

    public void HitProjectile(int n)
    {
        hp -= n;
        if (hp <= 0) state = State.DIE;
    }

    void DestroyEnemy(){
        Destroy(gameObject);
    }

}
