using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawController : MonoBehaviour
{

    public GameObject RailPrefab;
    bool dragging = false;
    float movedeltatospawn = 110f;

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

            if(distance >= movedeltatospawn)
            {
                SpawnRail();
                distance = 0;
            }
        }

    }

    private void SpawnRail()
    {

        Vector3 spawnPosition = main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,10));
        Quaternion rotation;
        var dir = lastSpawnPoint - mousePos;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)+90;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Instantiate(RailPrefab, spawnPosition, rotation,null);
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
