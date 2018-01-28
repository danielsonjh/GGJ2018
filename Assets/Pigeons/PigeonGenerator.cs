using Payloads;
using UnityEngine;

namespace Pigeons
{
    public class PigeonGenerator : MonoBehaviour
    {
        public GameObject Pigeon;

        private const float SecondsBetweenBirds = 1f;

        private float _secondsUntilNextBird;


        void Update()
        {
            _secondsUntilNextBird -= Time.deltaTime;

            if (_secondsUntilNextBird < 0)
            {
                var pigeon = Instantiate(Pigeon);
                pigeon.GetComponent<Pigeon>().SetPayload(PayloadGenerator.Instance.Generate());
                _secondsUntilNextBird = SecondsBetweenBirds;
            }
        }
    }
}
