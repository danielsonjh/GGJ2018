using Pigeons;
using UnityEngine;

namespace Eagles
{
    public class EagleBody : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PigeonBody))
            {
                other.GetComponentInParent<Pigeon>().Kill();
            }
        }
    }
}
