using System.Collections;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] GameObject laserBeam;
    [SerializeField] bool isActive = true;
    [SerializeField] float laserLength = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserBeam.transform.localScale = new Vector3(laserBeam.transform.localScale.x, 0, laserBeam.transform.localScale.z);
        StartCoroutine(ActivateLaserCoroutine());
    }

    IEnumerator ActivateLaserCoroutine()
    {
        while (isActive)
        {
            laserBeam.SetActive(true);
            float duration = 1f; // Duration of the laser extension/retraction
            float elapsed = 0f;
            Vector3 startScale = laserBeam.transform.localScale;
            Vector3 targetScale = new Vector3(laserBeam.transform.localScale.x, laserLength, laserBeam.transform.localScale.z);
            // Extend the laser
            while (elapsed < duration)
            {
                laserBeam.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }
            float waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime); // Wait for random time before retracting
            // Retract the laser
            elapsed = 0f;
            startScale = laserBeam.transform.localScale;
            targetScale = new Vector3(laserBeam.transform.localScale.x, 0, laserBeam.transform.localScale.z);
            while (elapsed < duration)
            {
                laserBeam.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }
            laserBeam.SetActive(false);
            waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime); // Wait for random time before toggling again
        }
    }
}
