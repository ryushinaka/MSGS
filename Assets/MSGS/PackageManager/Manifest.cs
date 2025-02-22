using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib;
using System.Data;
using System.IO;

namespace MiniScript.MSGS.PackageManager
{
    /// <summary>
    /// Container class that contains the declaration of the mod archive file.
    /// </summary>
    public class Manifest
    {
        public DataSet set = DefaultSet();
        List<ManifestEntry> entries = new List<ManifestEntry>();

        public IEnumerator<ManifestEntry> Entries
        {
            get
            {
                return entries.GetEnumerator();
            }
        }

        public int Count
        {
            get { return entries.Count; }
        }

        public void AddEntry(ManifestEntry me)
        {
            entries.Add(me);
            switch (me.ftype)
            {
                case FileTypes.Data:
                    var da = set.Tables["data"].NewRow();
                    da["name"] = me.filename;
                    da["filename"] = me.filename;
                    da["type"] = me.dtypes.ToString();
                    set.Tables["data"].Rows.Add(da);
                    return;

                case FileTypes.Image:
                    var im = set.Tables["images"].NewRow();
                    im["name"] = me.filename;
                    im["filename"] = me.filename;
                    set.Tables["images"].Rows.Add(im);
                    return;

                case FileTypes.Model3D:
                    var mo = set.Tables["models"].NewRow();
                    mo["name"] = me.filename;
                    mo["filename"] = me.filename;
                    set.Tables["models"].Rows.Add(mo);
                    return;

                case FileTypes.Script:
                    var sc = set.Tables["scripts"].NewRow();
                    sc["name"] = me.filename;
                    sc["filename"] = me.filename;
                    set.Tables["scripts"].Rows.Add(sc);
                    return;

                case FileTypes.Sound:
                    var so = set.Tables["sounds"].NewRow();
                    so["name"] = me.filename;
                    so["filename"] = me.filename;
                    set.Tables["sounds"].Rows.Add(so);
                    return;

                case FileTypes.Unknown:
                    return;
            }
        }

        public DataSet Finalize()
        {
            return set;
        }

        /// <summary>
        /// Loads the manifest contents from the stream given
        /// </summary>
        /// <param name="stream"></param>
        public Manifest(Stream stream)
        {
            set.ReadXml(stream, XmlReadMode.ReadSchema);
            //based on the contents of the XML datarows, rebuild the ManifestEntry list

            foreach(DataRow dr in set.Tables["data"].Rows)
            {
                entries.Add(new ManifestEntry()
                {   
                    filename = (string)dr["filename"],
                    ftype = FileTypes.Data,
                });
            }
            foreach (DataRow dr in set.Tables["scripts"].Rows)
            {
                entries.Add(new ManifestEntry()
                {
                    filename = (string)dr["filename"],
                    ftype = FileTypes.Script,
                });
            }
            foreach (DataRow dr in set.Tables["sounds"].Rows)
            {
                entries.Add(new ManifestEntry()
                {
                    filename = (string)dr["filename"],
                    ftype = FileTypes.Sound,
                });
            }
            foreach (DataRow dr in set.Tables["images"].Rows)
            {
                entries.Add(new ManifestEntry()
                {
                    filename = (string)dr["filename"],
                    ftype = FileTypes.Image,
                });
            }
            foreach (DataRow dr in set.Tables["models"].Rows)
            {
                entries.Add(new ManifestEntry()
                {
                    filename = (string)dr["filename"],
                    ftype = FileTypes.Model3D,
                });
            }
        }

        public Manifest() { }

