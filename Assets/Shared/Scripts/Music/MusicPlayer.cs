using Ball.Shared.Utility;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ball.Shared
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : Singleton<MusicPlayer>
    {
        [SerializeField] AudioSource source;

        MusicSet musicSet;
        AudioClip[] music;

        int currentMusicIndex;
        float nextMusicTime;

        private void Reset()
        {
            if (!source)
                source = GetComponent<AudioSource>();
        }

        private new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private void Update()
        {
            if (Time.time < nextMusicTime)
                return;

            AudioClip currentMusic = music[currentMusicIndex];
            source.clip = currentMusic;
            source.Play();
            nextMusicTime = Time.time + currentMusic.length + 3;
            currentMusicIndex = ++currentMusicIndex % music.Length;
        }

        void RandomizeOrder()
        {
            for (int i = 0; i < music.Length - 1; i++)
            {
                Swap(music, i, Random.Range(i, music.Length));
            }
        }

        void Swap<T>(T[] array, int i, int j)
        {
            T t = array[i];
            array[i] = array[j];
            array[j] = t;
        }

        void HandleSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            LevelMusic newLevelMusic = FindObjectOfType<LevelMusic>();
            if (newLevelMusic.MusicSet != musicSet)
                PlayNewLevelMusic();
        }

        private void PlayNewLevelMusic()
        {
            nextMusicTime = Time.time;
            currentMusicIndex = 0;

            LevelMusic levelMusic = FindObjectOfType<LevelMusic>();
            musicSet = levelMusic.MusicSet;
            music = musicSet.Music.ToArray();
            RandomizeOrder();
        }
    }
}