using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float MouseX;
    private float MouseY;

    [Header("Чувствительность мыши")]
    public float sensitivtyMouse = 200f;

    public Transform Player;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * sensitivtyMouse * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * sensitivtyMouse * Time.deltaTime;

        Player.Rotate(MouseX * new Vector3(0, 1, 0));

        transform.Rotate(-MouseY * new Vector3(1, 0, 0));
    }
}
