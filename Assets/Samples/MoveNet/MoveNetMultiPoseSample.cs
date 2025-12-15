using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TensorFlowLite.MoveNet;
using TMPro;

[RequireComponent(typeof(VirtualTextureSource))]
public class MoveNetMultiPoseSample : MonoBehaviour
{
    [Header("UI Display")]
    [SerializeField] private TMP_Text poseStatusText;

    // ตัวแปรกันแกว่ง (Debounce)
    private string lastConfirmedPose = "";
    private string currentDetectingPose = "";
    private int poseFrameCount = 0;

    // *** ถ้าอยากให้เปลี่ยนท่าไวขึ้น ให้ลดเลขนี้ (เช่นเหลือ 5-10) ***
    private const int FRAMES_TO_CONFIRM = 10;

    PoseClassifier classifier;
    [SerializeField] MoveNetMultiPose.Options options = default;
    [SerializeField] private RectTransform cameraView = null;
    [SerializeField] private bool runBackground = false;
    [SerializeField, Range(0, 1)] private float threshold = 0.3f;

    private MoveNetMultiPose moveNet;
    private MoveNetPoseWithBoundingBox[] poses;
    private MoveNetDrawer drawer;
    private UniTask<bool> task;

    private void Start()
    {
        moveNet = new MoveNetMultiPose(options);
        drawer = new MoveNetDrawer(Camera.main, cameraView);
        if (TryGetComponent(out VirtualTextureSource source)) source.OnTexture.AddListener(OnTextureUpdate);
        classifier = new PoseClassifier();
    }

    private void OnDestroy()
    {
        if (TryGetComponent(out VirtualTextureSource source)) source.OnTexture.RemoveListener(OnTextureUpdate);
        moveNet?.Dispose();
        drawer?.Dispose();
        classifier?.Dispose();
    }

    private void Update()
    {
        if (poses != null)
        {
            foreach (var pose in poses)
            {
                drawer.DrawPose(pose, threshold);

                // ส่งค่า keypoints 17 จุด
                Vector2[] points = new Vector2[17];
                for (int i = 0; i < 17; i++)
                {
                    var p = pose[i];
                    points[i] = new Vector2(p.x, p.y);
                }

                // ทำนายท่า
                string rawPoseName = classifier.Predict(points);

                // --- Logic กันตัวหนังสือกระพริบ ---
                if (rawPoseName == currentDetectingPose)
                {
                    poseFrameCount++;
                }
                else
                {
                    currentDetectingPose = rawPoseName;
                    poseFrameCount = 0;
                }

                if (poseFrameCount >= FRAMES_TO_CONFIRM)
                {
                    lastConfirmedPose = currentDetectingPose;

                    if (poseStatusText != null)
                    {
                        // เปลี่ยนเป็นภาษาอังกฤษ เพื่อแก้ Warning
                        poseStatusText.text = "Current Pose: " + lastConfirmedPose;
                    }
                }
            }
        }
    }

    // ... (ส่วนท้ายคงเดิม)
    private void OnTextureUpdate(Texture texture)
    {
        if (runBackground) { if (task.Status.IsCompleted()) task = InvokeAsync(texture, destroyCancellationToken); }
        else Invoke(texture);
    }
    private void Invoke(Texture texture) { moveNet.Run(texture); poses = moveNet.GetResults(); }
    private async UniTask<bool> InvokeAsync(Texture texture, CancellationToken cancellationToken)
    {
        await moveNet.RunAsync(texture, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        poses = moveNet.GetResults();
        return true;
    }
}