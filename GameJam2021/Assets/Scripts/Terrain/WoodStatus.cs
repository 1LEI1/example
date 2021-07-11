using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class WoodStatus : MonoBehaviour
{
    private TerrainCollection WoodStatus_TerrainCollection;
    private int fire;
    private int wood;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        WoodStatus_TerrainCollection = player.GetComponent<TerrainCollection>();
    }
    private void Update()
    {
        wood = TerrainCollection.WoodCount;
        fire = TerrainCollection.FireCount;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (fire > 0)
        {
            PlayerController.speed = 10;
            TerrainCollection.FireCount --;
           TerrainCollection.WoodCount++;
            Destroy(this.gameObject);
            
        }

    }
}
