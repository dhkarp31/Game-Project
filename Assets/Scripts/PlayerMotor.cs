using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = 9.8f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast memeriksa apakah karakter ada di atas terrain
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            // Karakter ada di terrain, menyesuaikan posisinya agar tidak tenggelam
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
    
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        moveDirection = transform.TransformDirection(moveDirection);

        if (controller.isGrounded)
        {
            // Pijakan ada di bawah karakter, atur kecepatan vertikal menjadi nol
            playerVelocity.y = 0f;
        }
        else
        {
            // Tidak ada pijakan di bawah karakter, terapkan gaya gravitasi
            playerVelocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * speed * Time.deltaTime + playerVelocity * Time.deltaTime);

        // Memeriksa pijakan di bawah karakter menggunakan raycast
        RaycastHit hit;
        if (!controller.isGrounded && Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            // Pijakan ada, atur karakter pada posisi yang tepat di atas pijakan
            transform.position = new Vector3(transform.position.x, hit.point.y + controller.skinWidth, transform.position.z);
        }
    }
}
