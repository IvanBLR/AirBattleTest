using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform[] _startBulletPositions;
    [SerializeField] private Transform _bulletsParent;
    [SerializeField] private ParticleSystem _hurtPoint;

    private ObjectPool<Bullet> _objectPool;

    private void Start()
    {
        _objectPool = new ObjectPool<Bullet>(OnBulletCreate, OnTake, OnRelease);
    }

    [UsedImplicitly] // назначен на кнопку стрельбы
    public void Fire()
    {
        var bullet1 = _objectPool.Get();
        var bullet2 = _objectPool.Get();
        var bullet3 = _objectPool.Get();
        var bullet4 = _objectPool.Get();

        bullet1.transform.position = _startBulletPositions[0].position;
        bullet2.transform.position = _startBulletPositions[1].position;
        bullet3.transform.position = _startBulletPositions[2].position;
        bullet4.transform.position = _startBulletPositions[3].position;

        bullet1.Hit += OnBulletHit;
        bullet2.Hit += OnBulletHit;
        bullet3.Hit += OnBulletHit;
        bullet4.Hit += OnBulletHit;
    }

    private Bullet OnBulletCreate()
    {
        return Instantiate(_bulletPrefab, _bulletsParent);
    }

    private void OnTake(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.position = Vector3.zero;
    }

    private void OnBulletHit(Bullet bullet)
    {
        var position = bullet.transform.position;
        Instantiate(_hurtPoint, position, Quaternion.identity);

        bullet.Hit -= OnBulletHit;
        _objectPool.Release(bullet);
    }
}