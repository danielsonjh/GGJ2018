using Pigeons;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pigeon"))
        {
            other.GetComponentInParent<Pigeon>().Kill();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}