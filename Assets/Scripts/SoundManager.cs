using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    private float BgmVol = 0;
    private float SfxVol = 0;

    private string mBgm = "BgmVal";
    private string mSfx = "SfxVal";

    [Header("버튼")]
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject pmusicOn;
    public GameObject pmusicOff;
    public GameObject sfxOn;
    public GameObject sfxOff;
    public GameObject psfxOn;
    public GameObject psfxOff;


    public static SoundManager instance;
    [Header("사운드등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [Header("배경음악 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;


    private void Awake()
    {
        instance = this;
        
        BgmVol = PlayerPrefs.GetFloat(mBgm, 0.25f);
        SfxVol = PlayerPrefs.GetFloat(mSfx, 1);

        for(int i = 0; i<sfxPlayer.Length; i++)
        {
            sfxPlayer[i].volume = SfxVol;
        }
        bgmPlayer.volume = BgmVol;

        if(BgmVol == 0.25f)
        {
            musicOff.SetActive(false); //인게임과 밖의 오브젝트의 동일시를 위해서
            musicOn.SetActive(true);

            pmusicOff.SetActive(false);
            pmusicOn.SetActive(true);
        }
        else
        {
            musicOff.SetActive(true);
            musicOn.SetActive(false);

            pmusicOff.SetActive(true);
            pmusicOn.SetActive(false);
        }

        if(SfxVol == 1)
        {
            sfxOff.SetActive(false);
            sfxOn.SetActive(true);

            psfxOff.SetActive(false);
            psfxOn.SetActive(true);
        }
        else
        {
            sfxOff.SetActive(true);
            sfxOn.SetActive(false);

            psfxOff.SetActive(true);
            psfxOn.SetActive(false);
        }
    }

    public void PlaySE(string _soundName)
    {
        for(int i = 0; i <sfxSounds.Length; i++)
        {
            if(_soundName == sfxSounds[i].soundName)
            {
                for(int j =0; j < sfxPlayer.Length; j++)
                {
                    if(!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfxSounds[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("플레이어가 전부 사용중입니다.");
                return;
            }
        }
        Debug.Log("등록된 효과음이 없습니다.");

    }

    public void PlayBgm(string _soundName)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_soundName == bgmSounds[i].soundName)
            {
                bgmPlayer.clip = bgmSounds[i].clip;
                bgmPlayer.Play();
                return;
            }
        }
        Debug.Log("등록된 효과음이 없습니다.");

    }
    public void BgmStop()
    {
        bgmPlayer.Stop();
    }
    public void MuteBgm()
    {

        PlayerPrefs.SetFloat(mBgm, 0);
        bgmPlayer.volume = 0;
        musicOn.SetActive(false);
        musicOff.SetActive(true);

        pmusicOn.SetActive(false);
        pmusicOff.SetActive(true);
        PlayerPrefs.Save();

    }

    public void ActiveBgm()
    {
        PlayerPrefs.SetFloat(mBgm, 0.25f);
        bgmPlayer.volume = 0.25f;
        musicOn.SetActive(true);
        musicOff.SetActive(false);

        pmusicOn.SetActive(true);
        pmusicOff.SetActive(false);
        PlayerPrefs.Save();

    }

    public void MuteSfx()
    {
        PlayerPrefs.SetFloat(mSfx, 0);
        for(int i = 0; i<sfxPlayer.Length; i++)
        {
            sfxPlayer[i].volume = 0;
        }
        sfxOn.SetActive(false);
        sfxOff.SetActive(true);

        psfxOn.SetActive(false);
        psfxOff.SetActive(true);
        PlayerPrefs.Save();
    }

    public void ActiveSfx()
    {
        PlayerPrefs.SetFloat(mSfx, 1);
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayer[i].volume = 1;
        }
        sfxOn.SetActive(true);
        sfxOff.SetActive(false);

        psfxOn.SetActive(true);
        psfxOff.SetActive(false);
        PlayerPrefs.Save();
    }

}
