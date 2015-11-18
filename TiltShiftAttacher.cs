using System;
using System.Collections;
using UnityEngine;

namespace TiltShift
{
    class TiltShiftAttacher : MonoBehaviour
    {
        public string Path;

        void Start()
        {
            StartCoroutine(SetTiltShift());
        }

        private IEnumerator SetTiltShift()
        {
            using (WWW www = new WWW(@"file://" + Path + "/ab/shader"))
            {
                yield return www;

                if (www.error != null)
                    throw new Exception("Loading had an error:" + www.error);

                AssetBundle bundle = www.assetBundle;

                Shader s = bundle.LoadAsset<Shader>("TiltShiftHdrLensBlur");

                TiltShift ts = Camera.main.gameObject.AddComponent<TiltShift>();

                ts.tiltShiftShader = s;
                ts.quality = TiltShift.TiltShiftQuality.High;
                ts.blurArea = 2;

                bundle.Unload(false);
            }
        }

        void OnDestroy()
        {
            Destroy(Camera.main.gameObject.GetComponent<TiltShift>());
        }
    }
}
