using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirePlus : MonoBehaviour
{
    private TerrainCollection FireStatus_TerrainCollection;
    private int water;
    private int fire;
    private GameObject player;
    private int currentIndex;

    [SerializeField]
    private GameObject FireBodyPrefab;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FireStatus_TerrainCollection = player.GetComponent<TerrainCollection>();
    }
    private void Update()
    {
        fire = TerrainCollection.FireCount;
        water = TerrainCollection.WaterCount;


    }
    private void OnCollisionEnter(Collision collision)
    {

        if (water > 0)
        {
            PlayerController.speed = 10;
            TerrainCollection.WaterCount--;
            TerrainCollection.FireCount += 2;
            Destroy(this.gameObject);
        }

    }
}
