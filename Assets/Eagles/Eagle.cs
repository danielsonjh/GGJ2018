using Pigeons;
using System.Linq;
using UnityEngine;

namespace Eagles
{
    public class Eagle : MonoBehaviour
    {
        private const float Speed = 4f;
        private const float HitDuration = 0.35f;
        private const float HitSpeed = 8f;

        private int _health = 3;
        private float _hitTimer;
        private Pigeon _nearestPigeon;

        void Update()
        {
            if (_hitTimer > 0)
            {
                _hitTimer -= Time.deltaTime;
                Flee();
            }
            else
            {
                FindNearestPigeon();
                Debug.Log(_nearestPigeon.transform.position);
                HuntNearestPigeon();
            }
        }

        void OnMouseUp()
        {
            _hitTimer = HitDuration;
            _health--;

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void FindNearestPigeon()
        {
            var pigeons = FindObjectsOfType<Pigeon>();
            _nearestPigeon = pigeons.FirstOrDefault();
            if (_nearestPigeon != null)
            {
                foreach (var pigeon in pigeons)
                {
                    if (Vector2.Distance(transform.position, pigeon.transform.position) <
                        Vector2.Distance(transform.position, _nearestPigeon.transform.position))
                    {
                        _nearestPigeon = pigeon;
                    }
                }
            }
        }

        private void HuntNearestPigeon()
        {
            transform.position += GetDirectionToNearestPigeon() * Speed * Time.deltaTime;
        }

        private void Flee()
        {
            transform.position -= GetDirectionToNearestPigeon() * HitSpeed * _hitTimer / HitDuration * Time.deltaTime;
        }

        private Vector3 GetDirectionToNearestPigeon()
        {
            return _nearestPigeon == null ? Vector3.zero : (_nearestPigeon.transform.position - transform.position).normalized;
        }
    }
}
