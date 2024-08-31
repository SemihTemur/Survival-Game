using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuKontrolleri : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 4f;
    public LayerMask groundMask;// bu bizim belli yerlerde �arp��ma kontrolunu kontrol
                          // etmem�z� sagl�yor LayerMask ozel�g� bunu zem�ne ver�yoruz
    Vector3 velocity;
    public bool isGrounded;



    void Update()
    { 

 //Karakterim ile karakterimin alt�ndak� mesafede bir layer var m� yok mu onun kontrolunu yap�yoruz.
 //Yani ben burda Karakterime diyorumki sen�n alt�nda  ground ad�nda b�r layer varsa �a�rp�� ve orda kal    
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // karekter yerdeyse 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        controller.Move(move * speed * Time.deltaTime);
        // z�plama tu�una bast�ysan ve karakterinde yerdeyse
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


}
