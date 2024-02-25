using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;

    private MMF_AudioSource _MMF_audioSource;

    private void Awake()
    {
        _MMF_audioSource = new MMF_AudioSource();
    }

    private void Start()
    {
        _enemyController.Die += PlayDamageClip;
    }

    private void OnDestroy()
    {
        _enemyController.Die -= PlayDamageClip;
    }

    [UsedImplicitly] // назначен на кнопку выстрела
    public void Strike()
    {
        _MMF_audioSource.Play(Vector3.zero);
    }

    public void Explosion()
    {
        _MMF_audioSource.Play(Vector3.zero);
    }

    private void PlayDamageClip()
    {
        _MMF_audioSource.Play(Vector3.zero);
    }
}