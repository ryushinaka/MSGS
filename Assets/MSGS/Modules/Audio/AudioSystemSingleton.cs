using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Audio;
using System;

namespace MiniScript.MSGS.Audio
{
    public class AudioSystemSingleton : Singleton<AudioSystemSingleton>
    {
        public List<Album> soundAlbums = new List<Album>();
        public List<SoundClip> generalSounds = new List<SoundClip>();
        //this stores the 
        public Dictionary<ValMap, Album> tempAlbums = new Dictionary<ValMap, Album>();

        public void NewAlbum(ValMap reff, string label)
        {
            bool found = false;
            foreach (KeyValuePair<ValMap, Album> kv in tempAlbums)
            {
                if (kv.Value.Name == label && kv.Key.Equals(reff)) { found = true; }
            }
            if (found)
            {
                MiniScript.MSGS.MiniScriptSingleton.LogError(
                    "Maestro.NewAlbum : Unable to create new SoundAlbum instance, name is already in use."
                    );
                return;
            }

            tempAlbums.Add(reff, new Album());
        }

        public void AddTrack(string lebal, AudioClip clp)
        {
            bool found = false;
            for (int i = 0; i < generalSounds.Count; i++)
            {
                if (generalSounds[i].label == lebal) { found = true; break; }
            }
            if (!found)
            {
                SoundClip tmp = new SoundClip();
                tmp.label = lebal;
                tmp.clip = clp;
                generalSounds.Add(tmp);
            }
        }

        /// <summary>
        /// Searches the generalSounds list to find the 'lebal' string match
        /// </summary>
        /// <param name="reff"></param>
        /// <param name="lebal"></param>
        public void AddTrack(ValMap reff, string lebal)
        {
            bool found = false;
            foreach (KeyValuePair<ValMap, Album> kv in tempAlbums)
            {
                if (kv.Value.Name == lebal && kv.Key.Equals(reff)) { found = true; }
            }
            if (found)
            {

            }
        }

        public void RemoveTrack(ValMap reff, string lebal) { }
        public void RemoveTrack(ValMap reff, int idx) { }

        public void ChangeTrackName(ValMap reff, string lebal, string s) { }

        public void FinishAlbum(ValMap reff, string label)
        {
            bool found = false;
            foreach (KeyValuePair<ValMap, Album> kv in tempAlbums)
            {
                if (kv.Value.Name == label && kv.Key.Equals(reff)) { found = true; }
            }
            if (!found)
            {
                MiniScript.MSGS.MiniScriptSingleton.LogError(
                    "Maestro.NewAlbum : Unable to create new SoundAlbum instance, name is already in use."
                    );
                return;
            }

            bool found2 = false;
            foreach (KeyValuePair<ValMap, Album> kv in tempAlbums)
            {
                if (kv.Value.Name == label && kv.Key.Equals(reff)) { found2 = true; }
            }
            if (!found2)
            {
                MiniScript.MSGS.MiniScriptSingleton.LogError(
                    "Maestro.NewAlbum : Unable to create new SoundAlbum instance, name is already in use."
                    );
                return;
            }

        }

        public bool FindTempAlbum(ValMap reff, out Album ss)
        {
            foreach (KeyValuePair<ValMap, Album> kv in tempAlbums)
            {
                if (kv.Key.Equals(reff))
                {
                    ss = kv.Value;
                    return true;
                }
            }

            ss = null;
            return false;
        }

        public bool AlbumNameExists(string lebal)
        {   
            //check the tempAlbums first
            foreach(KeyValuePair<ValMap, Album> kv in tempAlbums)
            {
                if(kv.Value.Name == lebal) { return true; }
            }
            //check the other albums
            foreach(Album a in soundAlbums)
            {
                if(a.Name == lebal) { return true; }
            }

            return false;
        }
    }
}
