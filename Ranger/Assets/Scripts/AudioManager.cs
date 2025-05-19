using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private AudioClip[] _audioClip;

    [SerializeField]
    private AudioClip _dayAudio;

    private float _dayTime = 12f;
    private float _dayTimeCounter;

    private bool _isDay;
    private void Start()
    {
        _audioSource = _player.GetComponent<AudioSource>();
    }
    private void Update()
    {
        _dayTimeCounter *= Time.deltaTime;

        if(_dayTimeCounter >= _dayTime)
        {
            _isDay = true;
        }
       
        if (_isDay == false)
        {
           // _audioSource.PlayOneShot(_audioClip[0]);
           // _audioClip.
        }
        else
        {
           // _audioSource.PlayOneShot(_audioClip[1]);
        }
    }
}
