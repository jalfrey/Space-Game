/**** 
 * Created by: Jason Alfrey
 * Date Created: March 28, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 28, 2022
 * 
 * Description: Spawn enemies
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy settings")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond;
    public float enemyDefaultPadding;

    private BoundsCheck bndCheck;

    
    // Start is called before the first frame update
    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
    
    void SpawnEnemy()
    {
        Debug.Log("Enemy spawned");
        // Select a random enemy to instantiate 
        int index = Random.Range(0, prefabEnemies.Length);
        GameObject obj = Instantiate<GameObject>(prefabEnemies[index]);

        // Position the enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;
        if(obj.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(obj.GetComponent<BoundsCheck>().radius);
        }

        // Set the initial position
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;

        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        obj.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
}