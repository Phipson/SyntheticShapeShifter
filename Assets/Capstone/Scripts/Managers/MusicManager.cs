using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace InterDigital.CMU
{
    public class MusicManager : Singleton<MusicManager>
    {
        public float transitionDuration = 1f;
        AudioSource source;

        // Start is called before the first frame update
        void Start()
        {
            source = GetComponent<AudioSource>();
            TransitionNewAudio(Emotion.Neutral);
        }

        public void TransitionNewAudio(Emotion emotion)
        {
            DOTween.To(() => volume, x => volume = x, 0f, transitionDuration).OnUpdate(SetVolume).OnComplete(() =>
            {
                AudioClip[] arr = PostProcessingManager.Instance.FetchConfig(emotion).audio;
                source.clip = arr[Random.Range(0, arr.Length - 1)];
                source.Play();

                DOTween.To(() => volume, x => volume = x, 0.1f, transitionDuration).OnUpdate(SetVolume);
            });
            
        }

        float volume = 0f;
        void SetVolume()
        {
            source.volume = volume;
        }
    }
}
