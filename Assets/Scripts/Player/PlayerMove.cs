using UnityEngine;
using Zenject;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float rotationSmoothTime = 0.1f;

    private InputManager inputManager;
    private Rigidbody rb;
    private Vector3 direction;
    private float rotationVelocity;
    private bool isMove;

    [Inject]
    public void Consturct(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    private void Start()
    {
        inputManager.MovePerformed += OnMovePerformed;
        inputManager.IsMove += SetMoveBool;

        rb = GetComponent<Rigidbody>();
    }

    private void OnMovePerformed(Vector2 dir)
    {
        direction = new Vector3(dir.x, 0, dir.y);      
    }

    private void SetMoveBool(bool value)
    {
        isMove = value;
    }

    public bool IsMove()
    {
        return isMove;
    }

    public void LookAtTarget(Vector3 targetPosition)
    {
        var direction = targetPosition - transform.position;
        var targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetRotation, 0);
    }

    public void Look()
    {
        if (direction != Vector3.zero)
        {
            var targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public void Move(float speed)
    {
        rb.velocity = speed * direction;
    }

    private void OnDestroy()
    {
        inputManager.MovePerformed -= OnMovePerformed;
        inputManager.IsMove -= SetMoveBool;
    }
}
