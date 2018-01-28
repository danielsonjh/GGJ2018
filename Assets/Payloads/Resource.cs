using Payloads;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public static Resource Instance { get; private set; }

    private const float MaxValue = 100;

    [SerializeField] private Text _scoreText;

    private float _totalEarned;
    private float _value = MaxValue;
    private RectTransform _rectTransform;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        _rectTransform.offsetMax = new Vector2(_rectTransform.offsetMax.x, -1080 * (MaxValue - _value) / MaxValue);
        _scoreText.text = _totalEarned.ToString();
    }

    public void DeliverPayload(IPayload payload)
    {
        _totalEarned += payload.Value;
        _value = Mathf.Clamp(_value + payload.Value, 0, MaxValue);
    }

    public void LosePayload(IPayload payload)
    {
        _value -= payload.Value;

        if (_value < 0)
        {
            GameManager.Instance.EndGame();
            Time.timeScale = 0;
        }
    }
}