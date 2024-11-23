using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    
        [SerializeField] private Slider m_slider;

        private void Start()
        {
          //  m_slider.value = SoundManager.Instance.Volume;
        }

        public void OnSliderChanged(float value)
        {
           // SoundManager.Instance.Volume = value;
        }
    }

