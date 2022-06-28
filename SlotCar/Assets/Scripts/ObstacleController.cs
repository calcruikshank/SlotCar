using Gameboard;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : Gameboard.Utilities.Singleton<ObstacleController>
{
    private GameboardTouchController touchController;
    public List<Obstacle> Obstacles;
    public Obstacle ObstaclePrefab;

    private bool shouldUpdate 
    { 
        get
        {
            return timeSinceUpdate > updateInterval;
        } 
    }

    public float updateInterval = 0.2f;
    private float timeSinceUpdate = 0f;
    private bool isUpdating = false;

    private void Awake()
    {
        Obstacles = new List<Obstacle>();
        touchController = Gameboard.Gameboard.Instance.boardTouchController;
        touchController.boardTouchHandler.BoardObjectsUpdated += onBoardObjectsUpdated;
        touchController.boardTouchHandler.NewBoardObjectsCreated += onBoardObjectsCreated;
        touchController.boardTouchHandler.BoardObjectSessionsDeleted += onBoardObjectDeleted;
    }

    private void onBoardObjectDeleted(object sender, List<uint> e)
    {
        if (e.Count > 0)
            throw new NotImplementedException();
    }

    private void onBoardObjectsCreated(object sender, List<TrackedBoardObject> objects)
    {
        if (objects.Count > 0)
        {
            // 
            Debug.Log($"onBoardObjectsCreated sender {sender} objects {objects}");
        }
    }

    private void onBoardObjectsUpdated(object sender, List<TrackedBoardObject> objects)
    {
        if (objects.Count > 0)
        {
            if (shouldUpdate)
            {
                isUpdating = true;
                UpdateObstacles(objects);
                
                Debug.Log($"onBoardObjectsUpdated sender {sender} objects {objects}");
            }
        }
    }

    // TODO: this needs optimization
    private void UpdateObstacles(List<TrackedBoardObject> objects)
    {
        // Remove existing 
        Obstacles.ForEach(o => Destroy(o.gameObject));
        Obstacles = new List<Obstacle>();

        // Add new ones
        objects.ForEach(o => {
            // In order to have a mesh with the points we need to add triangles which don't get passed so we would have to make them
            // instead  we could just make a cylinder and then scale it to the extents
            var newObstacle = Instantiate(ObstaclePrefab.transform.gameObject, this.gameObject.transform);
            if (newObstacle.TryGetComponent(out Obstacle obstacle))
            {
                Obstacles.Add(obstacle);
                // Get the bounds for the array of vectors
                var newBounds = new Bounds();
                for (var i=0; i < o.contourWorldVectors3D.Length; i++)
                {
                    newBounds.Encapsulate(o.contourWorldVectors3D[i]);
                }
                obstacle.Collider.center = o.sceneWorldPosition;
                obstacle.Collider.radius = UnityEngine.Mathf.Max(newBounds.size.x, newBounds.size.y);
                obstacle.Collider.height = newBounds.size.z;
            }
            
        });

        timeSinceUpdate = 0f;
        isUpdating = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdating)
            return;
        timeSinceUpdate += Time.deltaTime;
    }
}
