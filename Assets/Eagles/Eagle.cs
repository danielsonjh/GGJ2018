using Pigeons;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Eagles
{
    public class Eagle : MonoBehaviour
    {
        private const float Speed = 4f;
        private const float HitDuration = 0.35f;
        private const float HitSpeed = 50f;
        private const float WanderDirectionUpdateInterval = 0.25f;
        private const float WanderAngularSpeed = 4 * Mathf.PI;

        private int _health = 3;
        private float _hitTimer;
        private Pigeon _nearestPigeon;
        private Vector3 _targetWanderDirection;
        private Vector3 _wanderDirection;
        private Vector3 _prevPosition;

        void Start()
        {
            StartCoroutine(ChangeTargetWanderDirection());
        }

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
                if (_nearestPigeon != null)
                {
                    HuntNearestPigeon();
                }
                else
                {
                    Wander();
                }
            }

            SetHorizontalFlip();
            _prevPosition = transform.position;
        }

        void OnMouseUp()
        {
            _hitTimer = HitDuration;
            _health--;

            if (_health <= 0)
            {
                Destroy(gameObject);
                EagleGenerator.Instance.Count--;
            }
        }

        private void SetHorizontalFlip()
        {
            print(transform.position.x - _prevPosition.x);
            transform.localScale = transform.position.x - _prevPosition.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
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

        private void Wander()
        {
            _wanderDirection = Vector3.RotateTowards(_wanderDirection, _targetWanderDirection, WanderAngularSpeed * Time.deltaTime, 100.0f);
            transform.position += _wanderDirection * Speed * Time.deltaTime;
        }

        private void Flee()
        {
            transform.position -= GetDirectionToNearestPigeon() * HitSpeed * Mathf.Pow(_hitTimer, 2f) / HitDuration * Time.deltaTime;
        }

        private Vector3 GetDirectionToNearestPigeon()
        {
            return _nearestPigeon == null ? _wanderDirection : (_nearestPigeon.transform.position - transform.position).normalized;
        }

        private IEnumerator ChangeTargetWanderDirection()
        {
            while (true)
            {
                _targetWanderDirection = Random.insideUnitCircle.normalized;
                yield return new WaitForSeconds(WanderDirectionUpdateInterval);
            }
        }
    }
}
