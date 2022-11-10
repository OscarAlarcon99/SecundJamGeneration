using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Controller Sound")]
    public AudioMixer audioMixer;
    public float currentVolumen;
    public List<Sound> songs = new List<Sound>();
    
    [Header("Provicional Stats Sound Controller")]
    List<Sound> provicionalLevelSounds = new List<Sound>();
    List<AudioSource> pauseProvicionalSounds = new List<AudioSource>();

    /// <summary>
    /// Funcion que crea todas las instancias de sonidos por tipo de nivel
    /// /// </summary>
    public void CreateSoundsLevel(MusicLevel musicLevel)
    {
        foreach (Sound song in songs)
        {
            if (song.levelSound == musicLevel)
            {
                song.source = gameObject.AddComponent<AudioSource>();
                song.source.clip = song.song;
                song.source.volume = song.volume;
                song.source.pitch = song.pitch;
                song.source.playOnAwake = song.playOnAwake;
                song.source.loop = song.loop;
                song.source.outputAudioMixerGroup = song.mixerGroup;
                provicionalLevelSounds.Add(song);
            }
        }
    }
    /// <summary>
    /// Funcion que elimina todas las instancias de sonidos
    /// /// </summary>
    public void DeleteSoundsLevel()
    {
        foreach (Sound s in provicionalLevelSounds)
        {
            Destroy(s.source);
        }

        provicionalLevelSounds.Clear();
    }
    /// <summary>
    /// Funcion que envia volumen general
    /// /// </summary>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(Tags.VOLUMENMASTER_TAG, volume);
        audioMixer.GetFloat(Tags.VOLUMENMASTER_TAG, out currentVolumen);
    }
    /// <summary>
    /// Funcion que reproduce sonido determinado 
    /// /// </summary>
    public void PlayNewSound(string name)
    {
        Sound s = songs.Find(sounds => sounds.name == name);

        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogError("No Existe Track" + name);
        }
    }
    /// <summary>
    /// Funcion que pausa o despausa todos los sonidos que estan en reporoduccion
    /// /// </summary>
    public void PauseAllSounds(bool validation)
    {
        if (validation)
        {
            foreach (Sound s in provicionalLevelSounds)
            {
                if (s.source.isPlaying)
                {
                    pauseProvicionalSounds.Add(s.source);
                    s.source.Pause();
                }
            }
        }
        else
        {
            for (int i = 0; i < pauseProvicionalSounds.Count; i++)
            {
                pauseProvicionalSounds[i].Play();
            }

            pauseProvicionalSounds.Clear();
        }
    }
    /// <summary>
    /// Funcion que finaliza sonido determinado. 
    /// </summary>
    public void EndSound(string name)
    {
        Sound s = songs.Find(sounds => sounds.name == name);

        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogError("Track No Found" + name);
        }
    }
}


