using Pigeons;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Eagles
{
    public class Eagle : MonoBehaviour
    {
        private const string HitAnimationParam = "IsHit";
        private const float Speed = 3.5f;
        private const float HitDuration = 0.5f;
        private const float HitSpeed = 35f;
        private const float WanderDirectionUpdateInterval = 0.25f;
        private const float WanderAngularSpeed = 4 * Mathf.PI;

        [SerializeField] private AudioClip _hitAudioClip;

        private int _health = 3;
        private float _hitTimer;
        private Pigeon _nearestPigeon;
        private Vector3 _targetWanderDirection;
        private Vector3 _wanderDirection;
        private Vector3 _prevPosition;
        private Animator _animator;

        void Start()
        {
            StartCoroutine(ChangeTargetWanderDirection());
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            _animator.SetBool(HitAnimationParam, IsHit());

            if (IsHit())
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
            transform.Find("Feathers").GetComponent<ParticleSystem>().Play();
            _hitTimer = HitDuration;
            _health--;

            if (_health <= 0)
            {
                GetComponentInChildren<BloodParticles>().PlayAndDestroy();
                Destroy(gameObject);
                EagleGenerator.Instance.Count--;
            }

            AudioSourcePool.Instance.Play(_hitAudioClip);
        }

        private void SetHorizontalFlip()
        {
            if (!IsHit())
            {
                transform.localScale = transform.position.x - _prevPosition.x > 0
                    ? new Vector3(-1, 1, 1)
                    : new Vector3(1, 1, 1);
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

        private void Wander()
        {
            _wanderDirection = Vector3.RotateTowards(_wanderDirection, _targetWanderDirection, WanderAngularSpeed * Time.deltaTime, 100.0f);
            transform.position += _wanderDirection * Speed * Time.deltaTime;
        }

        private void Flee()
        {
            transform.position -= GetDirectionToNearestPigeon() * HitSpeed * Mathf.Pow(_hitTimer, 2f) / HitDuration * Time.deltaTime;
        }

        private bool IsHit()
        {
            return _hitTimer > 0;
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
