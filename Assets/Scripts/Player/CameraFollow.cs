using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float offset=-10f;
    private Transform playerTransform;
    private Vector3 velocity;

    [Inject]
    public void Constructor(Player player)
    {
        playerTransform = player.transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z+offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
