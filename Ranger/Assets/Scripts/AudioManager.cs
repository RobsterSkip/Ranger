using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _audioClip;

    [SerializeField]
    private AudioClip _dayAudio;

    private float _dayTime = 12f;
    private float _dayTimeCounter;

    private bool _isDay;
    private void Start()
    {

    }
    private void Update()
    {
       //_dayTimeCounter *= Time.deltaTime;
       //
       //if(_dayTimeCounter >= _dayTime)
       //{
       //    _isDay = true;
       //}
       //
       //if (_isDay == false)
       //{
       //    _audioSource.PlayOneShot(_audioClip[0]);
       //}
       //else
       //{
       //    _audioSource.PlayOneShot(_audioClip[1]);
       //}


        /*
         *  _audioSourceGeneral.clip = _audioClipsGeneral[0];
        _audioSourceGeneral.volume = 0.05f;
        _audioSourceGeneral.Play();
        */

        //_audioSourceGeneral.PlayOneShot(_audioClipsGeneral[5]);
    }
}
