using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class DirtStatus : MonoBehaviour
{
    private TerrainCollection DirtStatus_TerrainCollection;
    private int wood;
    private int water;
    private int dirt;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        wood = TerrainCollection.WoodCount;
        water = TerrainCollection.WaterCount;
        dirt = TerrainCollection.DirtCount;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (wood > 0 && water > 0)
        {
            PlayerController.speed = 7;
            TerrainCollection.WoodCount--;
            TerrainCollection.WaterCount--;
            TerrainCollection.DirtCount++;
            Destroy(this.gameObject);
            // ToDo  火车加相应车厢
        }
        
    }
}
