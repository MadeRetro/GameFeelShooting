using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float timeToRevert = 3f; // Adjust the time as needed
    [SerializeField] private float electrifyRange = 5f; // Adjust the range as needed
    private float timeSinceLastHit = 0f;

    void Update()
    {
        // If not hit for a certain duration, revert color to white
        if (timeSinceLastHit >= timeToRevert)
        {
            StartCoroutine(LerpColorToWhite(GetComponent<Renderer>(), 2f)); // Adjust the duration as needed
            timeSinceLastHit = 0f; // Reset the time since last hit
        }
        else
        {
            // Otherwise, increment the time since last hit
            timeSinceLastHit += Time.deltaTime;
        }

        // Check if the enemy is entirely blue
        if (IsEntirelyBlue(GetComponent<Renderer>().material.color))
        {
            Debug.Log("The enemy is entirely blue!");

            // Electrify nearby enemies
            ElectrifyNearbyEnemies();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Bullet" tag
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Handle bullet hit
            //ChangeColorToBlue();

            // Reset the time since last hit
            timeSinceLastHit = 0f;
        }
    }

    IEnumerator LerpColorToWhite(Renderer renderer, float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = renderer.material.color;
        Color targetColor = Color.white;

        while (elapsedTime < duration)
        {
            // Interpolate between initial and target colors
            renderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final color is set
        renderer.material.color = targetColor;
    }

    bool IsEntirelyBlue(Color color)
    {
        Color targetBlueColor = Color.blue;
        float colorDifference = Vector4.Distance(color, targetBlueColor);
        float colorThreshold = 0.1f; // Adjust as needed

        return colorDifference < colorThreshold;
    }

    void ElectrifyNearbyEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, electrifyRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.gameObject != gameObject)
            {
                EnemyScript enemyScript = collider.gameObject.GetComponent<EnemyScript>();

                // Check if the enemy is not already entirely blue
                if (enemyScript != null && !IsEntirelyBlue(enemyScript.GetComponent<Renderer>().material.color))
                {
                    StartCoroutine(LerpColorToBlue(enemyScript.GetComponent<Renderer>(), 5f));
                }
            }
        }
    }

    IEnumerator LerpColorToBlue(Renderer renderer, float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = renderer.material.color;
        Color targetColor = Color.blue;

        while (elapsedTime < duration)
        {
            // Interpolate between initial and target colors
            renderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final color is set
        renderer.material.color = targetColor;
    }
}
