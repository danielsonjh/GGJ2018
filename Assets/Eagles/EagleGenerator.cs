using UnityEngine;

namespace Eagles
{
    public class EagleGenerator : MonoBehaviour
    {
        public GameObject Eagle;

        private const float SecondsBetweenBirds = 5f;

        private float _secondsUntilNextBird = 0f;


        void Update()
        {
            _secondsUntilNextBird -= Time.deltaTime;

            if (_secondsUntilNextBird < 0)
            {
                var eagle = Instantiate(Eagle);
                eagle.transform.position = new Vector3(Random.Range(-8f, 8f), 6f);

                _secondsUntilNextBird = SecondsBetweenBirds;
            }
        }
    }
}
