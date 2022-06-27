using HSVPicker;
using UnityEngine;
using TMPro;
namespace HSVPickerExamples
{
    public class ColorPickerTester : MonoBehaviour 
    {

        public TextMeshProUGUI colorText;
        public Color Color = Color.red;

        private new Renderer renderer;
        public ColorPicker picker;

        public bool SetColorOnStart = false;

	    // Use this for initialization
	    void Start () 
        {
            picker.onValueChanged.AddListener(color =>
            {
                //renderer.material.color = color;
                colorText.color = color;
                Color = color;
            });

            colorText.color = picker.CurrentColor;
            //renderer.material.color = picker.CurrentColor;
            if (SetColorOnStart) 
            {
                picker.CurrentColor = Color;
            }
        }
	
	    // Update is called once per frame
	    void Update () {
	
	    }
    }
}