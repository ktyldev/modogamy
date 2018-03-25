using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    public string soundName;
    public GameObject soundObject;
}

public class SFXManager : AudioManager
{
    public List<SoundEffect> sounds;
    protected override string _VolumeKey
    {
        get
        {
            return GameTags.SFXVolume;
        }
    }

    public void PlaySound(GameObject sound)
    {
        var audioObj = Instantiate(sound, transform);
        var audio = audioObj.GetComponent<AudioSource>();

        audio.volume *= Volume;
        audio.Play();

        Destroy(audioObj, 1f);
    }

    public void PlaySound(string soundName)
    {
        var sound = sounds.SingleOrDefault(o => o.soundName == soundName);

        if (sound == null)
        {
            print("tried to play sound \'" + soundName + "\', no such sound exists");
            return;
        }

        if (sound.soundObject == null)
            return;

        PlaySound(sound.soundObject);
    }

    public void PlayRandomSound(string soundNameBase, int max)
    {
        var ಠ_ಠ = string.Format("{0}{1:00}", soundNameBase, Random.Range(1, max + 1));
        PlaySound(ಠ_ಠ);
    }

    public void PlaySoundDelayed(string soundName, float delay)
    {
        StartCoroutine(PlaySoundDelayedCoroutine(soundName, delay));
    }

    public void PlayRandomSoundDelayed(string soundNameBase, int max, float delay)
    {
        var ಠ_ಠ = string.Format("{0}{1:00}", soundNameBase, Random.Range(1, max + 1));
        StartCoroutine(PlaySoundDelayedCoroutine(ಠ_ಠ, delay));
    }

    private IEnumerator PlaySoundDelayedCoroutine(string soundName, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound(soundName);
    }
}