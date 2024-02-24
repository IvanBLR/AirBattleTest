using JetBrains.Annotations;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _strike;
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private AudioClip _gaveDemage;

    private void Start()
    {
        _enemyController.Die += PlayDamageClip;
    }

    private void OnDestroy()
    {
        _enemyController.Die -= PlayDamageClip;
    }

    [UsedImplicitly] // назначен на кнопку выстрела
    public void Strike() => _audioSource.PlayOneShot(_strike, 0.11f);

    public void Explosion() => _audioSource.PlayOneShot(_explosion, 0.35f);

    private void PlayDamageClip() => _audioSource.PlayOneShot(_gaveDemage);
}