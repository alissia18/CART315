using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    
    public EnemyController turtle;
    public EnemyController axolotl;
    public EnemyController whale;
    
    public int spawnAxisX = 12;
    public float shootDelay;
    private int distanceToShoot;

    public enum EnemyType{TURTLE, AXOLOTL, WHALE }

    private List<EnemySpawn> enemySpawns;
    private Dictionary<EnemyType, EnemyController> enemyDict;

    private float beatLength;
    private float spawnTimer;
    private float speed;
   
    private void Awake()
    {
        enemyDict = new Dictionary<EnemyType, EnemyController>();
        enemyDict.Add(EnemyType.TURTLE, turtle);
        enemyDict.Add(EnemyType.AXOLOTL, axolotl);
        enemyDict.Add(EnemyType.WHALE, whale);
    }
    
    void Start()
    {
        gameManager = GameManager.Instance;

        beatLength = gameManager.songData.beatLength;
        enemySpawns = new List<EnemySpawn>(gameManager.songData.enemySpawns);
        spawnTimer = - gameManager.startDelay * beatLength;
        distanceToShoot = spawnAxisX - gameManager.shootAxisX;
        speed = distanceToShoot / shootDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawns.Count == 0) return;
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= (enemySpawns[0].beat - 2) * beatLength)
        {
            SpawnEnemy(enemySpawns[0].type, enemySpawns[0].lane);
            enemySpawns.RemoveAt(0);
        }
    }

    public void SpawnEnemy(EnemyType type, int lane)
    {
        Vector3 pos = new Vector3(spawnAxisX, gameManager.GetLaneYPos(lane), 0);
        EnemyController newEnemy = Instantiate(enemyDict[type].gameObject, pos, Quaternion.identity).GetComponent<EnemyController>();
        newEnemy.speed = speed;
        newEnemy.projectileDelay = beatLength;
    }
    
    private IEnumerator TurtleSpawn()
    {
        int y = Random.Range(-3, 3);
        Instantiate(turtle, new Vector3(8, y, 0), Quaternion.identity);

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(TurtleSpawn());
    }
}

[Serializable]
public class EnemySpawn
{
    public EnemyManager.EnemyType type;
    public float beat;
    public int lane;
}
