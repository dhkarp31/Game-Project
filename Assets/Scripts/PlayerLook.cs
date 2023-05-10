using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensivity = 30f;
    public float ySensivity = 30f;
    // Start is called before the first frame update
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //Kalkulasi rotasi kamera melihat ke atas dan bawah
        xRotation -= (mouseY *Time.deltaTime) * ySensivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //aplly kamera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // rotasi player untuk melihat kanan dan kiri
        transform.Rotate(Vector3.up * (mouseX *Time.deltaTime) *xSensivity);
    }
}
