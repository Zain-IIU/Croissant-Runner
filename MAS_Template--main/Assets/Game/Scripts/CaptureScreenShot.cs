#if UNITY_EDITOR

using System.IO;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

namespace UnityEngine.Recorder.Examples
{
    /// <summary>
    /// This example shows how to set up a recording session via script.
    /// To use this example, add the CaptureScreenShotExample component to a GameObject.
    ///
    /// Entering playmode will display a "Capture ScreenShot" button.
    ///
    /// Recorded images are saved in [Project Folder]/SampleRecordings
    /// </summary>
    public class CaptureScreenShot : MonoBehaviour
    {
        RecorderController m_RecorderController;

        [SerializeField] Vector2 output;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_RecorderController.PrepareRecording();
                m_RecorderController.StartRecording();
            }
        }

        [ContextMenu("SetRes")]
        void SetRes()
        {
            output.x = UnityEditor.Handles.GetMainGameViewSize().x;
            output.y = UnityEditor.Handles.GetMainGameViewSize().y;
        }

        void OnEnable()
        {
            var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
            m_RecorderController = new RecorderController(controllerSettings);

            var mediaOutputFolder = Path.Combine(Application.dataPath, "..", "SampleRecordings");

            // Image
            var imageRecorder = ScriptableObject.CreateInstance<ImageRecorderSettings>();
            imageRecorder.name = "My Image Recorder";
            imageRecorder.Enabled = true;
            imageRecorder.OutputFormat = ImageRecorderSettings.ImageRecorderOutputFormat.PNG;
            imageRecorder.CaptureAlpha = false;

            imageRecorder.OutputFile = Path.Combine(mediaOutputFolder, "image_") + DefaultWildcard.Take;

            imageRecorder.imageInputSettings = new GameViewInputSettings
            {
                OutputWidth = (int)output.x,
                OutputHeight = (int)output.y,
            };

            // Setup Recording
            controllerSettings.AddRecorderSettings(imageRecorder);
            controllerSettings.SetRecordModeToSingleFrame(0);
        }

        void OnGUI()
        {
           // if (GUI.Button(new Rect(10, 10, 150, 50), "Capture ScreenShot"))
            {
               
            }
        }
    }
}

#endif
