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

    private List<GameObject> dronsArr = new List<GameObject>(10);
    

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject dron = Instantiate(DronPrefab, transform.position, Quaternion.identity);
            Dron dronSc = dron.GetComponent<Dron>();
            dronSc.baseHome = this;

            dronsArr.Add(dron);
            dronsArr[i].GetComponent<Dron>().enabled = false;
        }

    }

    public void AddScore()
    {
        score++;
    }

    public void SetNumDrons(float nums)
    {
        numDrons = (int)(nums * 10);
        Debug.Log(numDrons);

        int activeCount = Mathf.Clamp(numDrons, 0, dronsArr.Count);
        for (int i = 0; i < activeCount; i++)
        {
            dronsArr[i].GetComponent<Dron>().enabled = true;
        }
        for (int i = activeCount; i < dronsArr.Count; i++)
        {
            dronsArr[i].GetComponent<Dron>().enabled = false;
            dronsArr[i].transform.position = transform.position;
        }

    }
}
