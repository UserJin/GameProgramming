using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public int damage = 5;
    public float speed = 5.0f;
    public bool isDead = false;

    public GameObject playerBase;
    public GameObject player;
    
    private Rigidbody rb;

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
        state = State.MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.MOVE: //Move
                if (playerBase != null)
                {
                    Transform tr_Base = playerBase.transform;
                    Vector3 dir = tr_Base.position - transform.position;
                    transform.Translate(dir.normalized * speed * Time.deltaTime);
                    //transform.LookAt(new Vector3(tr_Base.position.x, 0, tr_Base.position.z));
                }
                float distance = Vector3.Distance(transform.position, player.transform.position);

                break;
            case State.TRACE:
                break;
            case State.ATTACK:
                break;
            case State.DIE:
                break;


        }
        
        // 체력이 모두 소진되면 키네마틱 해제 및 3초 뒤 제거
        if(hp <= 0 && !isDead)
        {
            GameManager.instance.enemyCount += 1;
            this.rb.isKinematic = false;
            Invoke("DestroyEnemy", 3.0f);
            isDead = true;
        }
    }

    public void HitProjectile(int n)
    {
        hp -= n;
    }

    void DestroyEnemy(){
        Destroy(gameObject);
    }

}
