using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class SnowStatus : MonoBehaviour
{
    private TerrainCollection SnowStatus_TerrainCollection;
    public int wood;
    public int fire;
    private int snow;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        wood = TerrainCollection.WoodCount;
        fire = TerrainCollection.FireCount;
        snow = TerrainCollection.SnowCount;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (wood > 0 && fire > 0)
        {
            PlayerController.speed = 7;
            TerrainCollection.WoodCount--;
            TerrainCollection.FireCount--;
            TerrainCollection.SnowCount++;
            Destroy(this.gameObject);
            
        }

    }
}
