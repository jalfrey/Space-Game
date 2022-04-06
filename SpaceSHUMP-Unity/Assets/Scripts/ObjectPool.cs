/**** 
 * Created by: Jason Alfrey
 * Date Created: April 6, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 6, 2022
 * 
 * Description: Pool of objects for reuse.
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Vars 
    #region Pool Singleton
    static public ObjectPool POOL;

    void CheckPoolIsInScene()
    {
        if (POOL == null)
            POOL = this;
        else
        {
            Debug.Log("POOL.Awake attempted to assign a second object pool");
        }
    }
    #endregion

    private Queue<GameObject> projectiles = new Queue<GameObject>();

    [Header("Pool Settings")]
    public GameObject projectilePrefab;
    public int poolStartSize = 5;

    private void Awake()
    {
        CheckPoolIsInScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolStartSize; i++)
        {
            GameObject gObject = Instantiate(projectilePrefab);
            projectiles.Enqueue(gObject);
            gObject.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        if(projectiles.Count > 0)
        {
            GameObject gObject = projectiles.Dequeue();
            gObject.SetActive(true);
            return gObject;
        }
        else
        {
            Debug.LogWarning("Out of projectiles. Reloading");
            return null;
        }
    }

    public void ReturnObjects(GameObject gObject)
    {
        projectiles.Enqueue(gObject);
        gObject.SetActive(false);
    }
}
