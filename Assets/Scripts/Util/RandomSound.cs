using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public List<AudioSource> audioSourceList;

    public List<AudioClip> audioClipList;

    private int _index = 0;

    public void PlayRandom()
    {
        if (_index >= audioSourceList.Count)
        {
            if(_index == 0)
            {
                Debug.LogError("No AudioSorce Found");
                return;
            }
            _index = 0;
        }

        audioSourceList[_index].clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSourceList[_index].Play();

        _index++;
    }
}
