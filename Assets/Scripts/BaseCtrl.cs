using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCtrl : MonoBehaviour
{
    // Start is called before the first frame update

    public int hp_base;
    void Start()
    {
        hp_base = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp_base<=0)
        {
            GameManager.instance.LoseGame();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("_Enemy"))
        {
            hp_base -= 1;
            Destroy(other.gameObject);
        }
    }
}
