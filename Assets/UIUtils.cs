using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUtils : MonoBehaviour {
    [SerializeField]  private Slider SliderX, SliderY;
    [SerializeField]  private Testing testing;


   public void onSliderXChange(float x) {
        testing.changeGrid((int)x, -1);
    }
    public void onSliderYChange(float y) {
        testing.changeGrid(-1, (int)y);
    }
}
