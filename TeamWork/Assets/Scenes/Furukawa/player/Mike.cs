using System;
using System.Linq;
using UnityEngine;

public class Mike : MonoBehaviour
{
    // �f�o�C�X��
    [SerializeField] private string m_DeviceName;
    // �I�[�f�B�I�N���b�v
    private AudioClip m_AudioClip;
    // �ŏI�_
    private int m_LastAudioPos;
    // ���x��
    private float m_AudioLevel;
    // �t�B�[�o�[�^�C���̃R���|�[�l���g
    private FeverTime m_fever;
    // �؂�̂Ēl
    [SerializeField, Range(0, 10)] private float m_lowestVolume;
    // �Q�C��
    [SerializeField, Range(10, 100)] private float m_AmpGain;
    // �{�����[��
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
    /// �I�[�f�B�I�̍X�V�����l���擾
    /// </summary>
    /// <returns>float</returns>
    private float[] GetUpdatedAudio()
    {

        int nowAudioPos = Microphone.GetPosition(null);// null�Ńf�t�H���g�f�o�C�X

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
    /// �{�����[�����擾
    /// </summary>
    /// <returns>float</returns>
    public float GetVolume()
    {
        return m_volume;
    }
}