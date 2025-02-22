using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace MiniScript.MSGS.Audio
{

    /// <summary>
    /// This object is meant for debugging purposes only!!!!
    /// DO NOT make use of this object during runtime/play mode as it blatantly creates double copies of the
    /// AudioClip data loaded from file.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundSheetInstance : MonoBehaviour
    {
        [Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "", ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
        public string FilePath;
        Album Sounds;
        public int SoundIndex = 0;

        public List<Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>>> tuples = new List<Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>>>();

        public List<SoundSheetElement> Elements;

        [ShowInInspector]
        public List<AudioClip> sounds
        {
            get
            {
                List<AudioClip> rst = new List<AudioClip>();
                foreach (Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>> t in tuples)
                {
                    rst.Add(t.Item3);
                }
                return rst;
            }
        }

        [Button]
        public void LoadSoundSheet()
        {
            Sounds = new Album();

            SoundSheetExtensions.LoadSoundSheet(Sounds, FilePath);

            tuples = new List<Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>>>();            
            tuples = Sounds.tuples;
            
            Elements = new List<SoundSheetElement>();            
            foreach(Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>> t in tuples)
            {
                Elements.Add(new SoundSheetElement()
                {
                    indices = t.Item4,
                    Sound = t.Item3,
                    UniqueID = t.Item2,
                    Label = t.Item1,
                });
            }
        }
    
        [Button]
        public void PlaySounds()
        {
            StartCoroutine("PlaySheet");
        }

        [Button]
        public void PlayClip()
        {
            //Debug.Log("tuples: " + tuples.Count);

            var go = new GameObject();
            
            go.AddComponent<AudioPlayOneShot>();
            go.GetComponent<AudioPlayOneShot>().ASource = go.GetComponent<AudioSource>();
            go.GetComponent<AudioPlayOneShot>().ASource.clip = Sounds[SoundIndex];
            StartCoroutine(go.GetComponent<AudioPlayOneShot>().PlaySound(1f));
        }

        IEnumerator PlaySheet()
        {
            int x = 0;
            foreach (SoundSheetElement sse in Elements)
            {
                var ac = Instantiate(Resources.Load("AudioClipPlayer") as GameObject);

                ac.GetComponent<AudioPlayOneShot>().ASource.clip = Elements[x].Sound;
                StartCoroutine(ac.GetComponent<AudioPlayOneShot>().PlaySound(1f));
                x++;
            }

            yield return null;
        }
    }
}

