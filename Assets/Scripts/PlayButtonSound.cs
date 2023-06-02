using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource buttonAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSound() {
        buttonAudio.Play();
    }
}
