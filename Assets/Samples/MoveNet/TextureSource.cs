using UnityEngine;
using UnityEngine.Events;

// เปลี่ยนจาก IDisposable เป็น MonoBehaviour เพื่อให้ใช้ GetComponent ได้
public abstract class TextureSource : MonoBehaviour
{
    public virtual int width { get; protected set; }
    public virtual int height { get; protected set; }
    public virtual Texture Texture { get; protected set; }
    public virtual string SourceName { get; protected set; }

    [System.Serializable]
    public class TextureEvent : UnityEvent<Texture> { }
    public TextureEvent OnTexture = new TextureEvent();

    public abstract void Play();
    public abstract void Stop();
    public abstract void Pause();
    public abstract void Resume();

    // เปลี่ยนจาก Dispose เป็น OnDestroy ของ Unity
    protected virtual void OnDestroy()
    {
        OnTexture.RemoveAllListeners();
    }
}