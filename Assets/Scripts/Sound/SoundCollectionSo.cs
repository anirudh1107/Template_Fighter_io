using UnityEngine;

[CreateAssetMenu(fileName = "SoundCollectionSo", menuName = "Scriptable Objects/SoundCollectionSo")]
public class SoundCollectionSo : ScriptableObject
{
    public SoundSo[] punchSounds;
    public SoundSo[] kickSounds;
    public SoundSo[] LightPunchHitSounds;
    public SoundSo[] LightKickHitSounds;
    public SoundSo[] HeavyPunchHitSounds;
    public SoundSo[] HeavyKickHitSounds;

    
}
