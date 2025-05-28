using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron : MonoBehaviour
{
    public float speed = 1f;
    public FractionBase baseHome;
    public Transform selfTrans;
    private Resource targetResource;
    private bool isMoving = false;
    private bool isLooting = false;
    private bool isReturningHome = false; 

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = baseHome.colorDron;
        FindResource();
    }

    void Update()
    {
        if (isMoving && targetResource != null && !isLooting && !isReturningHome)
        {
            // движение к ресурсам
            MoveTowards(targetResource.transform.position);

            float dist = Vector2.Distance(selfTrans.position, targetResource.transform.position);
            if (dist < 0.1f)
            {
                isMoving = false;
                Loot();
            }
        }
        else if (isReturningHome && !isLooting)
        {
            // возврат домой
            MoveTowards(baseHome.transform.position);

            float distHome = Vector2.Distance(selfTrans.position, baseHome.transform.position);
            if (distHome < 0.1f)
            {
                isReturningHome = false;
                UnLoadResource();
            }
        }
    }

    public void FindResource()
    {
        //Debug.Log("find resource");
        Resource[] allResource = FindObjectsOfType<Resource>();
        float minDist = Mathf.Infinity;
        Resource selectedResource = null;

        foreach (var resource in allResource)
        {
            if (!resource.isSelect)
            {
                float dist = Vector2.Distance(selfTrans.position, resource.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    selectedResource = resource;
                }
            }
        }

        if (selectedResource != null)
        {
            targetResource = selectedResource;
            targetResource.isSelect = true;
            isMoving = true;
            return;
        }
        else
        {
            StartCoroutine(WaitBeforeFindResource());
        }
    }

    private IEnumerator WaitBeforeFindResource()
    {
        yield return new WaitForSeconds(1f);
        FindResource();
    }

    private void MoveTowards(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - selfTrans.position).normalized;
        selfTrans.position += direction * speed * Time.deltaTime;
    }

    private void Loot()
    {
        if (!isLooting && targetResource != null)
        {
            StartCoroutine(LootCoroutine());
            Debug.Log("Wait looting;");
        }
    }

    private IEnumerator LootCoroutine()
    {
        isLooting = true;

    
        yield return new WaitForSeconds(2f);
        Debug.Log("Go home");
        Destroy(targetResource.gameObject);
        targetResource = null;

        isLooting = false;

        GoHome();
    }

    private void GoHome()
    {
        isReturningHome = true;
        isMoving = false;
    }

    private void UnLoadResource()
    {
        baseHome.AddScore();
        FindResource();
    }
}
