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
    public LayerMask groundMask;// bu bizim belli yerlerde çarpýþma kontrolunu kontrol
                          // etmemýzý saglýyor LayerMask ozelýgý bunu zemýne verýyoruz
    Vector3 velocity;
    public bool isGrounded;



    void Update()
    { 

 //Karakterim ile karakterimin altýndaký mesafede bir layer var mý yok mu onun kontrolunu yapýyoruz.
 //Yani ben burda Karakterime diyorumki senýn altýnda  ground adýnda býr layer varsa çaýrpýþ ve orda kal    
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
        // zýplama tuþuna bastýysan ve karakterinde yerdeyse
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


}
