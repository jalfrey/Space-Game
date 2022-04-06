/**** 
 * Created by: Jason Alfrey
 * Date Created: March 30, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 6, 2022
 * 
 * Description: Object pool
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturn : MonoBehaviour
{

    private ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        pool = ObjectPool.POOL;
    }

    private void OnDisable()
    {
        if(pool != null)
        {
            pool.ReturnObjects(this.gameObject);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
