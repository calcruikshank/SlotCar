using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] TrailRenderer trail; 
    [SerializeField] MeshCollider collider;
    [SerializeField] MeshFilter meshF;

    private void Start()
    {
    }
    private void Update()
    {
        DrawSwipe(trail);
    }
    public void DrawSwipe(TrailRenderer trail)
    {
        if (trail != null)
        {
            Mesh mesh = new Mesh();
            trail.BakeMesh(mesh, Camera.main, true);
            Mesh tempMesh = meshF.GetComponent<Mesh>();
            tempMesh = mesh;

            collider.sharedMesh = mesh;

        }
    }

}
