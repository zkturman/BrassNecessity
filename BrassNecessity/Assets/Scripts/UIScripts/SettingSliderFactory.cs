using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingSliderFactory
{
    private SettingSliderFactory() { }
    public static GenericSlider CreateSlider(SliderData sliderData, SliderInt sliderElement)
    {
        SliderType type = sliderData.Type;
        GenericSlider sliderToCreate;
        switch (type)
        {
            case SliderType.Sensitivity:
                sliderToCreate = new SensitivitySlider(sliderData, sliderElement);
                break;
            case SliderType.Brightness:
                sliderToCreate = new BrightnessSlider(sliderData, sliderElement);
                break;
            case SliderType.MusicVolume:
                sliderToCreate = new MusicVolumeSlider(sliderData, sliderElement);
                break;
            case SliderType.EffectsVolume:
                sliderToCreate = new EffectsVolumeSlider(sliderData, sliderElement);
                break;
            default:
                sliderToCreate = new GenericSlider(sliderData, sliderElement);
                break;
        }
        return sliderToCreate;
    }
}
