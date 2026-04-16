using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private SoundCollectionSo soundCollection;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandomPunchSound()
    {
        PlayRandomSound(soundCollection.punchSounds);
    }

    public void PlayRandomKickSound()
    {
        PlayRandomSound(soundCollection.kickSounds);
    }

    public void PlayRandomLightPunchHitSound(){
        PlayRandomSound(soundCollection.LightPunchHitSounds);}

    public void PlayRandomLightKickHitSound(){
        PlayRandomSound(soundCollection.LightKickHitSounds);}

    public void PlayRandomHeavyPunchHitSound(){
        PlayRandomSound(soundCollection.HeavyPunchHitSounds);}

    public void PlayRandomHeavyKickHitSound(){
        PlayRandomSound(soundCollection.HeavyKickHitSounds);}

     private void PlayRandomSound(SoundSo[] soundSoArray)
    {
        if (soundSoArray == null || soundSoArray.Length == 0) return;

        int randomIndex = Random.Range(0, soundSoArray.Length);
        SoundSo selectedSound = soundSoArray[randomIndex];
        SoundToPlay(selectedSound);
    }

    private void SoundToPlay(SoundSo soundSo)
    {
        AudioClip clip = soundSo.audioClip;
        float volume = soundSo.volume;
        float pitch = soundSo.randomizePitch ?
         soundSo.pitch + Random.Range(-soundSo.pitchRange,
          soundSo.pitchRange) : soundSo.pitch;
        bool loop = soundSo.loop;
         
        PaySound(clip, volume, pitch, loop);
    }

    private void PaySound(AudioClip clip, float volume, float pitch, bool loop)
    {
        if (clip == null) return;

        GameObject soundObject = new GameObject("Sound_" + clip.name);
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();

        // Destroy the AudioSource component after the clip finishes playing
        if(!loop)
            Destroy(audioSource, clip.length / audioSource.pitch);
    }
}
