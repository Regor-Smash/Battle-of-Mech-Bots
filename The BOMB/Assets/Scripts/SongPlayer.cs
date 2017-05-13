using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class SongPlayer : MonoBehaviour
{
    public static SongPlayer musicPlayer;
    public AudioMixerGroup menuMixer;
    public AudioMixerGroup battleMixer;
    public AudioClip[] battleSongs;
    public AudioClip menusLoop;

    AudioSource music;
    int musicIndex = 0;

    void Awake()
    {
        if (musicPlayer == null)
        {
            musicPlayer = this;
            DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += SceneChanged;
        }
        else if (musicPlayer != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!music.isPlaying && music.outputAudioMixerGroup == battleMixer)
        {
            music.clip = battleSongs[Helper.ChangeIndexNum(battleSongs.Length, musicIndex, true)];
            music.Play();
            Debug.Log("Next Battle Song");
        }
    }

    void SceneChanged(Scene thing, LoadSceneMode other)
    {
        if (thing.name == "Tutorial")
        {
            music.Pause();
        }
        else if (thing.path.Contains("Map") && music.outputAudioMixerGroup != battleMixer)
        {
            music.outputAudioMixerGroup = battleMixer;
            music.clip = battleSongs[0];
            music.loop = false;
        }
        else if(!thing.path.Contains("Map") && music.outputAudioMixerGroup != menuMixer)
        {
            music.outputAudioMixerGroup = menuMixer;
            music.clip = menusLoop;
            music.loop = true;
        }
        
    }
}
