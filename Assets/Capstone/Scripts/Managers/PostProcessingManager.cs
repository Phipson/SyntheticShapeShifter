using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

namespace InterDigital.CMU
{
    public enum Emotion
    {
        Neutral,
        Happy,
        Sad,
        Angry,
        Scared,
    }

    // TODO: Add audio configurations and other stylistic changes (e.g. VFX, Letterbox zoom)
    [System.Serializable]
    public struct EmotionConfig
    {
        public Emotion emotion;
        public PostProcessVolume volume;
        public AudioClip[] audio; 
    }

    public class PostProcessingManager : Singleton<PostProcessingManager>
    {
        [Header("Emotion Volume Configurations")]
        public EmotionConfig[] Config;

        [Header("Scene Change Configurations")]
        public float FadeDuration = 0.15f;

        Dictionary<Emotion, EmotionConfig> configMap = new Dictionary<Emotion, EmotionConfig>();
        EmotionConfig masterConfig;
        PostProcessVolume currentVolume, targetVolume;

        // Start is called before the first frame update
        void Awake()
        {
            foreach (EmotionConfig c in Config)
            {
                if (!configMap.ContainsKey(c.emotion))
                    configMap.Add(c.emotion, c);
            }

            SetEmotion(Emotion.Neutral);
        }

        public void SetEmotion(Emotion emotion)
        {
            masterConfig = configMap[emotion];

            targetVolume = masterConfig.volume;
            DOTween.To(() => volumeWeightLerp, x => volumeWeightLerp = x, 1f, 1f).OnUpdate(LerpVolumeConfig).OnComplete(() =>
            {
                currentVolume = targetVolume;
                volumeWeightLerp = 0f;
            });
        }

        public EmotionConfig FetchConfig(Emotion emotion)
        {
            return configMap[emotion];
        }

        float volumeWeightLerp = 0f;
        void LerpVolumeConfig()
        {
            if (targetVolume != null)
                targetVolume.weight = volumeWeightLerp;

            if (currentVolume != null)
                currentVolume.weight = 1 - volumeWeightLerp;
        }

        ColorGrading cg;
        float fadeLerp = 0f;
        float maxFade = -35f;
        void Fade()
        {
            masterConfig.volume.profile.TryGetSettings(out cg);
            cg.postExposure.value = fadeLerp * maxFade;
        }

        public void FadeToBlack(TweenCallback completedFade)
        {
            DOTween.To(() => fadeLerp, x => fadeLerp = x, 1f, FadeDuration).OnUpdate(Fade).OnComplete(() =>
            {
                completedFade?.Invoke();
                DOTween.To(() => fadeLerp, x => fadeLerp = x, 0f, FadeDuration).OnUpdate(Fade);
            });
        }
    }
}
