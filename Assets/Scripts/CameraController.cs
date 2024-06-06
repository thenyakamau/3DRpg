using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float pitch = 2f;

    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 15f;

    [SerializeField] private float yawSpeed = 100f;


    private float currentZoom = 10f;
    private float yawInput = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = target.position - (offSet * currentZoom);
        transform.LookAt(target.position + (Vector3.up * pitch));

        transform.RotateAround(target.position, Vector3.up, yawInput);
    }
}
