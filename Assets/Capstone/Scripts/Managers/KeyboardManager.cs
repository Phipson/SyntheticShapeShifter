using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InterDigital.CMU
{
    public class KeyboardManager : Singleton<KeyboardManager>
    {
        // Update is called once per frame
        void Update()
        {
            // FOR POST PROCESSING AND COLOR GRADING
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                PostProcessingManager.Instance.SetEmotion(Emotion.Neutral);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PostProcessingManager.Instance.SetEmotion(Emotion.Happy);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PostProcessingManager.Instance.SetEmotion(Emotion.Scared);
            }

            // FOR MUSIC
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MusicManager.Instance.TransitionNewAudio(Emotion.Neutral);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                MusicManager.Instance.TransitionNewAudio(Emotion.Happy);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                MusicManager.Instance.TransitionNewAudio(Emotion.Scared);
            }
        }
    }
}
