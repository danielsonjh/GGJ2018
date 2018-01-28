using UnityEngine;

namespace Eagles
{
    public class EagleGenerator : MonoBehaviour
    {
        public static EagleGenerator Instance { get; private set; }

        public const int MaxCount = 3;

        public GameObject Eagle;
        public int Count;

        private const float SecondsBetweenBirds = 7f;

        private float _secondsUntilNextBird = SecondsBetweenBirds;


        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            _secondsUntilNextBird -= Time.deltaTime;

            if (_secondsUntilNextBird < 0 && Count < MaxCount)
            {
                var eagle = Instantiate(Eagle);
                Count++;
                eagle.transform.position = new Vector3(Random.Range(-8f, 8f), 6f);

                _secondsUntilNextBird = SecondsBetweenBirds;
            }
        }
    }
}
