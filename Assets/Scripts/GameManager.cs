using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int enemyCount;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        enemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyCount);
        if(enemyCount >= 5)
        {
            SceneManager.LoadScene("Scene_Win");
        }
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Scene_Lose");
    }
}
