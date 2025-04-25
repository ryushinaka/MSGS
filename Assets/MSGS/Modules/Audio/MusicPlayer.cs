using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
#pragma warning disable CS0414
        public Album tracks;
        public int trackindex;
        public GameObject player1, player2, currentTrack;
        public bool isPlaying = false;
        bool track1done = false, track2done = false;
        bool changingTrack;
        [Tooltip("how long a fadein/fadeout lasts in seconds")]
        public float fadeDuration = 0.7f;

        public float volume;

        [Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "", ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
        public string FilePath;

        #region MonoBehaviour methods
        void Start()
        {
            volume = 1f;
            tracks = new Album();
            if (currentTrack == null)
            {
                currentTrack = new GameObject("track 1");
                currentTrack.transform.SetParent(this.transform);
                currentTrack.AddComponent<AudioPlayOneShot>();
                currentTrack.GetComponent<AudioPlayOneShot>().OnSoundFinished += new AudioPlayOneShot.SoundFinished(OnTrack1Finished);
            }
        }

        void Update()
        {
            if (isPlaying)
            {
                //are we changing tracks?
                if (changingTrack)
                {   //which track are we changing to?
                    if (player1 == currentTrack)
                    {   //we're changing to player2

                    }
                    else if (player2 == currentTrack)
                    {   //we're changing to player1

                    }
                }
            }
        }

        private void OnDestroy()
        {
            if (player1 != null) { player1.GetComponent<AudioPlayOneShot>().Halt(); }
            if (player2 != null) { player2.GetComponent<AudioPlayOneShot>().Halt(); }

            //this just makes sure that the music stops playing appropriately
            //after the tracks are stopped, then Unitys standard object destruction 
            //will take place and finish cleanup
        }
        #endregion

        public void AssignTracks(Album ss)
        {
            tracks = ss;
        }

        [Button]
        public void Play()
        {
            if (isPlaying)
            {
                //do nothing
                MiniScriptSingleton.LogWarning(
                    "Audio: Play() called on Music player when state is already playing."
                    );
            }
            else
            {
                if (tracks != null)
                {
                    trackindex = 0;
                    isPlaying = true; track1done = false; track2done = false;
                    currentTrack.GetComponent<AudioSource>().clip = tracks[trackindex];
                    StartCoroutine(currentTrack.GetComponent<AudioPlayOneShot>().PlaySound(volume));
                }
                else
                {
                    MiniScriptSingleton.LogError(
                        "Audio: Play() called without a sound track being loaded."
                        );
                }
            }
        }

        [Button]
        public void Stop()
        { 
            if(isPlaying)
            {
                currentTrack.GetComponent<AudioSource>().Stop();
                isPlaying = false;
            }
        }

        [Button]
        public void Forward()
        {
            if (isPlaying)
            {
                if ((currentTrack.GetComponent<AudioSource>().time + 10f) <
                    currentTrack.GetComponent<AudioSource>().clip.length)
                {
                    currentTrack.GetComponent<AudioSource>().time += 10f;
                }
            }
        }

        [Button]
        public void Rewind()
        {
            if (isPlaying)
            {
                if ((currentTrack.GetComponent<AudioSource>().time - 10f) > 0)
                {
                    currentTrack.GetComponent<AudioSource>().time -= 10f;
                }
            }
        }

        [Button]
        public void NextTrack()
        {
            if (isPlaying)
            {
                if ((trackindex + 1) <= tracks.Count)
                {
                    currentTrack.GetComponent<AudioSource>().Stop();
                    currentTrack.GetComponent<AudioSource>().clip = tracks[trackindex++];
                    currentTrack.GetComponent<AudioSource>().Play();
                }
            }
        }

        [Button]
        public void PreviousTrack()
        {
            if (isPlaying)
            {
                if ((trackindex - 1) >= 0)
                {
                    currentTrack.GetComponent<AudioSource>().Stop();
                    trackindex--;
                    currentTrack.GetComponent<AudioSource>().clip = tracks[trackindex];
                    currentTrack.GetComponent<AudioSource>().Play();
                }
            }
        }

        public void ChangeTrack(string name,
            int trackposition,
            bool fadeout,
            bool fadein)
        {
            if (!tracks.Contains(name))
            {
                MiniScriptSingleton.LogError("MusicPlayer: ChangeTrack to '" + name + "' failed as the track name was not found in the music collection");
                return;
            }

            //isFadingIn = fadein; isFadingOut = fadeout;
            if (player1 == currentTrack)
            {

            }
            else if (player2 == currentTrack)
            {

            }
        }

        public void ChangeTrack(int idx,
            int trackposition,
            bool fadeout,
            bool fadein)
        {
            if (idx >= 0 && idx <= tracks.Count && tracks.Count > 0)
            {
                //isFadingIn = fadein; isFadingOut = fadeout;
                if (player1 == currentTrack)
                {

                }
                else if (player2 == currentTrack)
                {

                }
            }
            else
            {
                MiniScriptSingleton.LogError("MusicPlayer: ChangeTrack to '" + name + "' failed as the track name was not found in the music collection");
                return;
            }
        }

        public void OnTrack1Finished()
        {
            Debug.Log("MChannel 1 Finished: " + tracks[trackindex].name);
            if (isPlaying)
            {
                trackindex++;
                if (trackindex >= tracks.Count) { trackindex = 0; }
                player1.GetComponent<AudioSource>().clip = tracks[trackindex];
                player1.GetComponent<AudioPlayOneShot>().PlaySound(1f);
            }
        }

        public void OnTrack2Finished()
        {
            Debug.Log("MChannel 2 Finished: " + tracks[trackindex].name);
            if (isPlaying)
            {
                trackindex++;
                if (trackindex >= tracks.Count) { trackindex = 0; }
                player1.GetComponent<AudioSource>().clip = tracks[trackindex];
                player1.GetComponent<AudioPlayOneShot>().PlaySound(1f);
            }
        }

        [Button]
        public void LoadTracks()
        {
            tracks.LoadSoundSheet(FilePath);

            Debug.Log("tracks loaded: " + tracks.Count);
        }

#pragma warning restore cs0414
    }
}
