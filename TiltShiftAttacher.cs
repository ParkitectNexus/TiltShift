using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace TiltShift
{
    class TiltShiftAttacher : MonoBehaviour
    {
        public string Path;

        public TiltShift TiltShift;

        public void Attach()
        {
            StartCoroutine(SetTiltShift());
        }

        private IEnumerator SetTiltShift()
        {
            using (WWW www = new WWW(@"file://" + System.IO.Path.Combine(System.IO.Path.Combine(Path, "ab"),"shader")))
            {
                yield return www;

                if (www.error != null)
                    throw new Exception("Loading had an error:" + www.error);

                AssetBundle bundle = www.assetBundle;

                Shader s = bundle.LoadAsset<Shader>("TiltShiftHdrLensBlur");

                TiltShift = Camera.main.gameObject.AddComponent<TiltShift>();

                TiltShift.tiltShiftShader = s;
                TiltShift.quality = TiltShift.TiltShiftQuality.High;

                // check if ini exists, if it does load the settings from it, otherwise set defaults
                if (File.Exists(Path + @"/settings.ini"))
                {
                    IniFile ini = new IniFile(Path + @"/settings.ini");

                    TiltShift.blurArea = float.Parse(ini.IniReadValue("General", "blur_area"));
                    TiltShift.maxBlurSize = float.Parse(ini.IniReadValue("General", "max_blur_size"));
                    TiltShift.downsample = int.Parse(ini.IniReadValue("General", "downsample"));
                }
                else
                {
                    TiltShift.blurArea = 2;
                }

                bundle.Unload(false);
            }
        }

        void OnDestroy()
        {
            Destroy(Camera.main.gameObject.GetComponent<TiltShift>());
        }
    }
}
