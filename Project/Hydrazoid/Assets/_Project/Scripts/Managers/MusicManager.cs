using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hydrazoid
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _musicList = new List<AudioClip>();

        private float _musicVolume = 0.75f;

        private AudioSource _audioSource;

        private void OnEnable()
        {
            LoadVolume();
        }

        private void OnDisable()
        {
            SaveVolume();
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartMusic();
        }

        private void Update()
        {
            if (!_audioSource.isPlaying)
            {
                StartMusic();
            }
        }

        private void StartMusic()
        {
            _audioSource.clip = _musicList[Random.Range(0, _musicList.Count)];
            _audioSource.Play();
        }

        public void SaveVolume()
        {
            PlayerPrefs.SetFloat("music_volume", _musicVolume);
        }

        public void LoadVolume()
        {
            _musicVolume = PlayerPrefs.GetFloat("music_volume");
            _audioSource.volume = _musicVolume;
        }
    }
}

