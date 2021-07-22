using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace InterDigital.CMU
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Components of UI")]
        public TextMeshProUGUI SubtitleMesh;
        public SpriteRenderer Img;

        public void SetNewImage(float delay, Material newImg)
        {
            Img.material = newImg;
        }

        public void SetNewText(string text)
        {
            SubtitleMesh.text = text;
        }
    }
}