using UnityEngine;

namespace TiltShift
{
    public class Main : IMod
    {
        private GameObject _go;

        public void onEnabled()
        {
            _go = new GameObject("Tilt shift attached");

            _go.AddComponent<TiltShiftAttacher>().Path = Path;
        }

        public void onDisabled()
        {
            Object.Destroy(_go.GetComponent<TiltShiftAttacher>());
        }

        public string Path { get; set; }
        public string Name { get { return "TiltShift"; } }
        public string Description { get { return "Adds a tilt shift effect to the camera"; } }
    }
}
