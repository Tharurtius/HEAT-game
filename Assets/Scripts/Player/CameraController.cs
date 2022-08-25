using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController _pCon;

    [SerializeField] private float _clampAngle = 85f;
    private float _vertRotation;
    // Start is called before the first frame update
    void Start()
    {
        _pCon = GetComponentInParent<PlayerController>();
        _vertRotation = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        _vertRotation -= Input.GetAxis("Mouse Y") * _pCon.turnSpeed;

        _vertRotation = Mathf.Clamp(_vertRotation, -_clampAngle, _clampAngle);

        transform.localRotation = Quaternion.Euler(_vertRotation, 0, 0);
    }
}
