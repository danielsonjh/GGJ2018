using Pigeons;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PigeonBody))
        {
            other.GetComponentInParent<Pigeon>().Kill();
        }
    }
}