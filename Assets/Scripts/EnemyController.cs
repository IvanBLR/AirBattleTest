using System;
using Cinemachine;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public Action Die;

    [SerializeField] private CinemachineVirtualCamera _fireCamera;
    [SerializeField] private TextMeshProUGUI _lifePoints;
    [SerializeField] private int _life;
    [SerializeField] private Vector3 _targetPoint;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private float _cameraRefreshTime;

    private bool _damageWasFixed;
    private float _timeForFireCamera;

    private void Start()
    {
        _lifePoints.text = _life.ToString();
        UpdateTargetPosition();
    }

    private void Update()
    {
        if (_damageWasFixed)
        {
            _timeForFireCamera += Time.deltaTime;
            if (_timeForFireCamera < _cameraRefreshTime)
            {
                _fireCamera.Priority = 11;
            }
            else
            {
                _fireCamera.Priority = 1;
                _timeForFireCamera = 0;
                _damageWasFixed = false;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _life--;
            _damageWasFixed = true;
            _timeForFireCamera = 0;
        }

        if (_life >= 0)
        {
            _lifePoints.text = _life.ToString();
        }

        if (_life is <= 10 and > 0)
        {
            _lifePoints.color = Color.yellow;
        }

        if (_life <= 0)
        {
            _lifePoints.color = Color.red;
            Die?.Invoke();
            Die = null;
        }
    }

    public void StartFireAnimation() => _fire.Play();

    private void UpdateTargetPosition()
    {
        int x = Random.Range(-75, -25);
        int y = Random.Range(-4, 4);
        int z = Random.Range(-30, 0);
        _targetPoint = new Vector3(x, y, z);
    }
}