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

        //length = 2;

    }
    private void Update()
    {
    //    if (Input.GetKeyDown(KeyCode.J))
    //    {
    //        AddBody();
    //    }

        //lastOnePostion = trainBodys[trainBodys.Length - 1].transform.position;

       //for (int i = trainBodys.Length - 1; i >= 1; i--)
       //{
       //    trainBodys[i].transform.position = trainBodys[i - 1].transform.position;
       //     trainBodys[i].transform.rotation = trainBodys[i - 1].transform.rotation;
       //}

    }
    //void AddBody()
    //{
    //    GameObject go = Instantiate(terrainPrefab) as GameObject;
    //    go.transform.position = lastOnePostion;
    //    //trainBodys.Add(go);

    //}

}
