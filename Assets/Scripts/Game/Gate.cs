using System.Collections;
using UnityEngine;
using System;

public class Gate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float doorOpeningHeight;

    public event Action PassedGate;

    public void OpenGate()
    {
        StartCoroutine(DoOpen());
    }

    private IEnumerator DoOpen()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, doorOpeningHeight, transform.position.z);

        float time = 0;
        while (time < 1)
        {
            transform.position =  Vector3.Slerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PassedGate.Invoke();
    }
}
