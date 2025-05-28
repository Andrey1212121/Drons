using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject resourcePrefab;
    public float delay = 5f;

    float minX = -5f;
    float maxX = 5f;
    float minY = -5f;
    float maxY = 5f;

    void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(resourcePrefab, randomPos, Quaternion.identity);
        }
    }


    public void SetSpeedDron(float newSpeed)
    {
        Dron[] allDrons = FindObjectsOfType<Dron>();
        foreach (var dron in allDrons)
        {
            dron.speed = newSpeed * 10f;
        }
    }


    public void SetTrail(bool isTrail)
    {
        Debug.Log("Change checkbox");
        Debug.Log(isTrail);
        Dron[] allDrons = FindObjectsOfType<Dron>();
        foreach (var dron in allDrons)
        {
            dron.GetComponent<TrailRenderer>().enabled = isTrail;
        }
    }

    public void SetDelay(float newDelay)
    {
        delay = newDelay;
    }
}
