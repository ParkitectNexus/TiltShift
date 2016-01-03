using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TiltShift
{
    public class Main : IMod, IModSettings
    {
        private GameObject _go;

        private TiltShiftAttacher _tilt;

        public void onEnabled()
        {
            _go = new GameObject("Tilt shift attached");

            _tilt = _go.AddComponent<TiltShiftAttacher>();

            _tilt.Path = Path;

            _tilt.Attach();
        }

        public void onDisabled()
        {
            Object.Destroy(_go.GetComponent<TiltShiftAttacher>());
        }

        public string Path { get; set; }
        public string Name { get { return "TiltShift"; } }
        public string Description { get { return "Adds a tilt shift effect to the camera"; } }
        public string Identifier { get; set; }

        public void onDrawSettingsUI()
        {
            if (GUILayout.Button("Defaults"))
            {
                SetDefaults();
            }
            
            int sliderWidth = 150;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Blur Area");
            _tilt.TiltShift.blurArea = GUILayout.HorizontalSlider(_tilt.TiltShift.blurArea, 0, 15, GUILayout.Width(sliderWidth));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Max Blur Size");
            _tilt.TiltShift.maxBlurSize = GUILayout.HorizontalSlider(_tilt.TiltShift.maxBlurSize, 0, 25, GUILayout.Width(sliderWidth));
            GUILayout.EndHorizontal();

            _tilt.TiltShift.downsample = Convert.ToInt32(GUILayout.Toggle(Convert.ToBoolean(_tilt.TiltShift.downsample), "Downsample"));
        }

        private void SetDefaults()
        {
            _tilt.TiltShift.blurArea = 2;
            _tilt.TiltShift.maxBlurSize = 5;
            _tilt.TiltShift.downsample = 0;
        }

        public void onSettingsOpened()
        {

        }

        public void onSettingsClosed()
        {
            IniFile ini = new IniFile(Path + @"/settings.ini");

            ini.IniWriteValue("General", "blur_area", _tilt.TiltShift.blurArea.ToString());
            ini.IniWriteValue("General", "max_blur_size", _tilt.TiltShift.maxBlurSize.ToString());
            ini.IniWriteValue("General", "downsample", _tilt.TiltShift.downsample.ToString());
        }
    }
}
