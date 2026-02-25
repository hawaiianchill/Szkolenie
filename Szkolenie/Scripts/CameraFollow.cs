using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera cam;
    public Vector3 offset = new Vector3(0, 2, -10);
    public float smoothSpeed = 5f;

    void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 desiredPosition = transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(cam.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        cam.transform.position = smoothedPosition;
    }
}