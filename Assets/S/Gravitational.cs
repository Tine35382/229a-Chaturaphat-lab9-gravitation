using UnityEngine;
using System.Collections.Generic;

public class Gravitational : MonoBehaviour
{
    public static List<Gravitational> otherGameObject;
    private Rigidbody rb;
    const float G = 0.006674f; 

    
    [SerializeField] bool planet = false; 
    [SerializeField] float orbitSpeed = 1; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherGameObject == null) { otherGameObject = new List<Gravitational>(); }
        otherGameObject.Add(this);

        if (!planet)
        {
           
            rb.AddForce(Vector3.left * orbitSpeed, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        foreach (Gravitational obj in otherGameObject)
        {
            if (obj != this) 
            {
                AttractionForce(obj);
            }
        }
    }

    void AttractionForce(Gravitational other)
    {
        Rigidbody otherRB = other.rb;
        Vector3 direction = rb.position - otherRB.position;
        float distance = direction.magnitude;

        if (distance == 0f)
        {
            return;
        }

        
        float forceMagnitude = G * (rb.mass * otherRB.mass / Mathf.Pow(distance, 2));
        Vector3 gravitationalForce = forceMagnitude * direction.normalized;
        otherRB.AddForce(gravitationalForce);
    }
}
