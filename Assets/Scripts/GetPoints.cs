using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetPoints 
{

    //public static int viewDirections = 20;
    static Vector3[] directions;

    public static Vector3[] GetPointsOnSphere(this Transform transform, int viewDirections){
        directions = new Vector3[viewDirections];
        
        float goldenRatio = 1.61803398875f;
        float angleIncrement = 2 * Mathf.PI * goldenRatio; //golden ratio is turn radius

        for(int i=0; i<viewDirections; i++){
            float distance = (float)i/ viewDirections;
            float phi = Mathf.Acos(1 - 2*distance);
            float theta = angleIncrement * i;
            
            float x = Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = Mathf.Cos(phi);
            directions[i] = new Vector3(x, y, z);
            directions[i] = transform.TransformDirection(directions[i]);
            //Debug.Log(directions[i]);
        }
        return directions;
    }
}
