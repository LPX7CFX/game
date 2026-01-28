using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CanvasBGM
{
    public Canvas canvas;
    public AudioClip clip;
    [HideInInspector] public bool prevActive = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;   // Loop = true
    public AudioSource sfxSource;

    [Header("Canvas -> BGM pairs (order = priority)")]
    public List<CanvasBGM> canvasBgms = new List<CanvasBGM>();

    [Header("SFX")]

    public AudioClip buttonClick;

    [Header("Typing SFX")]
    public AudioClip typingSFX;
    public AudioClip finishWordSFX;

    public void PlayTyping()
    {
        if (sfxSource == null || typingSFX == null) return;
        sfxSource.PlayOneShot(typingSFX);
    }

    public void PlayFinishWord()
    {
        if (sfxSource == null || finishWordSFX == null) return;
        sfxSource.PlayOneShot(finishWordSFX);
    }


    [Header("UI Sliders")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        SetupSliders();
    }

    void SetupSliders()
    {
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            SetBGMVolume(bgmSlider.value);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            SetSFXVolume(sfxSlider.value);
        }
    }

    public void SetBGMVolume(float value)
    {
        if (bgmSource != null)
            bgmSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (bgmSource != null) bgmSource.loop = true;
    }

    void Start()
    {
        for (int i = 0; i < canvasBgms.Count; i++)
        {
            canvasBgms[i].prevActive = IsCanvasActive(canvasBgms[i].canvas); //เปลี่ยนสถานะ prevActive ของแต่ละ canvasBGM เป็นสถานะตาม .canvas
        }

        // play initial BGM: if multiple active, choose last in list (higher index has higher priority)
        for (int i = canvasBgms.Count - 1; i >= 0; i--)
        {
            if (IsCanvasActive(canvasBgms[i].canvas))
            {
                PlayBGMClip(canvasBgms[i].clip);
                break;
            }
        }
    }

    void Update()
    {
        bool anyActiveNow = false;

        // detect transitions; last transition in list wins (priority)
        for (int i = 0; i < canvasBgms.Count; i++)
        {
            var item = canvasBgms[i];
            bool cur = IsCanvasActive(item.canvas);

            // transition off->on
            if (!item.prevActive && cur)
            {
                PlayBGMClip(item.clip);
            }

            if (cur) anyActiveNow = true;
            item.prevActive = cur;
        }

        // if none active, stop BGM
        if (!anyActiveNow && bgmSource != null && bgmSource.isPlaying)
        {
            StopBGM();
        }
    }

    bool IsCanvasActive(Canvas c)
    {
        return c != null && c.gameObject.activeInHierarchy;
    }

    // เล่น BGM โดยไม่เริ่มใหม่ถ้าเป็นคลิปเดียวกันและกำลังเล่น
    public void PlayBGMClip(AudioClip clip)
    {
        if (bgmSource == null || clip == null) return;

        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource == null) return;
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    // สำหรับปุ่ม ให้เรียกผ่าน OnClick()
    public void PlayClick()
    {
        if (sfxSource == null || buttonClick == null) return;
        sfxSource.PlayOneShot(buttonClick);
    }

    // ถ้าต้องการเรียก PlayClick พร้อมปรับ volume เฉพาะครั้ง
    public void PlayClick(float volumeScale)
    {
        if (sfxSource == null || buttonClick == null) return;
        sfxSource.PlayOneShot(buttonClick, Mathf.Clamp01(volumeScale));
    }
}