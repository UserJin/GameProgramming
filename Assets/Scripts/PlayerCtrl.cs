using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerCtrl : MonoBehaviour
{
    public float hp = 10.0f;
    public float damage = 10.0f;
    public float moveSpeed = 5.0f;
    public float turnSpeed = 100.0f;
    public int dashScale = 1;
    [SerializeField] private float jumpPower = 10.0f;

    private bool isDash;
    private bool isJump;
    private bool isBarrierOn;
    private bool possibleBarrier;

    private float barrierCoolTime = 10.0f;
    private float barrierDurationTime = 5.0f;

    private float h = 0;
    private float v = 0;

    public GameObject bullet;
    public GameObject granade;
    [SerializeField] private VisualEffect muzzleFlash;
    [SerializeField] private GameObject barrierEffect;


    private Transform tr;
    private CapsuleCollider col;
    private Rigidbody rb;
    private Animator anim;
    [SerializeField] private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        tr = this.gameObject.GetComponent<Transform>();
        col = this.gameObject.GetComponent<CapsuleCollider>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //firePoint = GameObject.Find("FirePoint").GetComponent<Transform>();

        isDash = false;
        isJump = false;
        isBarrierOn = false;
        possibleBarrier = true;

        //Debug.Log($"{gameObject.name} hp: {hp}");
        //Debug.Log($"{gameObject.name} damage: {damage}");
        //Debug.Log($"{gameObject.name} speed: {moveSpeed}");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MouseAction();
        ChangeAnim();
        CheckGround();
    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        float m_x = Input.GetAxis("Mouse X");

        // 좌측 쉬프트키 입력시 이동 속도 증가(대시)
        if (Input.GetKey(KeyCode.LeftShift) && v > 0)
        {
            dashScale = 3;
            isDash = true;
        }
        else
        {
            dashScale = 1;
            isDash = false;
        }


        rb.velocity = ((transform.forward * v + transform.right * h).normalized * moveSpeed * dashScale) + new Vector3(0, rb.velocity.y, 0);
        tr.Rotate(0, m_x * Time.deltaTime * turnSpeed, 0);


        //물리적 이동방식의 문제로 인해 잠시 보류
         if (Input.GetKey(KeyCode.Space) && !isJump)
        {
            isJump = true;
            rb.AddForce(Vector3.up*jumpPower, ForceMode.Impulse);
        }
    }

    void MouseAction()
    {
        // 마우스 왼쪽(총알), 마우스 오른쪽 배리어 사용
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            muzzleFlash.Play();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            UseBarrier();
        }
    }

    void ChangeAnim()
    {
        anim.SetInteger("v", (int)v);
        anim.SetInteger("h", (int)h);
        anim.SetBool("isDash", isDash);
    }

    void CheckGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            isJump = false;
        }
    }

    void UseBarrier()
    {
        if(possibleBarrier)
        {
            possibleBarrier = false;
            isBarrierOn = true;
            barrierEffect.SetActive(true);
            StartCoroutine(BarrierDurationFunc());
            StartCoroutine(BarrierCoolTimeFunc());
        }
    }

    IEnumerator BarrierCoolTimeFunc()
    {
        yield return new WaitForSeconds(barrierCoolTime);
        possibleBarrier = true;
    }

    IEnumerator BarrierDurationFunc()
    {
        yield return new WaitForSeconds(barrierDurationTime);
        isBarrierOn = false;
        barrierEffect.SetActive(false);
    }

    public void PlayerHit(float damage)
    {
        if(!isBarrierOn)
        {
            hp -= damage;
        }
        if(hp < 0.0f)
        {
            GameManager.instance.LoseGame();
        }
    }
}
