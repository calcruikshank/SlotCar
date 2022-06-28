using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawController : MonoBehaviour
{

    public GameObject RailPrefab;
    public GameObject RailParent;
    bool dragging = false;
    public float MoveDistancetospawn = 110f;
    public float SpawnDepth = 20;

    Vector2 lastSpawnPoint;
    Camera main;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
    }


    void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
        if (dragging)
        {

            float distance = Vector2.Distance(mousePos, lastSpawnPoint);

            if(distance >= MoveDistancetospawn)
            {
                SpawnRail();
                distance = 0;
            }
        }

    }

    public void ClearScreen()
    {
        for(int i = 0; i< RailParent.transform.childCount; i++)
        {
            Destroy(RailParent.transform.GetChild(i).gameObject);
        }
    }

    private void SpawnRail()
    {

        Vector3 spawnPosition = main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,SpawnDepth));
        Quaternion rotation;
        var dir = lastSpawnPoint - mousePos;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)+90;
        rotation = Quaternion.Euler(0, -angle, 0);

        Instantiate(RailPrefab, spawnPosition, rotation,RailParent.transform);
        lastSpawnPoint = mousePos;
    }

    void OnClick()
    {
        dragging = true;
        lastSpawnPoint = mousePos;

    }
    void OnUnclick()
    {
        dragging = false;

    }
}
