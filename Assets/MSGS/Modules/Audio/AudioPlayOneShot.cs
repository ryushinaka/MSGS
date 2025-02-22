using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.Audio
{
    [RequireComponent(typeof(UnityEngine.AudioSource))]
    public class AudioPlayOneShot : MonoBehaviour
    {
        public AudioSource ASource;
        public delegate void SoundFinished();
        public SoundFinished OnSoundFinished;

        // Start is called before the first frame update
        void Start()
        {
            ASource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator PlaySound(float volume)
        {
            ASource.Play();
            yield return new WaitForSeconds(ASource.clip.length);
            if (OnSoundFinished != null) { OnSoundFinished.Invoke(); }

#if UNITY_EDITOR
            DestroyImmediate(this.gameObject);
#else
            Destroy(this.gameObject);
#endif

        }

        public void FadeOut(float fadeduration)
        {

        }

        public void FadeIn(float fadeduration) 
        {

        }

        public void Halt() 
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
