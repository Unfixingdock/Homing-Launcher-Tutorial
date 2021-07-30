using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] float Speed;
   [SerializeField] float JumpHeight;
   bool canJump;
   Rigidbody rb;
   float horizontal;
   float vertical;
   
   private void Start() 
   {
      rb = GetComponent<Rigidbody>();   
   }

   private void Update() 
   {
     horizontal = Input.GetAxisRaw("Horizontal");
     vertical = Input.GetAxisRaw("Vertical");

     Vector3 move = new Vector3(horizontal, 0, vertical);
     transform.Translate(move * Speed * Time.deltaTime);

     if (canJump && Input.GetKeyDown(KeyCode.Space))
     {
         rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
     }    
   }

   private void OnCollisionEnter(Collision other) 
   {
       if (other.gameObject.tag == "Ground")
       {
           canJump = true;
       }
   }

   private void OnCollisionExit(Collision other) 
   {
          if (other.gameObject.tag == "Ground")
       {
           canJump = false;
       }
   }
}
