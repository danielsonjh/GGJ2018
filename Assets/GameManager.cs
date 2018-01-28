using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _gameStartPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private AudioClip _buttonPressAudioClip;


    void Awake()
    {
        Instance = this;
        Time.timeScale = 0f;
    }

    public void EndGame()
    {
        _gameOverPanel.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        AudioSourcePool.Instance.Play(_buttonPressAudioClip);
        _gameStartPanel.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        AudioSourcePool.Instance.Play(_buttonPressAudioClip);
        SceneManager.LoadScene(Scenes.Game);
    }
}