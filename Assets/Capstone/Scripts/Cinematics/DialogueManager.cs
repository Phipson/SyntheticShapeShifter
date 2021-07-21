using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace InterDigital.CMU
{
    [System.Serializable]
    public struct SubtitleDisplay
    {
        [TextArea(5, 20)]
        public string Subtitle;
        public float Delay;
        public OnEnableAnimation onEnableAnim;
    }

    public class DialogueManager : MonoBehaviour
    {
        public SubtitleDisplay[] subtitles;
        public void PlaySubtitles()
        {
            StartCoroutine(DisplaySubtitles());
        }

        IEnumerator DisplaySubtitles()
        {
            foreach (SubtitleDisplay sd in subtitles)
            {
                yield return new WaitForSeconds(sd.Delay);

                UIManager.Instance.SetNewText(sd.Subtitle);
                for (int i = 0; i < sd.onEnableAnim.GetPersistentEventCount(); i++)
                    Debug.Log(sd.onEnableAnim.GetPersistentMethodName(i));
                sd.onEnableAnim?.Invoke();
            }
        }
    }
}
