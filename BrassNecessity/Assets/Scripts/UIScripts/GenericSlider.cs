using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenericSlider
{
    protected SliderData sliderData;
    protected SliderInt sliderElement;
    public int MinValue
    {
        get => sliderElement.lowValue;
    }

    public int MaxValue
    {
        get => sliderElement.highValue;
    }

    public GenericSlider(SliderData sliderData, SliderInt sliderElement)
    {
        this.sliderData = sliderData;
        this.sliderElement = sliderElement;
    }

    public int GetCurrentValue()
    {
        return sliderElement.value;
    }

    public virtual void SetValue(int newValue)
    {
        this.sliderElement.value = newValue;
    }

    public virtual void ToggleSelect()
    {
        sliderElement.ToggleInClassList("settingSliderSelected");
    }
}
