using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasttral : MonoBehaviour
{
    [SerializeField] ParticleSystem draveEffect;

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            draveEffect.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            draveEffect.Play();
        }
    }
}
