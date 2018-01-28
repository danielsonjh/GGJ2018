﻿using Payloads;
using UnityEngine;

namespace Pigeons
{
    public class Pigeon : MonoBehaviour
    {
        private const float MaxSpeed = 3.5f;
        private const float MinSpeed = 2.5f;
        private const float MaxGravityScale = 5f;
        private const float MinGravityScale = 3f;
        private const float ClickForce = 30f;
        private const float LinearDrag = 7f;

        private static readonly Vector2 StartPosition = new Vector2(-8f, 3f);

        private float _speed;
        private IPayload _payload;

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

        public void SetPayload(IPayload payload)
        {
            _payload = payload;
            transform.Find("Payload").GetComponent<SpriteRenderer>().sprite = _payload.Sprite;
        }

        public void Kill()
        {
            Resource.Instance.LosePayload(_payload);
            Destroy(gameObject);
        }

        private void FinishJourney()
        {
            Resource.Instance.DeliverPayload(_payload);
            Destroy(gameObject);
        }
    }
}
