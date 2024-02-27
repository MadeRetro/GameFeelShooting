using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]


public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioSource ASRef;
    [SerializeField] private GameObject bulletImpact;
    private Vector3 position;
    private bool spawnParticles;


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, position, bulletSpeed* Time.deltaTime);

        if(transform.position == position)
        {
            if(spawnParticles )
            {
                Instantiate(bulletImpact, transform.position, transform.rotation);
            }

            Invoke("DestroyBullet",0.2f);

        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void setTargetPos(Vector3 newPosition, bool newSpawnParticles)
    {
        position = newPosition;
        spawnParticles = newSpawnParticles;
    }


    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle enemy hit
            Debug.Log("Il est Bleu hein !!!");
            ChangeEnemyColor(collision.gameObject);

            
        }
    }

    void ChangeEnemyColor(GameObject enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>();

        // Check if the object has a renderer component
        if (enemyRenderer != null)
        {
            // Define the target color (in this case, blue)
            Color targetColor = Color.blue;

            // Use a Coroutine to smoothly interpolate the color change
            StartCoroutine(LerpColor(enemyRenderer, targetColor, 0.5f));


        }
    }


    IEnumerator LerpColor(Renderer renderer, Color targetColor, float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = renderer.material.color;

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
