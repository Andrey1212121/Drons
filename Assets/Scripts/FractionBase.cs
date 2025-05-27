using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractionBase : MonoBehaviour
{
    public string name;
    public Color colorDron;
    public int numDrons = 1;
    
    public Transform transform;

    public float score;

    public GameObject DronPrefab;

    private List<GameObject> dronsArr = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dronsArr.Count < numDrons)
        {
            GameObject dron = Instantiate(DronPrefab, transform.position, Quaternion.identity);
            Dron dronSc = dron.GetComponent<Dron>();
            dronSc.baseHome = this;

            dronsArr.Add(dron);

        }
        else if (dronsArr.Count > numDrons)
        {
            Destroy(dronsArr[0]);
            dronsArr.RemoveAt(0);
        }

    }

    public void ChangeSpeed(int newSpeed)
    {
        foreach (var dron in dronsArr)
        {
            dron.GetComponent<Dron>().speed = newSpeed;
        }
        
    }

    public void AddScore()
    {
        score++;
    }
}
