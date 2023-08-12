using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �ǂ�����ł������SE��ShotSe��ʂ��Ė炷���Ƃ��ł���
/// </summary>
namespace Audio
{
    public class SeManager : MonoBehaviour
    {
        public static SeManager Instance { get; private set; }
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

        public void ShotSe(SeType type)
        {
            AudioClip clip = null;
            clip = seLists.FirstOrDefault(se => se.name.Equals(type.ToString()));

            if (clip != null)
            {
                AudioSource.PlayOneShot(clip);
            }
        }
        //SeManager.Instance.ShotSe(SeType.MoveCard);�Ɠ��͂���Ɖ����o��?
    }
}