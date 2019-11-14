using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSunflower : MonoBehaviour
{
    public int pointCount;
    public GameObject sphere;
    public float turnFraction;
    public float exponent;

    GameObject[] points;
    static Vector3[] directions;
    public int viewDirections = 20;

    private void Start() {
        points = new GameObject[pointCount];
        for(int i=0; i<pointCount;i++){
            points[i] = Instantiate(sphere, Vector3.zero, Quaternion.identity);
        }
    }

    void PlotPoint(float x, float y, int index){
        points[index].transform.position = new Vector3(x, y, 0f);
    }
    
    void Plot3DPoint(float x, float y, float z, int index){
        points[index].transform.position = new Vector3(x, y, z);
        //print(points[index].transform.position);
    }

    void GetPointOnSunflower(){
            for(int i=0; i<pointCount; i++){
                float distance = i / (pointCount-1f);
                distance = Mathf.Pow(distance, exponent);
                float angle = 2 * Mathf.PI * turnFraction * i;

                float x = distance * Mathf.Cos(angle);
                float y = distance * Mathf.Sin(angle);

                PlotPoint(x, y, i);
            }
    }

    void GetPointsOnSphere(){
        directions = new Vector3[pointCount];
        
        float goldenRatio = 1.61803398875f;
        float angleIncrement = 2 * Mathf.PI * goldenRatio; //golden ratio is turn radius

        for(int i=0; i<pointCount; i++){
            float distance = (float)i/ pointCount;
            float phi = Mathf.Acos(1 - 2*distance);
            float theta = angleIncrement * i;
            
            float x = Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = Mathf.Cos(phi);
            Plot3DPoint(x, y, z, i);
        }
    }
    

    private void Update() {
        //GetPointOnSunflower();
        GetPointsOnSphere();
    }
}
