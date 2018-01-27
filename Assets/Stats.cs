using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public static Stats Instance { get; private set; }

    public int Score;

    [SerializeField] private Text _scoreText;


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        _scoreText.text = Score.ToString();
    }
}
