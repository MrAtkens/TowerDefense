using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Shooting settings")]

    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireRate;

    private float fireCountdown=0;

    [SerializeField]
    private float radius;

    [Header("Rotation settings")]

    [SerializeField]
    private Transform partToRotate;

   [SerializeField]
    private float speed;

    private Transform target;

    private List<Transform> enemies = new List<Transform>();


    private void Start()
    {
        InvokeRepeating("CheckTarget", 0f, 0.3f);
    }

    // Update is called once per frame
    private void Update()
    {
        if(target == null)
            return;
        Rotate();

        if(fireCountdown <= 0){
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void Shoot(){
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.Seek(target);
    }

    private void CheckTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        for(int i=0; i < enemies.Length; i++){
            var enemy = enemies[i];
            var distanceToEnemy = Vector3.Distance(
                transform.position,
                enemy.transform.position
            );
            if(distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= radius){
            target = nearestEnemy.transform;
        }
        else{
            target = null;
        }
    }

    private void Rotate(){
        var direction = target.position - transform.position;
        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation, Time.deltaTime * speed).eulerAngles;
        
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy"){
            enemies.Add(other.transform);
            if(target == null){
            target= enemies[0];
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.tag == "Enemy"){
            if(enemies.Count > 0){
                enemies.RemoveAt(0);
            }
        }
    }
}
