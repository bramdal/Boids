using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoids : MonoBehaviour
{
    public GameObject boid;
    public int boidCount;

    private void Awake() {
        for(int i=0; i<boidCount; i++){
            Instantiate(boid, transform.position + Random.insideUnitSphere * 5, Quaternion.Euler(Random.insideUnitSphere));
        }
    }
}
