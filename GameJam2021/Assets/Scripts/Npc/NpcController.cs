using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))] //添加约束，当脚本拖拽到物体上时，如果没有NavMeshAgent,那么就会自动添加NavMeshAgent
public class NpcController : MonoBehaviour
{
    private NavMeshAgent agent;


    private GameObject player;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        MoveToPlayer(player.transform.position);
    }
    public void MoveToPlayer(Vector3 playerPosition)
    {
        agent.SetDestination(playerPosition);
    }



    }
