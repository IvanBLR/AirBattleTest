using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class DeathController : MonoBehaviour
{
    public Action GameOver;

    [SerializeField] private GameObject _explosionPoint;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private ParticleSystem _explosion;
    //[SerializeField] private FireController _fireController;
    [SerializeField] private Button _fireButton;
    [SerializeField] private SoundManager _soundManager;

    [SerializeField] private float _xSpeedRotation;
    [SerializeField] private float _ySpeedRotation;
    [SerializeField] private float _zSpeedRotation;

    private EnemyController _enemyController;
    private Coroutine _rotationCoroutine;
    private Transform _enemy;
    private Rigidbody _rigidbody;

    private Vector3 _virtualCameraPosition;

    private void Start()
    {
        _enemyController = FindObjectOfType<EnemyController>();
        _enemy = _enemyController.gameObject.transform;
        _enemyController.Die += DieAnimation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _soundManager.Explosion();
            _explosionPoint.transform.position = _enemy.position;
            _explosionPoint.SetActive(true);
            StartCoroutine(StartExploseAnimation());
            StopCoroutine(_rotationCoroutine);
            StopEnemyPhysic();
        }
    }

    private void OnDestroy()
    {
        _enemyController.Die -= DieAnimation;
    }

    private void DieAnimation()
    {
        _enemy.position = new Vector3(-500, 100, 0);// НЕ ИЗМЕНЯТЬ ЭТИ magic-numbers!!
        var x = _enemy.position.x - 50;// НЕ ИЗМЕНЯТЬ ЭТИ magic-numbers!!
        var y = _enemy.position.y - 50;// НЕ ИЗМЕНЯТЬ ЭТИ magic-numbers!!

        _virtualCameraPosition = new Vector3(x, y, 0);
        _virtualCamera.transform.position = _virtualCameraPosition;
        _virtualCamera.Priority = 100;

        _enemyController.gameObject.AddComponent<Rigidbody>();
        _rigidbody = _enemyController.gameObject.GetComponent<Rigidbody>();
        _rigidbody.mass = 40;

        _enemy.rotation = Quaternion.Euler(70, 70, -45);
        _rotationCoroutine = StartCoroutine(DrillFallin());
    }

    private void StopEnemyPhysic()
    {
        var enemyCollider = _enemy.gameObject.GetComponent<CapsuleCollider>();
        enemyCollider.isTrigger = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.mass = 1000;
        var material = gameObject.GetComponent<Renderer>().material;
        material.SetFloat("_Speed", 0f);

        Invoke("ResetGame", 4f);
    }

    private void ResetGame() => GameOver?.Invoke();

    private IEnumerator StartExploseAnimation()
    {
        _explosion.Play();
        var noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 1;
        noise.m_FrequencyGain = 20;
        yield return new WaitForSeconds(0.3f);
        noise.m_AmplitudeGain = 0;
    }

    private IEnumerator DrillFallin()
    {
        //_fireController.HideFirePoint();
        _enemyController.StartFireAnimation();
        _fireButton.interactable = false;
        while (true)
        {
            _enemy.Rotate(new Vector3(
                _xSpeedRotation * Time.deltaTime,
                _ySpeedRotation * Time.deltaTime,
                _zSpeedRotation * Time.deltaTime)
            );
            yield return null;
        }
    }
}