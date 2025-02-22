using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;

namespace MiniScript.MSGS.Audio
{
    [Serializable]
    public class SoundSheetElement
    {
        public string Label;
        public Guid UniqueID;
        [Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "", ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
        public string SoundPath;

        public AudioClip Sound;

        [Tooltip("Preset positions on a track for seeking to particular audio cues")]
        public List<SoundSheetElementPositions> indices;
    }

    [Serializable]
    public class SoundSheetElementPositions
    {
        [Tooltip("Unique name for this position (within this track)")]
        public string label;
        [Tooltip("Position on the track in seconds (ie 4 or 7.5)")]
        public float position;
    }
}
