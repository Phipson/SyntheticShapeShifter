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
        }
    }
}
