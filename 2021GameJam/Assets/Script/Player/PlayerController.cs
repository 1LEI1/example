using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2;
    private float rotateSpeed = 300;
    private Rigidbody rigidbody01;
    private void Start()
    {
        rigidbody01 = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float left_right_move = Input.GetAxis("Horizontal");
        float forword_back_move = Input.GetAxis("Vertical");
        CoordinateTransformation(left_right_move, forword_back_move);
        float left_right_view = Input.GetAxis("Mouse X");
        CoordinateRotation(left_right_view);

    }
    private void CoordinateTransformation(float left_right_move, float forword_back_move)
    {
        left_right_move *= moveSpeed * Time.deltaTime;
        forword_back_move *= moveSpeed * Time.deltaTime;
        rigidbody01.AddRelativeForce(new Vector3(left_right_move * 1000, 0, forword_back_move * 1000));
    }
    private void CoordinateRotation(float left_right_view)
    {
        left_right_view *= rotateSpeed * Time.deltaTime;
        this.transform.Rotate(0, left_right_view, 0, Space.World);
    }
}
