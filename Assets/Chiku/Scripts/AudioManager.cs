using UnityEngine;
using UnityEngine.SceneManagement;

//Create Your Own Audio Clip enums to play in game.
public enum Audio
{
    BackgroundMusic = 0,        //Music 
    ButtonClick = 1,            //SFX
    //ExampleAudioClip = 2      //Add Your Audio Clips Name Hear to play. +++
}

public class AudioManager : MonoBehaviour
{
    //✅ CALL -----> AudioManager.inst.musicButtonClicked();
    public void musicButtonClicked() {
        Music = Music == 0 ? 1 : 0;
    }

    //✅ CALL -----> AudioManager.inst.sfxButtonClicked();
    public void sfxButtonClicked()
    {
        Sfx = Sfx == 0 ? 1 : 0;
    }

    //✅ CALL -----> AudioManager.inst.isMusicOn();
    public bool isMusicOn()
    {
        return Music == 1 ? true : false;
    }

    //✅ CALL -----> AudioManager.inst.isSfxOn();
    public bool isSfxOn()
    {
        return Sfx == 1 ? true : false;
    }

    //✅ CALL -----> AudioManager.inst.Music = 0/1;          >>> 0 = OFF & 1 = ON
    private int Music
    {
        get { return PlayerPrefs.GetInt(_music, 1); }
        set
        {
            bool isMusicOn = value == 0 ? false : true;
            setMusic(isMusicOn);
            PlayerPrefs.SetInt(_music, value);
        }
    }

    //✅ CALL -----> AudioManager.inst.Sfx = 0/1;            >>> 0 = OFF & 1 = ON
    private int Sfx
    {
        get { return PlayerPrefs.GetInt(_sfx, 1); }
        set
        {
            bool isSfxOn = value == 0 ? false : true;
            setSfx(isSfxOn);
            PlayerPrefs.SetInt(_sfx, value);
        }
    }

    //Setting Status of Music/Sfx as per privious selection of User.
    #region Initialization
    
    public static AudioManager inst;
    private string _music = "music";
    private string _sfx = "sfx";
    
    private void Start()
    {
        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        setMusic(Music == 1);
        setSfx(Sfx == 1);
    }
    
    private void Awake()
    {
        singletone();
    }
    
    void singletone()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    
    [Header("Music")]
    [SerializeField] AudioSource BackGroundMusic;    
    private void setMusic(bool isOn)
    {
        BackGroundMusic.mute = !isOn;
    }

    [Header("SFX")]
    [SerializeField] AudioSource sfx;
    [Header("Add+ SFX Audio Clips")]
    [Tooltip("Add Your SFX AudioClips inside List as per Audio Enum Sequence")]
    [SerializeField] AudioClip[] sfxClipList;
    
    private void setSfx(bool isOn)
    {
        sfx.enabled = isOn;
    }
    #endregion

    //✅ CALL -----> AudioManager.inst.play(Audio.ButtonClick);               << With Default Volume Level.
    //CALL -----> AudioManager.inst.play(Audio.ButtonClick, 0.5f);         << With Volume Handle.
    #region You_Can_Call_This_From_Any_of_your_Script_By_Using_Just_Enum_To_Play_That_Sound.
    public void Play(Audio soundType,float volume = 1f)
    {
        try
        {
            sfx.PlayOneShot(sfxClipList[(int)soundType]);
            sfx.volume = volume;
        }
        catch (System.Exception)
        {
            Debug.LogError("Unable to Play Sound: " + soundType);
        }
    }
    #endregion

    //✅ CALL -----> AudioManager.inst.Stop();
    #region Stop_Playing_Audio_Any_Time 
    public void Stop()
    {
        try
        {
            sfx.Stop();
        }
        catch (System.Exception)
        {
            Debug.LogError("Can't Stop Sound:");
        }
    }
    #endregion

    //In Main Game-Play You need To SlowDown Main Background Music To Hear Clear SFX of game.
    #region GAME_PLAY_MUSIC_HANDLER
    private string yourGamePlaySceneName = "yourGamePlaySceneName";
    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetSceneByBuildIndex(level).name.Equals(yourGamePlaySceneName))
        {            
            BackGroundMusic.volume /= 2;
        }
        else
        {
            BackGroundMusic.volume = 1;
        }
    }
    #endregion

}//Class END.
