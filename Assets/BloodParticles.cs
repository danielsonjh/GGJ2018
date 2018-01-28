using System.Collections;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    public void PlayAndDestroy()
    {
        var deathPosition = transform.parent.position;
        transform.SetParent(null);
        transform.position = deathPosition;
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyAfterDonePlaying());
    }

    private IEnumerator DestroyAfterDonePlaying()
    {
        while (GetComponent<ParticleSystem>().isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
