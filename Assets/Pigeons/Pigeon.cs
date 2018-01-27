using UnityEngine;

namespace Pigeons
{
    public class Pigeon : MonoBehaviour
    {

        private const float MaxSpeed = 4f;
        private const float MinSpeed = 2.5f;

        private const float MaxGravityScale = 1.5f;
        private const float MinGravityScale = 0.5f;

        private const float ClickForce = 9f;

        private const float LinearDrag = 5f;


        private static readonly Vector2 StartPosition = new Vector2(-8f, 0f);

        private float _speed;

        void Start()
        {
            _speed = Random.Range(MinSpeed, MaxSpeed);
            GetComponent<Rigidbody2D>().gravityScale = Random.Range(MinGravityScale, MaxGravityScale);
            GetComponent<Rigidbody2D>().drag = LinearDrag;
            transform.position = StartPosition;
        }

        void Update()
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;

            if (transform.position.x > StartPosition.x * -1)
            {
                FinishJourney();
            }
        }

        void OnMouseUp()
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * ClickForce, ForceMode2D.Impulse);
        }

        private void FinishJourney()
        {
            Stats.Instance.Score++;
            Destroy(gameObject);
        }
    }
}
