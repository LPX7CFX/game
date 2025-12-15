using System;
using System.IO;
using System.Linq;
using UnityEngine;
using TensorFlowLite;

public class PoseClassifier : IDisposable
{
    private Interpreter interpreter;
    private float[] inputBuffer;
    private float[] outputBuffer;
    private string[] labels;

    private const string MODEL_FILENAME = "my_pose_classifier.tflite";
    private const string LABEL_FILENAME = "labels.txt";

    public PoseClassifier()
    {
        var modelPath = Path.Combine(Application.streamingAssetsPath, MODEL_FILENAME);
        var options = new InterpreterOptions();
        options.AddGpuDelegate();

        try
        {
            byte[] modelData = File.ReadAllBytes(modelPath);
            interpreter = new Interpreter(modelData, options);
            interpreter.AllocateTensors();

            int inputCount = interpreter.GetInputTensorInfo(0).shape[1];
            inputBuffer = new float[inputCount];
            int outputCount = interpreter.GetOutputTensorInfo(0).shape[1];
            outputBuffer = new float[outputCount];

            var labelPath = Path.Combine(Application.streamingAssetsPath, LABEL_FILENAME);
            labels = File.ReadAllLines(labelPath);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load model: {e.Message}");
        }
    }

    public string Predict(Vector2[] keypoints)
    {
        if (interpreter == null) return "Error";

        // --- แก้ไข: กลับมาใช้ข้อมูลดิบ (Raw Data) ---
        // ไม่ต้อง Normalize แล้ว เพราะจะทำให้ค่าเพี้ยนจากที่เทรนมา
        for (int i = 0; i < keypoints.Length && i < 17; i++)
        {
            inputBuffer[i * 2] = keypoints[i].x;
            inputBuffer[i * 2 + 1] = keypoints[i].y;
        }

        interpreter.SetInputTensorData(0, inputBuffer);
        interpreter.Invoke();
        interpreter.GetOutputTensorData(0, outputBuffer);

        float maxScore = 0f;
        int maxIndex = -1;

        for (int i = 0; i < outputBuffer.Length; i++)
        {
            if (outputBuffer[i] > maxScore)
            {
                maxScore = outputBuffer[i];
                maxIndex = i;
            }
        }

        // ลด Threshold ความมั่นใจลงนิดหน่อยเพื่อให้จับง่ายขึ้น
        if (maxIndex >= 0 && maxIndex < labels.Length && maxScore > 0.6f)
        {
            return labels[maxIndex];
        }

        return "Unknown";
    }

    public void Dispose()
    {
        interpreter?.Dispose();
    }
}