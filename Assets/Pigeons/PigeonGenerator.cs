using UnityEngine;

namespace Pigeons
{
    public class PigeonGenerator : MonoBehaviour
    {
        public GameObject Pigeon;

        private const float SecondsBetweenBirds = 1.25f;

        private float _secondsUntilNextBird = 0f;


        void Update()
        {
            _secondsUntilNextBird -= Time.deltaTime;

            if (_secondsUntilNextBird < 0)
            {
                Instantiate(Pigeon);
                _secondsUntilNextBird = SecondsBetweenBirds;
            }
        }
    }
}
