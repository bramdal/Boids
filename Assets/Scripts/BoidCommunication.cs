using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoidCommunication{
    static BoidBehaviour[] boids;
    static Vector3 flockCentrePosition;
    static Vector3 flockAlignmentDirection;
    static Vector3 boidAvoidanceDirections;

    static BoidCommunication(){
        boids = MonoBehaviour.FindObjectsOfType<BoidBehaviour>();
    }

    public static Vector3[] GetOtherBoids(BoidBehaviour callingBoid){
        Vector3 offsetFromVicinityBoids = Vector3.zero;
        int flockmates = 0;
        foreach (var boid in boids){
            if(boid != callingBoid){
                float boidDistance = Vector3.Distance(callingBoid.transform.position, boid.transform.position);
                if(boidDistance<boid.boidData.boidPerceptionRadius){
                    flockmates++;
                    flockCentrePosition += boid.transform.position;
                    flockAlignmentDirection += boid.velocity;
                    Vector3 offsetOtherBoids = boid.transform.position - callingBoid.transform.position;
                    if(boidDistance <boid.boidData.boidAvoidanceRadius){
                        offsetFromVicinityBoids -= offsetOtherBoids;
                    }
                }
            }
        }
        int totalCount;
        if(flockmates!=0)
            totalCount = flockmates;
        else
            totalCount = 1;    
        //Vector3 observerPosition = callingBoid.transform.position;

        //flockCentrePosition = ReturnPerceivedVector(flockCentrePosition, observerPosition, totalCount);
        flockAlignmentDirection /= totalCount;
        flockAlignmentDirection = flockAlignmentDirection - callingBoid.transform.position;
        //flockAlignmentDirection = ReturnPerceivedVector(flockAlignmentDirection, observerPosition, totalCount);
        flockAlignmentDirection /= totalCount;
        flockAlignmentDirection = flockAlignmentDirection - callingBoid.velocity;
        boidAvoidanceDirections = offsetFromVicinityBoids;

        Vector3[] returnValues = {flockCentrePosition, flockAlignmentDirection, boidAvoidanceDirections};
        return returnValues;
    }

    public static Vector3 ReturnPerceivedVector(Vector3 vector, Vector3 observerPosition, int totalCount){
        vector /= (totalCount);
        vector.Normalize();
        vector *= 5f;
        vector -=  observerPosition;
        
        return vector;
    }
}
