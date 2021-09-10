using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSound : MonoBehaviour
{
    public List<AudioClip> clipsList;
    public List<AudioSource> sourcesList;

    public void Play()
    {
        if (clipsList.Count == 0 || sourcesList.Count == 0) return;
        for (int i = 0; i < sourcesList.Count; i++)
        {
            int j = i;
            if (j >= clipsList.Count) 
            {
                j = clipsList.Count - 1; 
            }

            sourcesList[i].clip = clipsList[j];
            sourcesList[i].Play();
        }
    }
}
