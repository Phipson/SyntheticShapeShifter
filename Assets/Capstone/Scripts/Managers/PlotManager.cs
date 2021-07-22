using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace InterDigital.CMU
{
    [System.Serializable]
    public class SceneStartEvent : UnityEvent { }

    [System.Serializable]
    public struct PlotBranch
    {
        public ImageDisplay[] ImageOverlays;
        public AudioClip AudioTrack;
        public DialogueManager Subtitles;
        public SceneStartEvent OnNextScene;
    }

    [System.Serializable]
    public struct ImageDisplay
    {
        public Material ImageTxt;
        public float Delay;
    }

    public class PlotManager : MonoBehaviour
    {
        [Header("Audio and Camera Configurations")]
        public AudioSource[] sources;

        // 0 = Neutral
        // 1 = Happy
        // 2 = Horror
        // 3 = Sad
        [Header("SCENE: LRRH walking in the woods, seeing animals, etc.")]
        public PlotBranch[] Scene1;

        [Header("SCENE: LRRH encountering wolf in the woods")]
        public PlotBranch[] Scene2;

        [Header("SCENE: Wolf is in house with grandma")]
        public PlotBranch[] Scene3;

        [Header("SCENE: LRRH is in house with fake grandma")]
        public PlotBranch[] Scene4;

        [Header("SCENE: Huntsman with fake grandma in house")]
        public PlotBranch[] Scene5;

        Emotion currentPlotEmotion;
        PlotBranch[] actualPlotline = new PlotBranch[5]; // User-defined plot line based on emotions
        int currentPlotIndex = -1;

        // Start is called before the first frame update
        void Start()
        {
            // Test
            //actualPlotline[0] = Scene1[0];
            //actualPlotline[0] = Scene5[0];
            //StartCoroutine(playAudioSequentially());
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                SetPlot(0);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                SetPlot(1);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                SetPlot(2);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                SetPlot(3);
            }
        }

        void SetPlot(int index)
        {
            switch (currentPlotIndex)
            {
                case -1:
                    actualPlotline[0] = Scene1[index];
                    actualPlotline[1] = Scene2[index];
                    actualPlotline[2] = Scene3[index];
                    actualPlotline[3] = Scene4[index];
                    actualPlotline[4] = Scene5[index];
                    StartCoroutine(playAudioSequentially());
                    currentPlotIndex++;
                    break;

                case 0:
                    actualPlotline[1] = Scene2[index];
                    break;

                case 1:
                    actualPlotline[2] = Scene3[index];
                    break;

                case 2:
                    actualPlotline[3] = Scene4[index];
                    break;

                case 3:
                    actualPlotline[4] = Scene5[index];
                    break;
            }
        }


        IEnumerator playAudioSequentially()
        {
            yield return null;

            for (int i = 0; i < actualPlotline.Length; i++, currentPlotIndex++)
            {
                if (actualPlotline[i].AudioTrack != null)
                {
                    foreach (AudioSource s in sources)
                    {
                        s.clip = actualPlotline[i].AudioTrack;
                        s.Play();
                    }

                    actualPlotline[i].Subtitles.PlaySubtitles();

                    while (sources[0].isPlaying)
                    {
                        yield return null;
                    }

                    actualPlotline[i].OnNextScene?.Invoke();

                    yield return new WaitForSeconds(2);

                }
            }
        }
    }
}