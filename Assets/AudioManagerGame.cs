using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerGame : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManagerGame Instance;
    public AudioSource effect;


    public void PlayEffect() 
    {
        effect.Play();
        
    }
}
