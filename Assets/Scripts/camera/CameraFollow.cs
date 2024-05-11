
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 public Transform player; // Asigna el objeto del personaje desde el Inspector
    private readonly float smoothSpeed = 1f; // Controla la suavidad del seguimiento

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
