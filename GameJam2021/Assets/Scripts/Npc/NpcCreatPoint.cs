using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class NpcCreatPoint : MonoBehaviour
{
    public bool distanceRequire;
    public bool timeRequire;
    private List<GameObject> targetEnemy = new List<GameObject>();
    public int whichEnemy;
    private float lastTime;
    public float timeRange;
    public List<Transform> creatPoints = new List<Transform>();




    private void Start()
    {
        distanceRequire = false;
        timeRequire = false;
        targetEnemy = GetComponentInParent<NpcCreat>().targetEnemy;
        lastTime = -40;
    }
    private void Update()
    {
        JudgeTime();
        if (distanceRequire && timeRequire)
        {
            CreatEnemy();
            lastTime = Time.time;
            timeRequire = false;
            distanceRequire = false;
        }
    }
    private void JudgeTime()
    {
        if (Time.time - lastTime >= timeRange)
        {
            timeRequire = true;
        }
    }
    private void CreatEnemy()
    {
        for (int i = 0; i < creatPoints.Count; i++)
        {
            Instantiate(targetEnemy[whichEnemy], creatPoints[i].position, transform.rotation);
        }
    }
}
