using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript;

namespace MiniScript.MSGS.Audio
{
    public static class AudioIntrinsics
    {
        static ValMap mapIntrinsics;

        public static ValMap Get()
        {
            if(mapIntrinsics == null) { Initialize(); }
            return mapIntrinsics;
        }

        static void Initialize()
        {
            mapIntrinsics = new ValMap();

            var a = Intrinsic.Create("");
            #region LoadMusicTracks
            a.AddParam("AlbumName", new ValString("DefaultAlbum"));
            a.AddParam("StartingTrack", new ValNumber(0));
            a.AddParam("TrackPosition", new ValNumber(0));
            
            a.code = (context, PartialResult) =>
            {


                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("LoadMusicTracks"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("LoadMusicTracks", 
                "Loads the SoundAlbum specified",
                "Audio",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "AlbumName", variableType = typeof(System.String), 
                        Comment = "The name of the SoundAlbum to load for music tracks."
                    },
                    new IntrinsicParameter() { Name = "StartingTrack", variableType = typeof(System.Double),
                        Comment = "The track index to load first from the SoundAlbum." 
                    },
                    new IntrinsicParameter() { Name = "TrackPosition", variableType = typeof(System.Double), 
                        Comment = "The position (as seconds) in the track specified in 'StartingTrack'." 
                    }
                },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region UnloadMusicTracks
            a.code = (context, PartialResult) =>
            {

                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("UnloadMusicTracks"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("UnloadMusicTracks",
                "Unloads the currently loaded tracks",
                "Audio",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region ChangeMusicTrack
            a.AddParam("ChangeTo"); //name or index of the new track
            a.AddParam("TrackPosition"); //where in the new track to jump to
            a.AddParam("fadeout"); //fade out the old track yes/no
            a.AddParam("fadein"); //fade in the new track yes/no

            a.code = (context, PartialResult) =>
            {
                if(context.GetLocal("ChangeTo") is ValNumber)
                {

                }
                else if (context.GetLocal("ChangeTo") is ValString)
                {

                }
                
                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("ChangeMusicTrack"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("ChangeMusicTrack",
                "Changes the current track to the specified track (by index or name)",
                "Audio",
                new List<IntrinsicParameter>()
                {
                    new IntrinsicParameter()
                    {
                        Name = "ChangeTo",
                        variableType = typeof(int),
                        Comment = "The track to change to (either the name of the track or the position in the list as a number)"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "TrackPosition",
                        variableType = typeof(int),
                        Comment = "The track position to jump/seek to in the new track (as seconds)"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "fadeout",
                        variableType = typeof(int),
                        Comment = "True/False to fade out the previous track"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "fadein",
                        variableType = typeof(int),
                        Comment = "True/False to fade in the new track"
                    },
                },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region PauseMusic
            a.code = (context, PartialResult) =>
            {
                MiniScriptSingleton.Audio.PauseMusic();
                
                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("PauseMusic"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("PauseMusic",
                "Pauses playing the current track of music",
                "Audio",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region ResumeMusic
            a.code = (context, PartialResult) =>
            {
                MiniScriptSingleton.Audio.ResumeMusic();

                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("ResumeMusic"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("ResumeMusic",
                "Resumes playing the current music track",
                "Audio",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region StopMusic
            a.code = (context, PartialResult) =>
            {
                MiniScriptSingleton.Audio.StopMusic();

                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("StopMusic"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("StopMusic",
                "Stops playing the current music track",
                "Audio",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region StartMusic
            a.code = (context, PartialResult) =>
            {
                MiniScriptSingleton.Audio.StartMusic();

                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("StartMusic"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("StartMusic",
                "Starts playing the audio from the first track loaded currently",
                "Audio",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region PlaySound
            a.AddParam("Album");
            a.AddParam("name");
            a.code = (context, partialResult) =>
            {
                MiniScriptSingleton.Audio.PlaySound(
                    context.GetLocalString("Album"),
                    context.GetLocalString("name")
                    );
                
                return new Intrinsic.Result(null);
            };

            mapIntrinsics.map.Add(new ValString("PlaySound"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("PlaySound",
                "Plays the specified sound",
                "Audio",
                new List<IntrinsicParameter>() { 
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { }
                },
                new IntrinsicParameter()
                );
            #endregion

            a = Intrinsic.Create("");
            #region Maestro           
            a.code = (context, partialResult) =>
            {
                return new Intrinsic.Result(GetMaestro());
            };

            mapIntrinsics.map.Add(new ValString("Maestro"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("Maestro",
                "Creates an instance of Maestro, which allows for the creation/removal/editing of SoundAlbums",
                "Audio",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "albumName",
                        variableType = typeof(string),
                        Comment = "The name of the Album to assign for the Maestro instance to manage."
                    }
                },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region Conductor
            a.code = (context, partialResult) =>
            {
                return new Intrinsic.Result(GetConductor());
            };

            mapIntrinsics.map.Add(new ValString("Conductor"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("Conductor",
                "Creates an instance of Conductor, which allows for the creation/editing of sound tracks",
                "Audio",
                new List<IntrinsicParameter>()
                {

                },
                new IntrinsicParameter() { });
            #endregion
        }

        private static ValMap GetMaestro()
        {
            ValMap map = new ValMap();
            
            var a = Intrinsic.Create("");
            #region NewAlbum
            a.AddParam("label", new ValString("defaultAlbumName"));
            a.code = (context, partialResult) =>
            {
                AudioSystemSingleton.Instance.NewAlbum(map,
                    context.GetLocalString("label"));
                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("NewAlbum"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("NewAlbum",
                "Creates a new Album object associated with this instance of the Maestro",
                "Audio/Maestro",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "label",
                        variableType = typeof(string),
                        Comment = "The name/label property of the new SoundAlbum object"
                    }
                },
                new IntrinsicParameter() { }
                );
            #endregion

            a = Intrinsic.Create("");
            #region FinishAlbum
            a.AddParam("label", new ValString("defaultAlbumName"));
            a.code = (context, partialResult) =>
            {
                AudioSystemSingleton.Instance.FinishAlbum(map, context.GetLocalString("label"));
                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("FinishAlbum"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("FinishAlbum",
                "Finishes the Album object associated with this instance of the Maestro and adds it to the collection.",
                "Audio/Maestro",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "label",
                        variableType = typeof(string),
                        Comment = "The name/label property of the new SoundAlbum object"
                    }
                },
                new IntrinsicParameter() { }
                );
            #endregion

            a = Intrinsic.Create("");
            #region SetAlbumName
            a.AddParam("albumName", new ValString(""));
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("SetAlbumName"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("SetAlbumName",
                "Changes the name of the Album to the value specified.",
                "Audio/Maestro",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { }
                );
            #endregion

            a = Intrinsic.Create("");
            #region GetAlbum
            a.AddParam("albumName", new ValString(""));
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("SetAlbumName"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("SetAlbumName",
                "Changes the name of the Album to the value specified.",
                "Audio/Maestro",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { }
                );
            #endregion

            a = Intrinsic.Create("");
            #region AddTrack

            #endregion

            a = Intrinsic.Create("");
            #region RemoveTrack
            a.AddParam("trackName", new ValString("defaultTrackName"));
            a.AddParam("preserveTrack", ValNumber.Truth(true));
            a.code = (context, partialResult) => {

                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("RemoveTrack"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("RemoveTrack",
                "Removes the Track from the Album, optionally keeping the Track in the 'general' collection.",
                "Audio/Maestro",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "label",
                        variableType = typeof(string),
                        Comment = "The name/label property of the new SoundAlbum object"
                    }
                },
                new IntrinsicParameter() { }
                );
            #endregion


            a = Intrinsic.Create("");
            #region ChangeTrackName
            a.AddParam("track");
            a.AddParam("label", new ValString("defaultAlbumName"));

            a.code = (context, partialResult) =>
            {
                
                AudioSystemSingleton.Instance.FinishAlbum(map, context.GetLocalString("label"));
                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("FinishAlbum"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("FinishAlbum",
                "Finishes the Album object associated with this instance of the Maestro and adds it to the collection.",
                "Audio/Maestro",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "label",
                        variableType = typeof(string),
                        Comment = "The name/label property of the new SoundAlbum object"
                    }
                },
                new IntrinsicParameter() { }
                );
            #endregion 
            
            
            return map;
        }
    
        private static ValMap GetConductor()
        {
            ValMap map = new ValMap();


            return map;
        }
    }
}
