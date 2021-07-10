using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class NpcCreat : MonoBehaviour
{
    public GameObject player;
    //Enemys
    public List<GameObject> targetEnemy = new List<GameObject>();
    //��������λ�ú����ĵ�λ��
    public Transform[] centrePoints;

    private float[] distances;
    public float detectingRange;
    private void Start()
    {
        distances = new float[centrePoints.Length];
    }
    private void Update()
    {
        for (int i = 0; i < centrePoints.Length; i++)
        {
            distances[i] = Vector3.Distance(player.transform.position, centrePoints[i].position);
        }



        int n = 0;
        for (int i = 0; i < centrePoints.Length - 1; i++)
        {
            if (distances[i + 1] < distances[i])
            {
                n = i + 1;
            }
        }
        if (distances[n] <= detectingRange)
        {
            centrePoints[n].GetComponent<NpcCreatPoint>().distanceRequire = true;
        }


    }
}
