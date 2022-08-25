using UnityEngine;

public class Draver_ : MonoBehaviour
{
    [SerializeField] float sSpeed = 0.2f;
    [SerializeField] float mSpeed = 0.2f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * sSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * mSpeed * Time.deltaTime;
        transform.Rotate(0,0 ,  -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
    void OnCollisionEnter2D(Collision2D collision)
        {
            mSpeed = slowSpeed;
        }  
    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Boost") {
                mSpeed = boostSpeed;
            }
        }
}

