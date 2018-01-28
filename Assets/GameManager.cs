using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _gameOverPanel;


    void Awake()
    {
        Instance = this;
    }

    public void EndGame()
    {
        _gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Scenes.Game);
    }
}