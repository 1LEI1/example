using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///
///<summary>
public class CherryText : MonoBehaviour
{
    private Text CherryNum;

    private void Start()
    {
        CherryNum = this.GetComponent<Text>();
    }
    private void Update()
    {
        CherryNum.text = string.Format("Cherry:{0:d}", PlayerController.Cherry);
    }
}
