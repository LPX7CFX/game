using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Canvas introCanvas;
    [Header("Canvas Intro (เรียงลำดับ)")]
    [Tooltip("ลาก Canvas แต่ละหน้ามาใส่ตามลำดับ")]
    [SerializeField] private GameObject[] introPages;

    [Header("ตั้งเวลา Fade")]
    [Tooltip("เวลาที่ใช้ในการ Fade In (วินาที)")]
    [SerializeField] private float fadeInDuration = 1f;

    [Tooltip("เวลาที่แสดงหน้า (วินาที)")]
    [SerializeField] private float displayDuration = 2f;

    [Tooltip("เวลาที่ใช้ในการ Fade Out (วินาที)")]
    [SerializeField] private float fadeOutDuration = 1f;

    [Header("Canvas ถัดไป")]
    [Tooltip("ลาก Canvas หลักที่จะแสดงหลังจบ Intro")]
    [SerializeField] private Canvas nextCanvas;

    private CanvasGroup[] canvasGroups;
    private bool isSkipping = false;

    private void Start()
    {
        SetupCanvasGroups();
        HideAllPages();
        StartCoroutine(PlayIntroSequence());
    }

    private void Update()
    {
        // กดคลิกซ้าย = ข้าม
        if (!isSkipping && Input.GetMouseButtonDown(0))
        {
            SkipIntro();
        }
    }

    /// <summary>
    /// เตรียม CanvasGroup สำหรับแต่ละหน้า
    /// </summary>
    private void SetupCanvasGroups()
    {
        canvasGroups = new CanvasGroup[introPages.Length];

        for (int i = 0; i < introPages.Length; i++)
        {
            if (introPages[i] != null)
            {
                canvasGroups[i] = introPages[i].GetComponent<CanvasGroup>();

                if (canvasGroups[i] == null)
                {
                    canvasGroups[i] = introPages[i].gameObject.AddComponent<CanvasGroup>();
                }
            }
        }
    }

    /// <summary>
    /// ซ่อนทุกหน้า
    /// </summary>
    private void HideAllPages()
    {
        foreach (var canvas in introPages)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }
        }

        foreach (var cg in canvasGroups)
        {
            if (cg != null)
            {
                cg.alpha = 0f;
            }
        }

        // ซ่อน Canvas ถัดไป
        if (nextCanvas != null)
        {
            nextCanvas.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// เล่น Intro ทั้งหมดตามลำดับ
    /// </summary>
    private IEnumerator PlayIntroSequence()
    {
        for (int i = 0; i < introPages.Length; i++)
        {
            if (isSkipping) break;

            yield return StartCoroutine(ShowPage(i));
        }

        // จบแล้ว -> แสดง Canvas ถัดไป
        if (!isSkipping)
        {
            ShowNextCanvas();
        }
    }

    /// <summary>
    /// แสดงหน้าที่กำหนด พร้อม Fade In/Out
    /// </summary>
    private IEnumerator ShowPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= introPages.Length) yield break;
        if (introPages[pageIndex] == null) yield break;

        GameObject currentobject = introPages[pageIndex];
        CanvasGroup currentCanvasGroup = canvasGroups[pageIndex];

        // เปิด Canvas
        currentobject.gameObject.SetActive(true);

        // Fade In
        yield return StartCoroutine(FadeCanvasGroup(currentCanvasGroup, 0f, 1f, fadeInDuration));

        // แสดงหน้า
        yield return new WaitForSeconds(displayDuration);

        // Fade Out
        yield return StartCoroutine(FadeCanvasGroup(currentCanvasGroup, 1f, 0f, fadeOutDuration));

        // ปิด Canvas
        currentobject.gameObject.SetActive(false);
    }

    /// <summary>
    /// Fade CanvasGroup จาก alpha หนึ่งไปอีก alpha หนึ่ง
    /// </summary>
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        if (cg == null) yield break;

        float elapsed = 0f;
        cg.alpha = startAlpha;

        while (elapsed < duration)
        {
            if (isSkipping) yield break;

            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        cg.alpha = endAlpha;
    }

    /// <summary>
    /// ข้าม Intro ไปแสดง Canvas ถัดไปทันที
    /// </summary>
    private void SkipIntro()
    {
        if (isSkipping) return;

        isSkipping = true;
        StopAllCoroutines();

        // ซ่อนทุก Canvas Intro
        foreach (var canvas in introPages)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }
        }

        ShowNextCanvas();
    }

    /// <summary>
    /// แสดง Canvas ถัดไป
    /// </summary>
    private void ShowNextCanvas()
    {
        if (nextCanvas != null)
        {
            nextCanvas.gameObject.SetActive(true);
            introCanvas.gameObject.SetActive(false);
        }
    }
}