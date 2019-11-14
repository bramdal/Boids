using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    public BoidAttributes boidData;

    int flockmates;
    Vector3 flockCentrePosition;
    Vector3 flockAlignemntDirection;
    Vector3 boidAvoidanceDirection;

    Vector3 targetOffset;

    //movement
    [HideInInspector]
    public Vector3 velocity;

    //layer mask
    int obstacleMaskIndex;

    public bool target;
    Transform flockTarget;

    private void Awake() {
        boidData = ScriptableObject.CreateInstance<BoidAttributes>();
        obstacleMaskIndex = LayerMask.NameToLayer("Obstacle");
        obstacleMaskIndex = ~obstacleMaskIndex;

        velocity = transform.forward;

        if(target)
            flockTarget = GameObject.FindWithTag("Target").transform;
    }

    private void Update() {
        Vector3 moveDirection = Vector3.zero;
        
        if(target || flockTarget!=null){
            targetOffset = flockTarget.position - transform.position;
            targetOffset = Vector3.ClampMagnitude(targetOffset, 5f);
            moveDirection += (targetOffset - velocity);
        }

        //do steering calculations here
        Vector3[] flockData = BoidCommunication.GetOtherBoids(this);
        var flockCentrePosition = Vector3.ClampMagnitude(flockData[0], 5f);
        var flockAlignemntDirection =  Vector3.ClampMagnitude(flockData[1], 5f);
        flockAlignemntDirection.Normalize();
        var boidAvoidanceDirection = Vector3.ClampMagnitude(flockData[2], 5f);

        moveDirection += (flockCentrePosition-velocity) * boidData.cohesionWeight;
        moveDirection += (flockAlignemntDirection - velocity) * boidData.alignmentWeight;
        moveDirection += (boidAvoidanceDirection-velocity) * boidData.seperationWeight; 

        if(CheckForCollision()){
            moveDirection += Vector3.ClampMagnitude(GetCollisionFreeDirection(), 3f) * boidData.objectAvoidanceWeight;
        }
           
        velocity += moveDirection * boidData.flightSpeed * Time.deltaTime;
        //clamp speed
        float tempSpeed = velocity.magnitude;
        Vector3 tempDirection = velocity/tempSpeed;
        tempSpeed = Mathf.Clamp(tempSpeed, 3f, 5f);
        velocity = tempSpeed*tempDirection;
        
        Vector3 holdCurrentPosition = transform.position;
        holdCurrentPosition += velocity * Time.deltaTime;
        if(moveDirection != Vector3.zero)
            transform.forward = tempDirection;
        transform.position = holdCurrentPosition;       
    }

    bool CheckForCollision(){
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, boidData.objectAvoidanceRadius, transform.forward, out hit, boidData.spherecastDistance, obstacleMaskIndex))
            return true;
        return false;
    }

    Vector3 GetCollisionFreeDirection(){
        Vector3 direction = transform.forward;
        Vector3[] directions = transform.GetPointsOnSphere(300);    // make variable for view directions later
        for(int i=0; i<directions.Length; i++){
            //print(directions[i]);
            Ray dir = new Ray(transform.position, directions[i]);
            if(!Physics.SphereCast(dir, boidData.objectAvoidanceRadius, boidData.spherecastDistance, obstacleMaskIndex)){
                direction = directions[i];
                direction.Normalize();
                return direction;
            }
        }
        return transform.forward;
    }
}
