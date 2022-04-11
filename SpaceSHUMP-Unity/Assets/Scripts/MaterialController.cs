/**** 
 * Created by: Jason Alfrey
 * Date Created: April 11, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 11, 2022
 * 
 * Description: Scrolling background generation. 
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    private Material goMat;
    private Renderer goRenderer;
    private Vector2 offset; 
    public Vector2 scrollSpeed = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        goRenderer = GetComponent<Renderer>();
        goMat = goRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = (scrollSpeed * Time.deltaTime);
        goMat.mainTextureOffset += offset;
    }
}
