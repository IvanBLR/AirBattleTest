using System;
using MoreMountains.Feedbacks;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Bullet : MonoBehaviour
{
    public Action<Bullet> Hit;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _firepoint;

    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        var direction = _firepoint - transform.position;
        _rigidbody.velocity = direction.normalized * _speed;
    }

    private void OnTriggerEnter(Collider other) => Hit?.Invoke(this);

    public void UpdateFirePointCoordinate(Vector3 point) => _firepoint = point;
}