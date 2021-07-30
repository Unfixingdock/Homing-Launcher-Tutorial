using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    [SerializeField]float dirX, moveSpeed;
    bool moveRight = true;

    private void Update() 
    {
      if (transform.position.x > 4f)
      moveRight = false;
      if (transform.position.x < -4f)
      moveRight = true;
       
       if (moveRight)
       transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, 1.5f, 0);
       else
       transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, 1.5f, 0);
    }
}
