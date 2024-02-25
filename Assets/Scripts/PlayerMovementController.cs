using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private FireController _fireController;
    [SerializeField] private Transform _targetFirePoint;
    [SerializeField] private float _speedRotation;
    [SerializeField] private int _xRotationEuler;
    [SerializeField] private int _zRotationEuler;

    private InputSystemActions _input;

    private Quaternion _startRotation;
    private Quaternion _targetRotation;


    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Transform _firePoint;

    private float _sphereRadius;
    private Vector2 _moveInput;

    private float _z;

    //
    private void Awake()
    {
        _sphereRadius = Vector3.Distance(transform.position, _firePoint.position);
        Debug.Log(_sphereRadius);
        //

        _startRotation = transform.rotation;
        _targetRotation = _startRotation;

        _input = new InputSystemActions();
        _input.Player.Move.performed += OnRotate;
        _input.Enable();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnRotate;
        _input.Disable();
    }

    private void Update()
    {
        // if (_input.Player.Move.IsPressed())
        // {
        //     transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _speedRotation * Time.deltaTime);
        //     var targetPoint = _targetFirePoint.TransformPoint(_targetFirePoint.localPosition);
        //     _fireController.UpdateFirePointPosition(targetPoint);
        // }
        // else
        // {
        //     StartCoroutine(ReturnToStartRotation());
        // }

        _firePoint.position = new Vector3(_moveInput.x, _moveInput.y, _z);
        transform.LookAt(_firePoint.position);
    }

    private IEnumerator ReturnToStartRotation()
    {
        while (Quaternion.Angle(transform.rotation, _startRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _startRotation, _speedRotation * Time.deltaTime);
            var targetPoint = _targetFirePoint.TransformPoint(_targetFirePoint.localPosition);
            _fireController.UpdateFirePointPosition(targetPoint);
            yield return null;
        }

        transform.rotation = _startRotation;
    }

    private void OnRotate(InputAction.CallbackContext objContext)
    {
        // float x = objContext.ReadValue<Vector2>().y * _xRotationEuler;
        // float z = objContext.ReadValue<Vector2>().x * _zRotationEuler;
        //
        // Quaternion inputRotation = Quaternion.Euler(x, 0, z);
        //
        // var targetPoint = _targetFirePoint.TransformPoint(_targetFirePoint.localPosition);
        //
        // _targetRotation = _startRotation * inputRotation;
        // _fireController.UpdateFirePointPosition(targetPoint);


        _moveInput = objContext.ReadValue<Vector2>();
        _z = Mathf.Sqrt(_sphereRadius * _sphereRadius
                        - _moveInput.x * _moveInput.x
                        - _moveInput.y * _moveInput.y);
    }
}