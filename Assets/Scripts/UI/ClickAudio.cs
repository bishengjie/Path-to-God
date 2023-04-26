using UnityEngine;

public class ClickAudio : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private ManagerVars _vars;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        _vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.PlayClickAudio, PlayAudio);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.PlayClickAudio, PlayAudio);
    }
    private void PlayAudio()
    {
        m_AudioSource.PlayOneShot(_vars.buttonClip);
    }
    
    // 音效是否开启
    private void IsMusicOn(bool value)
    {
        m_AudioSource.mute = !value;
    }
}
