using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoidAttributes", menuName = "Idea pad/BoidAttributes", order = 0)]
public class BoidAttributes : ScriptableObject {
    public float flightSpeed = 1f;

    public float boidPerceptionRadius = 4f;
    public float boidAvoidanceRadius = 1f;
    
    //object collision
    public float spherecastDistance = 5f;
    public float objectAvoidanceRadius = 0.25f;
    public int obstacleMask = 9;
    public int viewDirection = 300;

    //weights defined by Parker
    // public float cohesionWeight = 0.01f;
    // public float alignmentWeight = 0.125f;
    // public float seperationWeight = 1f;
    // public float objectAvoidanceWeight = 20f;

    public float cohesionWeight = 0.5f;
    public float alignmentWeight = 0.125f;
    public float seperationWeight = 1f;
    public float objectAvoidanceWeight = 100f;
}
