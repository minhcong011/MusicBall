using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 50)]
    public float smoothFactor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Follow();
    }
    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
