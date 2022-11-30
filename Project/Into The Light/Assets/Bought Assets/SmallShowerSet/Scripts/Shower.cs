using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour
{
    public bool On = false;
    public ParticleSystem effect;
    public AudioSource aud;
    Animator anim;
 
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (On)
                On = false;
            else
                On = true;
        }

        if (On)
        {
            anim.SetBool("ShowerOn", true);
            if (!effect.isEmitting)
            {
                effect.Play();
                aud.Play();
            }

        }
        else
        {
            anim.SetBool("ShowerOn", false);
            if (effect.isEmitting)
            {
                effect.Stop();
                aud.Stop();
            }
        }
    }
}
