using UnityEngine;
using Gameboard;

[RequireComponent(typeof(CapsuleCollider))]
public class Obstacle : MonoBehaviour
{
    public CapsuleCollider Collider;

    private void Awake()
    {
        Collider = GetComponent<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out CarController car))
        {
            Debug.Log("collision happened");
            // TODO: would need to bounce back here based on the direction of the collision
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
