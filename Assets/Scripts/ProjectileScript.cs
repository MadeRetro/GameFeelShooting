using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle enemy hit
            ChangeEnemyColor(collision.gameObject);
        }
    }

    void ChangeEnemyColor(GameObject enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>();

        // Check if the object has a renderer component
        if (enemyRenderer != null)
        {
            // Change the color to blue
            enemyRenderer.material.color = Color.blue;
            Debug.Log("Il devient Bleu !");

            // Optional: You can revert the color after a certain duration or under specific conditions
            Invoke("RevertColor", 2f);



        }
    }

    void RevertColor()
    {
        Renderer enemyRenderer = GetComponent<Renderer>();

        // Check if the object has a renderer component
        if (enemyRenderer != null)
        {
            // Revert the color to the original color (assuming original color is white)
            enemyRenderer.material.color = Color.white;
            Debug.Log("Il redevient Blanc !");
        }
    }
}