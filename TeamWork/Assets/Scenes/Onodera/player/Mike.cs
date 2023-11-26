using System;
using System.Linq;
using UnityEngine;

public class Mike : MonoBehaviour
{
    // デバイス名
    [SerializeField] private string m_DeviceName;
    // オーディオクリップ
    private AudioClip m_AudioClip;
    // 最終点
    private int m_LastAudioPos;
    // レベル
    private float m_AudioLevel;
    // フィーバータイムのコンポーネント
    private FeverTime m_fever;
    // 切り捨て値
    [SerializeField, Range(0, 10)] private float m_lowestVolume;
    // ゲイン
    [SerializeField, Range(10, 100)] private float m_AmpGain;
    // ボリューム
    private float m_volume = 0;

    // Start is called before the first frame update
    void Start()
    {
        string targetDevice = "";

        foreach (var device in Microphone.devices)
        {
            Debug.Log($"Device Name: {device}");
            if (device.Contains(m_DeviceName))
            {
                targetDevice = device;
            }
        }

        Debug.Log($"=== Device Set: {targetDevice} ===");
        m_AudioClip = Microphone.Start(targetDevice, true, 10, 48000);
        m_fever = GetComponent<FeverTime>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_fever.GetIsBig() == false)
        //{
        //    return;
        //}

        float[] waveData = GetUpdatedAudio();
        if (waveData.Length == 0) return;

        m_AudioLevel = waveData.Average(Mathf.Abs);
        float vol = m_AmpGain * m_AudioLevel;
        if (vol < m_lowestVolume)
        {
            vol = 0;
        }

        m_volume = vol;
    }

    /// <summary>
    /// オーディオの更新した値を取得
    /// </summary>
    /// <returns>float</returns>
    private float[] GetUpdatedAudio()
    {

        int nowAudioPos = Microphone.GetPosition(null);// nullでデフォルトデバイス

        float[] waveData = Array.Empty<float>();

        if (m_LastAudioPos < nowAudioPos)
        {
            int audioCount = nowAudioPos - m_LastAudioPos;
            waveData = new float[audioCount];
            m_AudioClip.GetData(waveData, m_LastAudioPos);
        }
        else if (m_LastAudioPos > nowAudioPos)
        {
            int audioBuffer = m_AudioClip.samples * m_AudioClip.channels;
            int audioCount = audioBuffer - m_LastAudioPos;

            float[] wave1 = new float[audioCount];
            m_AudioClip.GetData(wave1, m_LastAudioPos);

            float[] wave2 = new float[nowAudioPos];
            if (nowAudioPos != 0)
            {
                m_AudioClip.GetData(wave2, 0);
            }

            waveData = new float[audioCount + nowAudioPos];
            wave1.CopyTo(waveData, 0);
            wave2.CopyTo(waveData, audioCount);
        }

        m_LastAudioPos = nowAudioPos;

        return waveData;
    }

    /// <summary>
    /// ボリュームを取得
    /// </summary>
    /// <returns>float</returns>
    public float GetVolume()
    {
        return m_volume;
    }
}