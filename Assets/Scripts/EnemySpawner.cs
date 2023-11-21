using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] float spawnTime = 5.0f;
    [SerializeField] float spawnStartTime = 10.0f;

    public List<Transform> spawnPoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform point in this.transform)
        {
            spawnPoints.Add(point);
        }
        // 게임 시작 1초 뒤부터 3초 간격으로 적 생성
        InvokeRepeating("SpawnEnemy", spawnStartTime, spawnTime);
    }


    void SpawnEnemy()
    {
        // 적 스폰 위치 리스트에서 임의의 장소를 고르고 해당 장소에 적 생성
        int i = Random.Range(0, spawnPoints.Count);
        Instantiate(enemy, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
    }
}
