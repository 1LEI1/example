using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class textController : MonoBehaviour
{
    Flowchart flow;
    public string f;
    private void Awake()
    {
        flow = GameObject.Find("Flowchart").GetComponent<Flowchart>();
    }
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") flow.ExecuteBlock(f);

    }
    public void Bag()
    {
        flow.ExecuteBlock("Bag");
    }
}
