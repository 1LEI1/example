using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class FireStatus : MonoBehaviour
{
    private TerrainCollection   FireStatus_TerrainCollection;
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

        if ( water > 0)
        {
            PlayerController.speed = 10;
            TerrainCollection.WaterCount--;
           TerrainCollection.FireCount++;

            //currentIndex = FireStatus_TerrainCollection.trainBodys.Length;
            //TerrainCollection.length++;
            //AddFireBody();
            Destroy(this.gameObject);
            // ToDo  火车加相应车厢
        }

    }
    //void AddFireBody()
    //{
    //    GameObject fireBody = Instantiate(FireBodyPrefab) as GameObject;
    //    fireBody.transform.position = TerrainCollection.lastOnePostion;
    //}
}
