using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private LayerMask bonusLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Transform line;
    [SerializeField] private GameObject gameOverMenu;
    public Transform spawnArea;
    public int minSpawnBonuses = 0;                
    public int maxSpawnBonuses = 3;                
    public float moveValue = -1f;
    public int maxAttempts = 20;    
    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private List<GameObject> spawnedBonuses = new List<GameObject>();
    private int count;
    private float checkDistance = 0.5f;

    private int wave;
    void Start()
    {
        Time.timeScale = 1;
        wave = 0;
        StartGame();
    }

    private void StartGame()
    {
        wave++;
        GenerateObjects();
    }
    
    public void NextWave()
    {
        wave++;
        GenerateObjects();
        MoveObstacles();
    }

    private void GenerateObjects()
    {
        DeleteAllBonuses();
        count = wave >= 9 ? 9 : wave;
        
        GenerateObstacles();
        GenerateBonuses();
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < count; i++)
        {
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Vector3 randomPosition = GetRandomPositionInSpawnArea();

                if (!IsOverlap(randomPosition, obstacleLayer))
                {
                    GameObject newObstacle = Instantiate(obstaclePrefabs[Random.Range(0, count)], randomPosition, Quaternion.identity);
                    spawnedObstacles.Add(newObstacle);

                    break;
                }

                attempts++;
            }
        }
    }
    
    private Vector3 GetRandomPositionInSpawnArea()
    {
        Vector3 min = spawnArea.position - spawnArea.localScale / 2f;
        Vector3 max = spawnArea.position + spawnArea.localScale / 2f;

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);

        return new Vector3(randomX, randomY, 0f);
    }

    private bool IsOverlap(Vector3 position, LayerMask layerMask) => Physics2D.OverlapCircle(position, checkDistance, layerMask);

    private void GenerateBonuses()
    {
        for (int i = 0; i < Random.Range(minSpawnBonuses, maxSpawnBonuses); i++)                                                                       
        {                                                                                                     
            int attempts = 0;                                                                                 
                                                                                                      
            while (attempts < maxAttempts)                                                                    
            {                                                                                                 
                Vector3 randomPosition = GetRandomPositionInSpawnArea();                                      
                                                                                                      
                if (!IsOverlap(randomPosition, obstacleLayer))                                                               
                {                                                                                             
                    GameObject newBonus = Instantiate(bonusPrefab, randomPosition, Quaternion.identity);
                    spawnedBonuses.Add(newBonus);
                    
                    break;                                                                                    
                }                                                                                             
                                                                                                      
                attempts++;                                                                                   
            }                                                                                                 
        }                                                                                                     
    }

    private void MoveObstacles()
    {
        for (int i = 0; i < spawnedObstacles.Count; i++)
        {
            spawnedObstacles[i].transform.position = new Vector3(spawnedObstacles[i].transform.position.x,
                spawnedObstacles[i].transform.position.y + moveValue, spawnedObstacles[i].transform.position.z);

            if (spawnedObstacles[i].transform.position.y - spawnedObstacles[i].transform.localScale.y <= line.position.y)
            {
                Time.timeScale = 0;
                gameOverMenu.SetActive(true);
            }
        }
    }

    public void DeleteObstacle(GameObject obstacle)
    {
        spawnedObstacles.Remove(obstacle);
    }

    private void DeleteAllBonuses()
    {
        for (int i = 0; i < spawnedBonuses.Count; i++)
        {
            Destroy(spawnedBonuses[i]);
        }
        spawnedBonuses.Clear();
    }
}
