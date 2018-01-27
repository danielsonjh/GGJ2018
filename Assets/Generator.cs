using UnityEngine;

public class Generator : MonoBehaviour
{

    public GameObject Bird;

    private const float SecondsBetweenBirds = 1f;

    private float _secondsUntilNextBird = 0f;


    void Update()
    {
        _secondsUntilNextBird -= Time.deltaTime;

        if (_secondsUntilNextBird < 0)
        {
            var bird = Instantiate(Bird);
            _secondsUntilNextBird = SecondsBetweenBirds;
        }
    }
}