        public static bool isValidManifest(ref System.IO.Stream stream)
        {
            if (stream == null) return false;

            DataSet set = new DataSet();
            try
            {
                set.ReadXml(stream, XmlReadMode.ReadSchema);
            }
            catch { return false; }

            bool images, scripts, data, models, sounds;

            if (set.Tables.Contains("images"))
            {
                if (set.Tables["images"].Columns.Contains("name") &&
                    set.Tables["images"].Columns.Contains("filename") &&
                    set.Tables["images"].Columns.Contains("type"))
                {
                    images = true;
                }
                else
                {
                    MiniScriptSingleton.LogWarning("Archive manifest table 'images' is missing the required DataColumns.");
                    return false;
                }
            }
            else
            {
                MiniScriptSingleton.LogWarning("Archive manifest missing images tables.");
                return false;
            }

            if (set.Tables.Contains("data"))
            {
                if (set.Tables["images"].Columns.Contains("name") &&
                    set.Tables["images"].Columns.Contains("filename") &&
                    set.Tables["images"].Columns.Contains("type"))
                {
                    data = true;
                }
                else
                {
                    MiniScriptSingleton.LogWarning("Archive manifest table 'data' is missing the required DataColumns.");
                    return false;
                }
            }
            else
            {
                MiniScriptSingleton.LogWarning("Archive manifest missing data table.");
                return false;
            }

            if (set.Tables.Contains("scripts"))
            {
                //dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
                //dc = new DataColumn("filename", typeof(string)); dt.Columns.Add(dc);
                //dc = new DataColumn("runOnDedicatedThread", typeof(string)); dt.Columns.Add(dc);
                //dc = new DataColumn("autoRestart", typeof(string)); dt.Columns.Add(dc);
                //dc = new DataColumn("OnCompletion", typeof(string)); dt.Columns.Add(dc);
                if (set.Tables["scripts"].Columns.Contains("name") &&
                   set.Tables["scripts"].Columns.Contains("filename") &&
                   set.Tables["scripts"].Columns.Contains("runOnDedicatedThread") &&
                   set.Tables["scripts"].Columns.Contains("runUniquely") &&
                   set.Tables["scripts"].Columns.Contains("autoRestart") &&
                   set.Tables["scripts"].Columns.Contains("OnCompletion"))
                {
                    scripts = true;
                }
                else
                {
                    MiniScriptSingleton.LogWarning("Archive manifest table 'scripts' is missing the required DataColumns.");
                    return false;
                }
            }
            else
            {
                MiniScriptSingleton.LogWarning("Archive manifest missing scripts table.");
                return false;
            }

            if (set.Tables.Contains("models"))
            {   
                if(set.Tables["models"].Columns.Contains("name") &&
                    set.Tables["models"].Columns.Contains("filename"))
                {
                    models = true;
                }
                else
                {
                    MiniScriptSingleton.LogWarning("Archive manifest table 'models' is missing the required DataColumns.");
                    return false;
                }
                
            }
            else
            {
                MiniScriptSingleton.LogWarning("Archive manifest missing 3D models table.");
                return false;
            }

            if(set.Tables.Contains("sounds"))
            {
                if (set.Tables["sounds"].Columns.Contains("name") &&
                    set.Tables["sounds"].Columns.Contains("filename"))
                {
                    sounds = true;
                }
                else
                {
                    MiniScriptSingleton.LogWarning("Archive manifest table 'sounds' is missing the required DataColumns.");
                    return false;
                }
            }
            else
            {
                MiniScriptSingleton.LogWarning("Archive manifest missing sounds table.");
                return false;
            }

            if(images && data && scripts && models && sounds) { return true; }

            return false;
        }

