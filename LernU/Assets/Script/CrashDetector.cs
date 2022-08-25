using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    CircleCollider2D circle;
    [SerializeField] float ReloadSceneInspector = 1f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip audioClip;
    bool crahs = false;
    void Start()
    {
        
        circle = GetComponent<CircleCollider2D>();    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && circle.IsTouching(other.collider) && !crahs) {
            FindObjectOfType<Drave>().DesibaleMet();
            GetComponent<AudioSource>().PlayOneShot(audioClip);
                crashEffect.Play();
                crahs = true;
           
            
            Invoke("ReloadScene", ReloadSceneInspector);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
