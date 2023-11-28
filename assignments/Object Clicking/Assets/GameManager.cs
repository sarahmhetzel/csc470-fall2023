using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public event Action<UnitScript> UnitSelectedHappened;

    // singleton 
    public static GameManager SharedInstance;
    public List<UnitScript> units = new List<UnitScript>();

    public GameObject unitPrefab;
    public GameObject objectPrefab;
    public GameObject housePrefab;

    UnitScript selectedUnit;


    
    void Awake()
    {
        if (SharedInstance != null)
        {
            Debug.Log("why is there more than one GameManager?!?!");
        }
        SharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999999, ~LayerMask.NameToLayer("ground")))
            {
                if (selectedUnit != null)
                {
                    selectedUnit.SetTarget(hit.point);
                }

                //if (Input.GetKey(KeyCode.Space))
                //{
                // if we get in here, we hit something
                // the 'hit' object contains info about what we hit
                //GameObject unit = Instantiate(unitPrefab, hit.point, Quaternion.identity);
                //units.Add(unit.GetComponent<UnitScript>());
                // }
                // else if (Input.GetKey(KeyCode.Return))
                // {
                //     GameObject unit = Instantiate(objectPrefab, hit.point, Quaternion.identity);
                //     units.Add(unit.GetComponent<UnitScript>());
                // }
            }
        }
    }

    public void SelectUnit(UnitScript unit)
    {
        // Deselect any units that think they are selected
        foreach (UnitScript u in units)
        {
            u.selected = false;
            u.SetUnitColor();
        }
        selectedUnit = unit;
        selectedUnit.selected = true;
        selectedUnit.SetUnitColor();

        UnitSelectedHappened?.Invoke(unit);
    }
}