        static DataSet DefaultSet()
        {
            DataSet set = new DataSet();

            //declaration for each image, and how it should be setup
            var dt = new DataTable("images");
            var dc = new DataColumn("key", typeof(long));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 0;
            dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("filename", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("type", typeof(string)); dt.Columns.Add(dc);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            
            dt.AcceptChanges();
            set.Tables.Add(dt);

            //declaration for each script, and how it should be setup (ScriptExecutionContext)
            dt = new DataTable("scripts");
            dc = new DataColumn("key", typeof(long));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 0;
            dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("filename", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("runOnDedicatedThread", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("runUniquely", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("autoRestart", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("OnCompletion", typeof(string)); dt.Columns.Add(dc);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            dt.AcceptChanges();
            set.Tables.Add(dt);

            //declaration for each data file, and how it should be setup
            dt = new DataTable("data");
            dc = new DataColumn("key", typeof(long));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 0;
            dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("filename", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("type", typeof(string)); dt.Columns.Add(dc);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            dt.AcceptChanges();
            set.Tables.Add(dt);

            dt = new DataTable("sounds");
            dc = new DataColumn("key", typeof(long));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 0;
            dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("filename", typeof(string)); dt.Columns.Add(dc);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            set.Tables.Add(dt);

            //declaration for each 3D model, and how it should be setup
            #region
            //columns derived from TriLibCore.AssetLoaderOptions
            dt = new DataTable("models");
            dc = new DataColumn("key", typeof(long));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 0;
            dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("filename", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("UseFileScale", typeof(bool)); dt.Columns.Add(dc);
            ////dc = new DataColumn("ApplyTexturesOffsetAndScaling", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("UserPropertiesMapper", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("LoadTexturesAsSRGB", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ApplyGammaCurveToMaterialColors", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("UseUnityNativeNormalCalculator", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("MarkTexturesNoLongerReadable", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("MergeVertices", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("UseMaterialKeywords", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("EnsureQuaternionContinuity", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("Timeout", typeof(int)); dt.Columns.Add(dc);
            //dc = new DataColumn("CloseStreamAutomatically", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ShowLoadingWarnings", typeof(bool)); dt.Columns.Add(dc);
            ////dc = new DataColumn("ExternalDataMapper", typeof(string)); dt.Columns.Add(dc);
            ////dc = new DataColumn("AnimationClipMappers", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("AnimationWrapMode", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("ResampleFrequency", typeof(float)); dt.Columns.Add(dc);
            //dc = new DataColumn("AutomaticallyPlayLegacyAnimations", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("EnforceAnimatorWithLegacyAnimations", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("DestroyOnError", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("EnforceTPose", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("DiscardUnusedTextures", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ForcePowerOfTwoTextures", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("AddAllBonesToSkinnedMeshRenderers", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("CoroutineMaxStallTime", typeof(float)); dt.Columns.Add(dc);
            //dc = new DataColumn("SearchTexturesRecursively", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("DisableTesselation", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ConvertMaterialTexturesUsingHalfRes", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ConvertMaterialTextures", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("BufferizeFiles", typeof(string)); dt.Columns.Add(dc);
            ////dc = new DataColumn("FixedAllocations", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("EmbeddedDataExtractionPath", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("ExtractEmbeddedData", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("PivotPosition", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("CompressMeshes", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("LoadPointClouds", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("SetUnusedTexturePropertiesToNull", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("MergeSingleChild", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("DisableObjectsRenaming", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportLights", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportCameras", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("LoadMaterialsProgressively", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("UseUnityNativeTextureLoader", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("EnableProfiler", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("MaxTexturesResolution", typeof(int)); dt.Columns.Add(dc);
            //dc = new DataColumn("CreateVerticesAsNativeLists", typeof(bool)); dt.Columns.Add(dc);
            ////dc = new DataColumn("LipSyncMappers", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("SampleBindPose", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("CalculateBlendShapeNormals", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportBlendShapeNormals", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("SmoothingAngle", typeof(float)); dt.Columns.Add(dc);
            //dc = new DataColumn("GenerateTangents", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("GenerateNormals", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportNormals", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("KeepQuads", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("LODScreenRelativeTransitionHeightBase", typeof(float)); dt.Columns.Add(dc);
            ////dc = new DataColumn("IndexFormat", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportColors", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportBlendShapes", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ConvexColliders", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("GenerateColliders", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("OptimizeMeshes", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("MarkMeshesAsDynamic", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ReadAndWriteEnabled", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ReadEnabled", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("LimitBoneWeights", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportMeshes", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("AddAssetUnloader", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("Static", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportVisibility", typeof(bool)); dt.Columns.Add(dc);
            ////dc = new DataColumn("HumanoidAvatarMapper", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportTangents", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("SwapUVs", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportMaterials", typeof(bool)); dt.Columns.Add(dc);
            ////dc = new DataColumn("RootBoneMapper", typeof(string)); dt.Columns.Add(dc);
            ////dc = new DataColumn("HumanDescription", typeof(string)); dt.Columns.Add(dc);
            ////dc = new DataColumn("Avatar", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("AvatarDefinition", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("ScaleThreshold", typeof(float)); dt.Columns.Add(dc);
            //dc = new DataColumn("RotationThreshold", typeof(float)); dt.Columns.Add(dc);
            //dc = new DataColumn("PositionThreshold", typeof(float)); dt.Columns.Add(dc);
            //dc = new DataColumn("SimplifyAnimations", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("AnimationType", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("FixNormalMaps", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("SortHierarchyByName", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("GenerateMipmaps", typeof(bool)); dt.Columns.Add(dc);
            ////dc = new DataColumn("TextureMappers", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("ScanForAlphaPixels", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("AlphaMaterialMode", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("Enforce16BitsTextures", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("ImportTextures", typeof(bool)); dt.Columns.Add(dc);
            //dc = new DataColumn("TextureCompressionQuality", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("ScaleFactor", typeof(float)); dt.Columns.Add(dc);
            #endregion

            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            dt.AcceptChanges();
            set.Tables.Add(dt);


            return set;
        }
    }
}

