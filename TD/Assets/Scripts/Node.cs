using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject turretObject;
    private TurretPriceList turret;

    [SerializeField]
    private Color hoverColor;
    
    private Color startColor;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material.color = startColor;
    }

    private void OnMouseEnter(){
        var turretToBuild = BuildManager.instance.GetTurretToBuild();
        if(turretToBuild == null){
            return;
        }

        if(PlayerStats.Money < turretToBuild.cost){
             rend.material.color = Color.red;
        }
        else{
             rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit(){
        rend.material.color = startColor;
    }

    private void OnMouseDown(){
        if(turret != null){
            Debug.Log("Can t build here");
            return;
        }

        turret = BuildManager.instance.GetTurretToBuild();

        if(turret == null){
            return;
        }

        if(PlayerStats.Money < turret.cost){
            Debug.Log("NOT ENOUGH MINERALS!!!");
            return;
        }


            turretObject = Instantiate(turret.prefab, transform.position + new Vector3(0,0.5f,0), transform.rotation);      
            Debug.Log(PlayerStats.Money);
            Debug.Log(turret.cost); 
            PlayerStats.Money -= turret.cost; 
    }
}
