using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float chaseSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, chaseSpeed * Time.deltaTime);
    }
}
