using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class CubeRotate : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" &&Input.GetKeyDown(KeyCode.J))
        {
            this.transform.Rotate(0, 90, 0);
        }
    }
}
