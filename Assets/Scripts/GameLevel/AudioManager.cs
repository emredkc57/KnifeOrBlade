using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

   
    public AudioClip coin,playerhurt,playerSeriousHurt,diamond;

    public AudioClip rockCollisionAudio,knifeCollisionAudio,coinCollisionAudio;


    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
