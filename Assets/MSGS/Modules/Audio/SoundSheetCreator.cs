using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using MiniScript.MSGS.ScriptableObjects;
using System;

namespace MiniScript.MSGS.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSheetCreator : MonoBehaviour
    {
        public string SheetName;
        AudioSource source;

        [FolderPath(AbsolutePath = true, ParentFolder = "Assets/", RequireExistingPath = true)]
        public string PkgFolder;

        public SoundSheetElement[] SoundSheet;

        private void Awake()
        {
            if (SoundSheet == null) SoundSheet = new SoundSheetElement[1];
        }

        void Start()
        {
            SoundSheet = new SoundSheetElement[1];
        }

        void Update()
        {

        }

        [Button]
        void Add()
        {
            if (SoundSheet == null) { SoundSheet = new SoundSheetElement[1]; }
            var tmp = new SoundSheetElement[SoundSheet.Length + 1];
            System.Array.Copy(SoundSheet, tmp, SoundSheet.Length);
            tmp[tmp.Length - 1] = new SoundSheetElement()
            {
                UniqueID = System.Guid.NewGuid()
            };
            SoundSheet = tmp;
        }

        [Button]
        void WriteSoundSheet()
        {
            source = this.GetComponent<AudioSource>();
            Album atlas = new Album();
            atlas.Name = SheetName;
            foreach (SoundSheetElement kv in SoundSheet)
            {
                var x = System.IO.File.ReadAllBytes(kv.SoundPath);
                atlas.tuples.Add(Tuple.Create<string, Guid, AudioClip, List<SoundSheetElementPositions>>(
                    kv.Label,
                    System.Guid.NewGuid(),
                    WaveUtility.ToAudioClip(x, 0, kv.Label),
                    kv.indices));
            }
            
            atlas.SaveSoundSheet(PkgFolder + "/" + SheetName + ".xml");
        }
    }
}
