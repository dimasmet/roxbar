using UnityEngine;

public enum SoundName
{
    Click,
    Message,
    Pour,
    Upgrade,
    NewBarOpen,
    BalanceChange,
    FunMood,
    GoodMood,
    NoFunMood,
    GlassOnTable
}

public enum MusicName
{
    MenuMusic,
    Bar1Music,
    Bar2Music,
    Bar3Music
}

public enum VibroName
{
    VibroType1,
    VibroType2,
    VibroType3
}

public class SoundsGame : MonoBehaviour
{
    public static SoundsGame Instance;

    private void Awake()
    {
        Vibration.Init();
        if (Instance == null) Instance = this;
    }

    [SerializeField] private AudioSource _audioSourceSounds;
    [SerializeField] private AudioSource _audioSourceMusic;

    [Header("Sounds")]
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _messageSound;
    [SerializeField] private AudioClip _pourSound;
    [SerializeField] private AudioClip _upgradeSound;
    [SerializeField] private AudioClip _newBarOpenSound;
    [SerializeField] private AudioClip _changeBalanceSound;
    [SerializeField] private AudioClip _funMoodSound;
    [SerializeField] private AudioClip _goodMoodSound;
    [SerializeField] private AudioClip _noFunMoodSound;
    [SerializeField] private AudioClip _glassOnTableSound;

    [Header("Musics")]
    [SerializeField] private AudioClip _bar1Music;
    [SerializeField] private AudioClip _bar2Music;
    [SerializeField] private AudioClip _bar3Music;
    [SerializeField] private AudioClip _menuMusic;

    public bool isMusicGlobalActive = true;
    public bool isSoundsGlobalActive = true;
    public bool isVibroGlobalActive = true;

    private MusicName _currentActiveMusic = MusicName.MenuMusic;

    public bool ChangeMusicStatus()
    {
        isMusicGlobalActive = !isMusicGlobalActive;

        if (isMusicGlobalActive) _audioSourceMusic.Play();
        else _audioSourceMusic.Pause();

        return isMusicGlobalActive;
    }

    public bool ChangeSoundsStatas()
    {
        isSoundsGlobalActive = !isSoundsGlobalActive;

        return isSoundsGlobalActive;
    }

    public bool ChangeVibroHaptic()
    {
        isVibroGlobalActive = !isVibroGlobalActive;

        return isVibroGlobalActive;
    }

    public void PlayShotSound(SoundName soundName)
    {
        if (isSoundsGlobalActive)
        {
            switch (soundName)
            {
                case SoundName.Click:
                    _audioSourceSounds.PlayOneShot(_clickSound);
                    break;
                case SoundName.Message:
                    _audioSourceSounds.PlayOneShot(_messageSound);
                    break;
                case SoundName.Pour:
                    _audioSourceSounds.PlayOneShot(_pourSound);
                    break;
                case SoundName.Upgrade:
                    _audioSourceSounds.PlayOneShot(_upgradeSound);
                    break;
                case SoundName.NewBarOpen:
                    _audioSourceSounds.PlayOneShot(_newBarOpenSound);
                    break;
                case SoundName.BalanceChange:
                    _audioSourceSounds.PlayOneShot(_changeBalanceSound);
                    break;
                case SoundName.FunMood:
                    _audioSourceSounds.PlayOneShot(_funMoodSound);
                    break;
                case SoundName.GoodMood:
                    _audioSourceSounds.PlayOneShot(_goodMoodSound);
                    break;
                case SoundName.NoFunMood:
                    _audioSourceSounds.PlayOneShot(_noFunMoodSound);
                    break;
                case SoundName.GlassOnTable:
                    _audioSourceSounds.PlayOneShot(_glassOnTableSound);
                    break;
            }
        }
    }

    public void PlayMusic(MusicName musicName)
    {
        if (isMusicGlobalActive)
        {
            if (_currentActiveMusic != musicName)
            {
                _currentActiveMusic = musicName;
                switch (musicName)
                {
                    case MusicName.Bar1Music:
                        _audioSourceMusic.clip = _bar1Music;
                        break;
                    case MusicName.Bar2Music:
                        _audioSourceMusic.clip = _bar2Music;
                        break;
                    case MusicName.Bar3Music:
                        _audioSourceMusic.clip = _bar3Music;
                        break;
                    case MusicName.MenuMusic:
                        _audioSourceMusic.clip = _menuMusic;
                        break;
                }

                _audioSourceMusic.Play();
            }
        }
    }

    public void RunVibroHaptic(VibroName vibroName)
    {
        if (isVibroGlobalActive)
        {
            switch (vibroName)
            {
                case VibroName.VibroType1:
                    Vibration.VibratePop();
                    break;
                case VibroName.VibroType2:
                    Vibration.VibratePeek();
                    break;
                case VibroName.VibroType3:
                    Vibration.VibrateNope();
                    break;
            }
        }
    }
}
