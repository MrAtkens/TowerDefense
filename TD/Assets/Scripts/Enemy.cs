using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int healthPoint;
    public static int startHealtPoint;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int healtForEnemy;
    private Transform targetPoint;

    private int pointIndex=0;

    private void Start()
    {
        startHealtPoint = healtForEnemy;
        healthPoint = startHealtPoint;
        targetPoint = Points.points[0];
    }

    private void Update()
    {
       transform.position=Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

         var distance=Vector3.Distance(transform.position, targetPoint.position);

        if(distance <= 0.4f){
            GetNextPoint();
        }
    }

    private void GetNextPoint(){
        if(pointIndex >= Points.points.Length-1){
            Destroy(gameObject);
            PlayerStats.Lifes -= 1;
            return;

        }
        pointIndex++;
        targetPoint=Points.points[pointIndex];
    }
}
