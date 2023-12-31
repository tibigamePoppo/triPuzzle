using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// どこからでも特定のSEをShotSeを通じて鳴らすことができる
/// </summary>
namespace Audio
{
    public class BGMManager : MonoBehaviour
    {
        public static BGMManager Instance { get; private set; }
        public float CurrentVolume { get; private set; }
        public AudioSource AudioSource { get; private set; }

        [SerializeField] private List<AudioClip> seLists;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                AudioSource = GetComponent<AudioSource>();
                CurrentVolume = AudioSource.volume;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShotSe(BGMType type)
        {
            AudioClip clip = null;
            clip = seLists.FirstOrDefault(se => se.name.Equals(type.ToString()));

            if (clip != null)
            {
                AudioSource.Stop();
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }
        //SeManager.Instance.ShotSe(SeType.MoveCard);と入力すると音が出る?
    }
}