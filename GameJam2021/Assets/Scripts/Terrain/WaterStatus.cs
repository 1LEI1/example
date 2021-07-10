using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class WaterStatus : MonoBehaviour
{
    private TerrainCollection DirtStatus_TerrainCollection;
    private int dirt;
    private int water;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        dirt = TerrainCollection.DirtCount;
        water = TerrainCollection.WaterCount;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (dirt > 0)
        {
            PlayerController.speed = 10;
            TerrainCollection.DirtCount--;
            TerrainCollection.WaterCount ++;
            Destroy(this.gameObject);
            
        }

    }
}
