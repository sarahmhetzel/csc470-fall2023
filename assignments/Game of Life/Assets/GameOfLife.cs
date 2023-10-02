using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{

    public GameObject cellPrefab;
    public CellScript[,] cells;

    int widthGame = 30;
    int heightGame = 30;

    // Start is called before the first frame update
    void Start()
    {
        cells = new CellScript[widthGame, heightGame];

        for (int x = 0; x < widthGame; x++)
        {
            for (int z = 0; z < heightGame; z++)
            {
                Vector3 pos = transform.position;
                float cellWidth = 1f;
                float spacing = 0.1f;
                pos.x = pos.x + x * (cellWidth + spacing);
                pos.z = pos.z + z * (cellWidth + spacing);
                GameObject cellObj = Instantiate(cellPrefab, pos, transform.rotation);

                cells[x, z] = cellObj.GetComponent<CellScript>();
                cells[x, z].x = x;
                cells[x, z].z = z;

                cells[x, z].alive = (Random.value < 0.2f);
            }
        }
    }

    // finding number of neighbors
    void GetLiveNeighbors()
    {
        for (int i = 0; i < widthGame; i++)
        {
            for (int j = 0; j < heightGame; j++)
            {
                int liveCells = 0;
                if (i+1 < widthGame)
                {
                    if (cells[i + 1, j].alive == true)
                    {
                        liveCells++;
                    }
                }
                if (i-1 >0)
                {
                    if (cells[i - 1, j].alive == true)
                    {
                        liveCells++;
                    }
                }
                if (j+1 < heightGame)
                {
                    if (cells[i, j + 1].alive == true)
                    {
                        liveCells++;
                    }
                }
                if (j-1 > 0)
                {
                    if (cells[i, j - 1].alive == true)
                    {
                        liveCells++;
                    }
                }
                if((i + 1 < widthGame) && (j + 1 < heightGame))
                {
                    if (cells[i + 1, j + 1].alive == true)
                    {
                        liveCells++;
                    }
                }
                if((i-1 > 0) && (j + 1 < heightGame))
                {
                    if (cells[i - 1, j + 1].alive == true)
                    {
                        liveCells++;
                    }
                }
                if((i-1 > 0) && (j-1 > 0))
                {
                    if (cells[i - 1, j - 1].alive == true)
                    {
                        liveCells++;
                    }
                }
                if((i + 1 < widthGame) && (j-1 > 0))
                {
                    if (cells[i + 1, j - 1].alive == true)
                    {
                        liveCells++;
                    }
                }
                cells[i, j].liveNeighbors = liveCells;
            }
        }
    }

    // determining the rules of the game and setting it in motion
    void StartSequence()
    {
        for(int x = 0; x < widthGame; x++)
        {
            for (int z = 0; z < heightGame; z++)
            {
                if(cells[x, z].alive == true)
                {
                    if(cells[x, z].liveNeighbors > 3 || cells[x, z].liveNeighbors < 2)
                    {
                        cells[x, z].alive = false;
                        cells[x, z].UpdateColor();
                    }
                }
                else
                {
                    if(cells[x, z].liveNeighbors == 3)
                    {
                        cells[x, z].alive = true;
                        cells[x, z].UpdateColor();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetLiveNeighbors();
        StartSequence();
    }
}