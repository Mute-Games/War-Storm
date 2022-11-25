using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffMusic : MonoBehaviour
{
    AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        if (!GameData.Instance.MusicOn) music.Stop();
    }
}
