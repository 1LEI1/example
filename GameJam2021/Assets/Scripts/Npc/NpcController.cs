using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))] //���Լ�������ű���ק��������ʱ�����û��NavMeshAgent,��ô�ͻ��Զ����NavMeshAgent
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
