using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound", menuName = "ScriptableObjects/Sound", order = 0)]
public class Sound : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public AudioClip song;
    public bool playOnAwake;
    public bool loop;
    public AudioMixerGroup mixerGroup;
    public AudioSource source;
    public MusicLevel levelSound;
    public MusicType musicType;
}
