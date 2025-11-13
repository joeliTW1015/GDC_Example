using System.Collections;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    Animator animator;
    bool isDead = false;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spike") && !isDead) //避免重複觸發死亡
        {
            isDead = true;
            StartCoroutine(DieCoroutine());
        }
    }

    IEnumerator DieCoroutine()
    {
        GetComponent<PlayerMovement>().enabled = false; // Disable player movement
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Stop player movement
        animator.SetTrigger("die");
        yield return new WaitForSeconds(1.5f); // Wait for the death animation to finish
        // Reload the current scene or handle player death
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
