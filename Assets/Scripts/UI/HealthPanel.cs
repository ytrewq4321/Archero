using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, cameraTransform.position.y, cameraTransform.position.z));
        //transform.Rotate(0, 180, 0);
    }

    public void SetupHealth(float maxValue)
    {
        healthSlider.maxValue = maxValue;
        UpdateHealth(maxValue);
    }

    public void UpdateHealth(float health)
    {
        healthSlider.value = health;
    }
}
