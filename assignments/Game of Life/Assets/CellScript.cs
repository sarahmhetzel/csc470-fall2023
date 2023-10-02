using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool alive = false;
    public int liveNeighbors;
    public bool nextAlive;
    bool onceAlive = false;
    float finalHeight;

    public int x = -1;
    public int z = -1;

    public Color aliveColor;
    public Color deadColor;
    public Color onceAliveColor;

    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponentInChildren<Renderer>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if(finalHeight <= 100)
            {
                Vector3 scale = rend.transform.localScale;
                rend.transform.localScale = new Vector3(1, scale.y + 0.1f, 1);
                finalHeight++;
            }
            
        }
    }


    // updating the color of the cell depending on status
    public void UpdateColor()
    {
        if (alive)
        {
            onceAlive = true;
            rend.material.color = aliveColor;
        }
        else
        {
            if (onceAlive)
            {
                rend.material.color = onceAliveColor;
            }
            else
            {
                rend.material.color = deadColor;
            }
        }
    }

    //private void OnMouseDown()
    //{
    //    alive = !alive;
    //    UpdateColor();
    //    Debug.Log(x + ", " + z + " alive: " + alive);
    //}

}
