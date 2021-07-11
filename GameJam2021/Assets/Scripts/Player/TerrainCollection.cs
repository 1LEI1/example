using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class TerrainCollection : MonoBehaviour
{
    //public static int length; 
    //[SerializeField ]
    //public  GameObject[] trainBodys = new GameObject[length];
    //[SerializeField]
    //private GameObject terrainPrefab;

    //public static  Vector3 lastOnePostion = new Vector3(0, 0.15f, -0.9f);


    public static  int WaterCount;
    public static  int DirtCount;
    public static  int FireCount;
    public static  int SnowCount;
    public static int WoodCount;
    private void Start()
    {
        WaterCount = 1;
        DirtCount = 1;
        FireCount = 1;
        SnowCount = 1;
        WoodCount = 1;



    }


}
