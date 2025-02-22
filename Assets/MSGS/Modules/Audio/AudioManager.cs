using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public MusicPlayer MusicPlayer;

        private void Start()
        {   
            this.name = "AudioSingleton";
            var go = new GameObject();
            go.AddComponent<MusicPlayer>();
            go.transform.SetParent(this.transform);
            go.name = "MusicPlayer";
            MusicPlayer = go.GetComponent<MusicPlayer>();

            MiniScriptSingleton.Audio = this;
        }

        public void StartMusic()
        {

        }

        public void StopMusic()
        {

        }

        public void ResumeMusic()
        {

        }

        public void PauseMusic()
        {

        }

        public void PlaySound(string name)
        {
            var clip = SoundSheetCollection.Get(name);
            if (clip != null)
            {
                var go = new GameObject();
                go.AddComponent<AudioPlayOneShot>();
                go.GetComponent<AudioPlayOneShot>().ASource.clip = clip;
                go.GetComponent<AudioPlayOneShot>().ASource.Play();
            }
            else
            {
                MiniScriptSingleton.LogWarning("Audio: PlaySound('" + name + "') AudioClip not found.");
            }
        }

        public void PlaySound(string sheet, string name)
        {
            var clip = SoundSheetCollection.Get(sheet, name);
            if (clip != null)
            {
                var go = new GameObject();
                go.AddComponent<AudioPlayOneShot>();
                go.GetComponent<AudioPlayOneShot>().ASource.clip = clip;
                go.GetComponent<AudioPlayOneShot>().ASource.Play();
            }
            else
            {
                MiniScriptSingleton.LogWarning("Audio: PlaySound('" + sheet + "'/'" + name + "') AudioClip not found.");
            }
        }
    }
}
