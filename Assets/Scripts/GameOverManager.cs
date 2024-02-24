using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private DeathController _deathController;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _deathController.GameOver += ActivateRestartCanvas;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ActivateRestartCanvas();
            _audioSource.mute = true;
        }
    }

    private void OnDestroy()
    {
        _deathController.GameOver -= ActivateRestartCanvas;
    }

    [UsedImplicitly] // назначен на кнопку Рестарт
    public void RestartGame()
    {
        SceneManager.LoadScene("FirstScene");
    }

    [UsedImplicitly] // назначен на крестик
    public void QiutGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ActivateRestartCanvas()
    {
        _gameOverCanvas.gameObject.SetActive(true);
        _audioSource.mute = true;
    }
}