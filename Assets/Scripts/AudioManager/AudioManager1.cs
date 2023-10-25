using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AudioManager
{
    public class AudioManager1 : MonoBehaviour
    {
        public static AudioManager1 current;

        public AudioClip myClip;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Awake()
        {
            current = this;
            AudioSource audioArray = GetComponent<AudioSource>();
        }

        public void playMyClip()
        {
            
        }
    }
}