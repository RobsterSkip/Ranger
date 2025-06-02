using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSourceDay;
    [SerializeField]
    private AudioSource _audioSourceNight;
    [SerializeField]
    private AudioSource _audioSourceSoundEffects;

    [SerializeField]
    private AudioClip[] _audioClip;
    [SerializeField]
    private AudioClip _audioClipDay;
    [SerializeField]
    private AudioClip _audioClipNight;

    private bool _isDayPlayed;
    private bool _isNightPlayed;
    private bool _journalCompletePlayed;
    private void Start()
    {
        _audioSourceDay.volume = 0.2f;
        _audioSourceDay.clip = _audioClipDay;
        _audioSourceNight.clip = _audioClipNight;

        _audioSourceDay.Play();
    }
    private void Update()
    {
        if (BooleanManager.IsDay && !_isDayPlayed)
        {
            _audioSourceNight.Stop();
            _audioSourceDay.volume = 0.2f;
            _audioSourceDay.Play();
            _isDayPlayed = true;
            _isNightPlayed = false;
        }

        if (BooleanManager.IsNight && !_isNightPlayed)
        {
            _audioSourceDay.Stop();
            _audioSourceNight.volume = 0.2f;
            _audioSourceNight.Play();
            _isDayPlayed = false;
            _isNightPlayed = true;
        }

        if (BooleanManager.IsJournalCompleted && !_journalCompletePlayed)
        {
            _audioSourceSoundEffects.volume = 0.2f;
            _audioSourceSoundEffects.PlayOneShot(_audioClip[0]);
            _journalCompletePlayed = true;
        }

        if (BooleanManager.IsBaitDropped)
        {

        }

        if (BooleanManager.IsItemPicked)
        {
            _audioSourceSoundEffects.volume = 0.2f;
            _audioSourceSoundEffects.PlayOneShot(_audioClip[1]);
            BooleanManager.IsItemPicked = false;
        }

        if (BooleanManager.IsItemAddedJournal && !BooleanManager.IsJournalCompleted)
        {
            _audioSourceSoundEffects.PlayOneShot(_audioClip[2]);
            BooleanManager.IsItemAddedJournal = false;
        }

        if (BooleanManager.IsLineCast)
        {

        }

        if (BooleanManager.IsFishingMinigame)
        {

        }
    }
}
