using UnityEngine;
using System.Collections;

public class VirtualTextureSource : TextureSource
{
    [Header("WebCam Settings")]
    public string webcamName;
    public Vector2Int requestedResolution = new Vector2Int(640, 480);
    public int requestedFPS = 30;

    private WebCamTexture webCamTexture;

    // เริ่มทำงานทันทีเมื่อเริ่มเกม
    private void Start()
    {
        Play();
    }

    private void Update()
    {
        // ตรวจสอบว่ากล้องพร้อมและมีภาพใหม่มาหรือไม่
        if (webCamTexture != null && webCamTexture.didUpdateThisFrame)
        {
            // อัปเดตขนาดภาพ
            if (width != webCamTexture.width || height != webCamTexture.height)
            {
                width = webCamTexture.width;
                height = webCamTexture.height;
                Texture = webCamTexture;
            }

            // ส่งภาพไปให้ MoveNet ประมวลผล
            OnTexture.Invoke(webCamTexture);
        }
    }

    public override void Play()
    {
        if (webCamTexture == null)
        {
            // สร้าง WebCamTexture ตามชื่อกล้อง (ถ้าว่างจะใช้กล้องแรกที่เจอ)
            webCamTexture = new WebCamTexture(webcamName, requestedResolution.x, requestedResolution.y, requestedFPS);
        }

        if (!webCamTexture.isPlaying)
        {
            webCamTexture.Play();
        }

        Texture = webCamTexture;
    }

    public override void Stop()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }

    public override void Pause()
    {
        if (webCamTexture != null)
        {
            webCamTexture.Pause();
        }
    }

    public override void Resume()
    {
        if (webCamTexture != null)
        {
            webCamTexture.Play();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            Destroy(webCamTexture);
        }
    }
}