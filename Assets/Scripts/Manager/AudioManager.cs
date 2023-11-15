using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseBehaviour
{
    public static AudioManager instance;

    [SerializeField] private SongData songData;

    private bool isStartInGameSong;

    private AudioSource currentSong;
    public override void Awake()
    {
        instance = this;
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        PlayGamePlaySong();
    }
    private void PlayGamePlaySong()
    {
        if (!isStartInGameSong) return;
        PlaySound("Key/TuyAm");
        isStartInGameSong = false;
    }
    public float GetAudioLevel()
    {
        // Sử dụng spectrumData để lấy dữ liệu âm thanh từ audio source
        float[] spectrumData = new float[256];
        currentSong.GetSpectrumData(spectrumData, 0, FFTWindow.Hamming);

        // Tính toán mức độ lớn tổng cộng của spectrumData
        float audioLevel = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            audioLevel += spectrumData[i];
        }

        // Chia tổng cho độ dài của spectrumData để có giá trị trung bình
        audioLevel /= spectrumData.Length;

        return audioLevel;
    }
    //public void PlayKeySound()
    //{
    //    string keyName;
    //    if (!songData.GetCurrentSong().keyArray[currentKey].isDoubleNote)
    //    {
    //        keyName = songData.GetCurrentSong().keyArray[currentKey].note[0].name;
    //        currentKey++;
    //    }
    //    else
    //    {
    //        keyName = songData.GetCurrentSong().keyArray[currentKey].note[currentNote].name;
    //        currentNote++;
    //        if (currentNote >= 2)
    //        {
    //            currentKey++;
    //            currentNote = 0;
    //        }

    //    }

    //    PlaySound("Key/" + keyName);
    //    if (currentKey >= songData.GetCurrentSong().keyArray.Length) currentKey = 0;
    //}
    public void PlaySound(string soundName)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioClip = Resources.Load("Sounds/" + soundName) as AudioClip;
        audioSource.clip = audioClip;
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + audioClip.length);
        audioSource.PlayScheduled(AudioSettings.dspTime);

        StartCoroutine(RemoveAudioSource(audioSource));
        currentSong = audioSource;
    }
    private IEnumerator RemoveAudioSource(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(audioSource);
    }
    public void StartPlayInGameSong()
    {
        isStartInGameSong = true;
    }
}
