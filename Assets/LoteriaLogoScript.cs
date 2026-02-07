using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoteriaLogoScript : MonoBehaviour
{
    public AudioSource _audioSoruce;
    // Start is called before the first frame update
    void Start()
    {
        _audioSoruce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrowSoundEffect()
    {
        _audioSoruce.Play();
    }
}
