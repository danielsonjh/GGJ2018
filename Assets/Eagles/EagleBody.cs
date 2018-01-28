using Pigeons;
using UnityEngine;

namespace Eagles
{
    public class EagleBody : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Pigeon"))
            {
                other.GetComponentInParent<Pigeon>().Kill();
            }
        }
    }
}
