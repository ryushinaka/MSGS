using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS;
using System.Data;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI
{
    public class PrefabCreator : MonoBehaviour
    {
        public string PrefabName;
        //public bool createSpriteAtlas;
        SpriteSheet _spriteatlas;

        List<GameObject> listOfChildren;
        List<Component> listOfComponents;
        DataSet _set;
        private void GetChildRecursive(GameObject obj)
        {
            if (null == obj) { return; }

            foreach (Transform child in obj.transform)
            {
                if (null == child) { continue; }

                listOfChildren.Add(child.gameObject);

                List<Component> tmp = new List<Component>();
                child.GetComponents<Component>(tmp);

                DataRow dr = _set.Tables["GameObjects"].NewRow();
                dr["Name"] = child.transform.name;
                if (child.parent != null) { dr["childof"] = child.parent.GetInstanceID(); }
                else { dr["childof"] = -1; }
                dr["instanceid"] = child.transform.GetInstanceID();

                _set.Tables["GameObjects"].Rows.Add(dr);

                if (AppendComponents(child.gameObject))
                {
                    GetChildRecursive(child.gameObject);
                }
            }
        }

        [Button("Create Prefab")]
        public void CreateMSUI_Prefab()
        {
            _set = CreateDefaultDataSet(PrefabName);
            _set.DataSetName = PrefabName;
            _spriteatlas = new SpriteSheet();
            _spriteatlas.Name = PrefabName;

            listOfChildren = new List<GameObject>();
            listOfComponents = new List<Component>();
            GameObject go = this.gameObject;

            DataRow dr = _set.Tables["Config"].NewRow();
            dr["Type"] = "Prefab";
            dr["Name"] = PrefabName;
            _set.Tables["Config"].Rows.Add(dr);

            dr = _set.Tables["GameObjects"].NewRow();
            dr["Name"] = go.name;
            dr["childof"] = -1;
            dr["instanceid"] = go.transform.GetInstanceID();
            _set.Tables["GameObjects"].Rows.Add(dr);

            AppendComponents(go);

            GetChildRecursive(go);

            //_spriteatlas.SaveSpriteSheet(Application.dataPath + "/" + PrefabName + "_sprites.xml");
            _set.WriteXml(Application.streamingAssetsPath + "/" + PrefabName + ".xml", XmlWriteMode.WriteSchema);
            //Debug.Log("Prefab xml created for '" + PrefabName + "' for MiniScript UI system.");
        }

        bool AppendComponents(GameObject go)
        {
            List<Component> tmp = new List<Component>();
            go.GetComponents<Component>(tmp);
            bool parseChildren = true;

            foreach (Component c in tmp)
            {
                DataRow dr = null;

                switch (c.GetType().FullName)
                {
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Button":
                        #region

                        dr = _set.Tables["Button"].NewRow();
                        MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton muibutton = (MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton)c;
                        dr["OwnerInstanceID"] = c.transform.GetInstanceID();
                        if (muibutton.transform.parent != null)
                        { dr["ChildOf"] = muibutton.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }
                        dr["enabled"] = muibutton.gameObject.activeSelf;
                        dr["name"] = muibutton.name;
                        dr["OnPointerEnterHandler"] = muibutton.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = muibutton.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = muibutton.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = muibutton.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = muibutton.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = muibutton.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = muibutton.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = muibutton.ScriptOnScrollUp;
                        dr["OnScrollDown"] = muibutton.ScriptOnScrollDown;
                        if (muibutton.image.sprite != null && muibutton.image.sprite.texture.isReadable)
                        {
                            dr["Sprite"] = System.Convert.ToBase64String(
                            muibutton.image.sprite.texture.EncodeToPNG());
                        }
                        else
                        {
                            dr["Sprite"] = string.Empty;
                        }

                        dr["ColorR"] = muibutton.image.color.r.ToString();
                        dr["ColorG"] = muibutton.image.color.g.ToString();
                        dr["ColorB"] = muibutton.image.color.b.ToString();
                        dr["ColorA"] = muibutton.image.color.a.ToString();
                        dr["Maskable"] = muibutton.image.maskable;
                        dr["PreserveAspect"] = (bool)muibutton.image.preserveAspect;

                        if (muibutton.Text.enabled && muibutton.Text.text.Length > 0)
                        {
                            dr["TextValue"] = (string)muibutton.Text.text;
                            dr["FontSize"] = muibutton.Text.fontSize;
                            dr["FontSizeMin"] = muibutton.Text.fontSizeMin;
                            dr["FontSizeMax"] = muibutton.Text.fontSizeMax;
                            dr["AutoSize"] = muibutton.Text.autoSizeTextContainer;
                            dr["TextColorR"] = muibutton.Text.color.r.ToString();
                            dr["TextColorG"] = muibutton.Text.color.g.ToString();
                            dr["TextColorB"] = muibutton.Text.color.b.ToString();
                            dr["TextColorA"] = muibutton.Text.color.a.ToString();

                            dr["alignHorizontal"] = muibutton.Text.horizontalAlignment.ToString();
                            dr["alignVertical"] = muibutton.Text.verticalAlignment.ToString();

                            var avm = muibutton.ToValMap();
                            dr["Bold"] = avm.map[new ValString("Bold")].BoolValue();
                            dr["Italic"] = avm.map[new ValString("Italic")].BoolValue();
                            dr["Underline"] = avm.map[new ValString("Underline")].BoolValue();
                            dr["Strike"] = avm.map[new ValString("Strike")].BoolValue();
                            dr["Lowercase"] = avm.map[new ValString("Lowercase")].BoolValue();
                            dr["Uppercase"] = avm.map[new ValString("Uppercase")].BoolValue();
                            dr["SmallCaps"] = avm.map[new ValString("SmallCaps")].BoolValue();
                        }
                        else
                        {
                            dr["TextValue"] = string.Empty;
                            dr["FontSize"] = 14;
                            dr["FontSizeMin"] = 14;
                            dr["FontSizeMax"] = 14;
                            dr["AutoSize"] = false;
                            dr["TextColorR"] = 255;
                            dr["TextColorG"] = 255;
                            dr["TextColorB"] = 255;
                            dr["TextColorA"] = 255;

                            dr["alignHorizontal"] = TMPro.HorizontalAlignmentOptions.Center.ToString();
                            dr["alignVertical"] = TMPro.VerticalAlignmentOptions.Middle.ToString();

                            dr["Bold"] = false;
                            dr["Italic"] = false;
                            dr["Underline"] = false;
                            dr["Strike"] = false;
                            dr["Lowercase"] = false;
                            dr["Uppercase"] = false;
                            dr["SmallCaps"] = false;
                        }

                        _set.Tables["Button"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.ButtonAnimated":
                        #region
                        dr = _set.Tables["ButtonAnimated"].NewRow();
                        MUUIButtonAnimated buttonani = (MUUIButtonAnimated)c;
                        dr["OwnerInstanceID"] = c.transform.GetInstanceID();
                        if (buttonani.transform.parent != null)
                        { dr["ChildOf"] = buttonani.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }
                        dr["enabled"] = buttonani.gameObject.activeSelf;
                        dr["name"] = buttonani.name;
                        dr["OnPointerEnterHandler"] = buttonani.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = buttonani.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = buttonani.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = buttonani.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = buttonani.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = buttonani.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = buttonani.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = buttonani.ScriptOnScrollUp;
                        dr["OnScrollDown"] = buttonani.ScriptOnScrollDown;

                        dr["ColorR"] = buttonani.image.color.r.ToString();
                        dr["ColorG"] = buttonani.image.color.g.ToString();
                        dr["ColorB"] = buttonani.image.color.b.ToString();
                        dr["ColorA"] = buttonani.image.color.a.ToString();
                        dr["Maskable"] = buttonani.image.maskable;
                        dr["PreserveAspect"] = (bool)buttonani.image.preserveAspect;

                        if (buttonani.Text.enabled && buttonani.Text.text.Length > 0)
                        {
                            dr["TextValue"] = (string)buttonani.Text.text;
                            dr["FontSize"] = buttonani.Text.fontSize;
                            dr["FontSizeMin"] = buttonani.Text.fontSizeMin;
                            dr["FontSizeMax"] = buttonani.Text.fontSizeMax;
                            dr["AutoSize"] = buttonani.Text.autoSizeTextContainer;
                            dr["TextColorR"] = buttonani.Text.color.r.ToString();
                            dr["TextColorG"] = buttonani.Text.color.g.ToString();
                            dr["TextColorB"] = buttonani.Text.color.b.ToString();
                            dr["TextColorA"] = buttonani.Text.color.a.ToString();

                            dr["alignHorizontal"] = buttonani.Text.horizontalAlignment.ToString();
                            dr["alignVertical"] = buttonani.Text.verticalAlignment.ToString();

                            var avm = buttonani.ToValMap();
                            dr["Bold"] = avm.map[new ValString("Bold")].BoolValue();
                            dr["Italic"] = avm.map[new ValString("Italic")].BoolValue();
                            dr["Underline"] = avm.map[new ValString("Underline")].BoolValue();
                            dr["Strike"] = avm.map[new ValString("Strike")].BoolValue();
                            dr["Lowercase"] = avm.map[new ValString("Lowercase")].BoolValue();
                            dr["Uppercase"] = avm.map[new ValString("Uppercase")].BoolValue();
                            dr["SmallCaps"] = avm.map[new ValString("SmallCaps")].BoolValue();
                        }
                        else
                        {
                            dr["TextValue"] = string.Empty;
                            dr["FontSize"] = 14;
                            dr["FontSizeMin"] = 14;
                            dr["FontSizeMax"] = 14;
                            dr["AutoSize"] = false;
                            dr["TextColorR"] = 255;
                            dr["TextColorG"] = 255;
                            dr["TextColorB"] = 255;
                            dr["TextColorA"] = 255;

                            dr["alignHorizontal"] = TMPro.HorizontalAlignmentOptions.Center.ToString();
                            dr["alignVertical"] = TMPro.VerticalAlignmentOptions.Middle.ToString();

                            dr["Bold"] = false;
                            dr["Italic"] = false;
                            dr["Underline"] = false;
                            dr["Strike"] = false;
                            dr["Lowercase"] = false;
                            dr["Uppercase"] = false;
                            dr["SmallCaps"] = false;
                        }

                        if (buttonani.image.sprite != null && buttonani.image.sprite.texture.isReadable)
                        {
                            dr["Sprite"] = System.Convert.ToBase64String(
                            buttonani.image.sprite.texture.EncodeToPNG());
                        }
                        else
                        {
                            dr["Sprite"] = string.Empty;
                        }

                        if (buttonani.sprites.Count > 0)
                        {
                            //System.Text.StringBuilder str = new System.Text.StringBuilder();
                            //System.IO.MemoryStream strm = new System.IO.MemoryStream(10000);
                            //int sum = 0;
                            //foreach(Sprite sp in buttonani.sprites)
                            //{
                            //    var tmpsp = sp.texture.EncodeToPNG();
                            //    sum += tmpsp.Length;
                            //    strm.Write(tmpsp, 0, tmpsp.Length);
                            //    str.Append(tmpsp.Length + ",");
                            //    strm.Flush();
                            //}

                            //strm.Position = 0;
                            ////Debug.Log(buttonani.sprites.Count + "," + sum + "," + strm.ToArray().Length);
                            //dr["Sprites"] = System.Convert.ToBase64String(strm.ToArray());
                            //dr["SpriteIndices"] = str.ToString();
                            int counter = 0;
                            foreach (Sprite sp in buttonani.sprites)
                            {
                                var zz = _set.Tables["SpritesTable"].NewRow();
                                zz["Sprite"] = System.Convert.ToBase64String(sp.texture.EncodeToPNG());
                                zz["OwnerInstanceID"] = buttonani.transform.GetInstanceID();
                                zz["Name"] = buttonani.name + "_" + counter.ToString();
                                counter++;
                                _set.Tables["SpritesTable"].Rows.Add(zz);
                            }
                        }
                        else
                        {
                            //dr["Sprites"] = string.Empty;
                            //dr["SpriteIndices"] = string.Empty;
                        }

                        dr["CycleTime"] = buttonani.cycleTime;
                        dr["StartFrame"] = buttonani.currentFrame;
                        dr["DoAnimate"] = buttonani.DoAnimate;

                        _set.Tables["ButtonAnimated"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Image":
                        #region
                        MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage i = (MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage)c;
                        dr = _set.Tables["Image"].NewRow();
                        dr["OwnerInstanceID"] = c.transform.GetInstanceID();
                        if (i.transform.parent != null)
                        { dr["ChildOf"] = i.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }
                        if (i.image.sprite != null && i.image.sprite.texture.isReadable)
                        {
                            dr["Sprite"] = System.Convert.ToBase64String(
                            i.image.sprite.texture.EncodeToPNG());
                        }
                        else
                        {
                            dr["Sprite"] = string.Empty;
                        }
                        dr["ColorR"] = i.image.color.r.ToString();
                        dr["ColorG"] = i.image.color.g.ToString();
                        dr["ColorB"] = i.image.color.b.ToString();
                        dr["ColorA"] = i.image.color.a.ToString();
                        dr["RaycastTarget"] = i.image.raycastTarget;
                        dr["Maskable"] = i.image.maskable;
                        dr["PreserveAspect"] = i.image.preserveAspect;
                        dr["enabled"] = i.image.gameObject.activeSelf;
                        dr["name"] = i.name;
                        dr["AlphaThreshold"] = i.image.alphaHitTestMinimumThreshold;
                        dr["fillAmount"] = i.image.fillAmount;
                        dr["fillCenter"] = i.image.fillCenter;
                        dr["fillClockwise"] = i.image.fillClockwise;
                        dr["fillMethod"] = i.image.fillMethod.ToString();
                        dr["type"] = i.image.type.ToString();
                        dr["OnPointerEnterHandler"] = i.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = i.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = i.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = i.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = i.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = i.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = i.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = i.ScriptOnScrollUp;
                        dr["OnScrollDown"] = i.ScriptOnScrollDown;

                        _set.Tables["Image"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated":
                        #region
                        MUUIImageAnimated ia = (MUUIImageAnimated)c;
                        dr = _set.Tables["ImageAnimated"].NewRow();
                        dr["OwnerInstanceID"] = c.transform.GetInstanceID();
                        if (ia.transform.parent != null)
                        { dr["ChildOf"] = ia.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }
                        if (ia.image.sprite != null && ia.image.sprite.texture.isReadable)
                        {
                            dr["Sprite"] = System.Convert.ToBase64String(
                            ia.image.sprite.texture.EncodeToPNG());
                        }
                        else
                        {
                            dr["Sprite"] = string.Empty;
                        }
                        dr["ColorR"] = ia.image.color.r.ToString();
                        dr["ColorG"] = ia.image.color.g.ToString();
                        dr["ColorB"] = ia.image.color.b.ToString();
                        dr["ColorA"] = ia.image.color.a.ToString();
                        dr["RaycastTarget"] = ia.image.raycastTarget;
                        dr["Maskable"] = ia.image.maskable;
                        dr["PreserveAspect"] = ia.image.preserveAspect;
                        dr["enabled"] = ia.image.gameObject.activeSelf;
                        dr["name"] = ia.name;
                        dr["AlphaThreshold"] = ia.image.alphaHitTestMinimumThreshold;
                        dr["fillAmount"] = ia.image.fillAmount;
                        dr["fillCenter"] = ia.image.fillCenter;
                        dr["fillClockwise"] = ia.image.fillClockwise;
                        dr["fillMethod"] = ia.image.fillMethod.ToString();
                        dr["type"] = ia.image.type.ToString();
                        dr["OnPointerEnterHandler"] = ia.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = ia.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = ia.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = ia.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = ia.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = ia.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = ia.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = ia.ScriptOnScrollUp;
                        dr["OnScrollDown"] = ia.ScriptOnScrollDown;

                        if (ia.sprites.Count > 0)
                        {
                            //System.Text.StringBuilder str = new System.Text.StringBuilder();
                            //System.IO.MemoryStream strm = new System.IO.MemoryStream(10000);
                            //foreach (Sprite sp in ia.sprites)
                            //{
                            //    var tmpsp = sp.texture.EncodeToPNG();
                            //    strm.Write(tmpsp, 0, tmpsp.Length);
                            //    str.Append(tmpsp.Length + ",");
                            //    strm.Flush();
                            //}

                            //strm.Position = 0;
                            //dr["Sprites"] = System.Convert.ToBase64String(strm.ToArray());
                            //dr["SpriteIndices"] = str.ToString();
                            int counter = 0;
                            foreach (Sprite sp in ia.sprites)
                            {
                                var zz = _set.Tables["SpritesTable"].NewRow();
                                zz["Sprite"] = System.Convert.ToBase64String(sp.texture.EncodeToPNG());
                                zz["OwnerInstanceID"] = ia.transform.GetInstanceID();
                                zz["Name"] = ia.name + "_" + counter.ToString();
                                counter++;
                                _set.Tables["SpritesTable"].Rows.Add(zz);
                            }
                        }
                        else
                        {
                            //dr["Sprites"] = string.Empty;
                            //dr["SpriteIndices"] = string.Empty;
                        }

                        dr["CycleTime"] = ia.cycleTime;
                        dr["StartFrame"] = ia.currentFrame;
                        dr["DoAnimate"] = ia.DoAnimate;

                        _set.Tables["ImageAnimated"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Text":
                        #region
                        MiniScript.MSGS.MUUI.TwoDimensional.MUUIText text = (MiniScript.MSGS.MUUI.TwoDimensional.MUUIText)c;
                        dr = _set.Tables["Text"].NewRow();
                        dr["OwnerInstanceID"] = text.transform.GetInstanceID();
                        if (text.transform.parent != null)
                        { dr["ChildOf"] = text.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }
                        dr["enabled"] = text.gameObject.activeSelf;
                        dr["name"] = text.name;
                        dr["TextValue"] = text.mText.text;
                        dr["FontSize"] = text.mText.fontSize;
                        dr["FontSizeMin"] = text.mText.fontSizeMin;
                        dr["FontSizeMax"] = text.mText.fontSizeMax;
                        dr["AutoSize"] = text.mText.autoSizeTextContainer;
                        dr["TextColorR"] = text.mText.color.r.ToString();
                        dr["TextColorG"] = text.mText.color.g.ToString();
                        dr["TextColorB"] = text.mText.color.b.ToString();
                        dr["TextColorA"] = text.mText.color.a.ToString();
                        dr["alignHorizontal"] = text.mText.horizontalAlignment.ToString();
                        dr["alignVertical"] = text.mText.verticalAlignment.ToString();
                        dr["OnPointerEnterHandler"] = text.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = text.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = text.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = text.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = text.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = text.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = text.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = text.ScriptOnScrollUp;
                        dr["OnScrollDown"] = text.ScriptOnScrollDown;

                        var vm = text.ToValMap();
                        dr["Bold"] = vm.map[new ValString("Bold")].BoolValue();
                        dr["Italic"] = vm.map[new ValString("Italic")].BoolValue();
                        dr["Underline"] = vm.map[new ValString("Underline")].BoolValue();
                        dr["Strike"] = vm.map[new ValString("Strike")].BoolValue();
                        dr["Lowercase"] = vm.map[new ValString("Lowercase")].BoolValue();
                        dr["Uppercase"] = vm.map[new ValString("Uppercase")].BoolValue();
                        dr["SmallCaps"] = vm.map[new ValString("SmallCaps")].BoolValue();
                        _set.Tables["Text"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Panel":
                        #region
                        MUUIPanel p = (MUUIPanel)c;
                        dr = _set.Tables["Panel"].NewRow();
                        dr["OwnerInstanceID"] = c.transform.GetInstanceID();
                        if (p.transform.parent != null)
                        { dr["ChildOf"] = p.transform.parent.gameObject.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }

                        dr["enabled"] = p.gameObject.activeSelf;
                        dr["name"] = p.name;
                        dr["draggable"] = p.isDraggable;
                        dr["OnPointerEnterHandler"] = p.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = p.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = p.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = p.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = p.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = p.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = p.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = p.ScriptOnScrollUp;
                        dr["OnScrollDown"] = p.ScriptOnScrollDown;

                        _set.Tables["Panel"].Rows.Add(dr);
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Canvas":
                        #region

                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.DropDown":
                        #region
                        MUUIDropDown drop = (MUUIDropDown)c;
                        dr = _set.Tables["DropDown"].NewRow();
                        dr["OwnerInstanceID"] = drop.transform.GetInstanceID();
                        if (drop.transform.parent != null)
                        { dr["ChildOf"] = drop.transform.parent.gameObject.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }
                        dr["FontSize"] = drop.defaultText.fontSize;
                        dr["FontSizeMin"] = drop.defaultText.fontSizeMin;
                        dr["FontSizeMax"] = drop.defaultText.fontSizeMax;
                        dr["AutoSize"] = drop.defaultText.autoSizeTextContainer;
                        dr["TextColorR"] = drop.defaultText.color.r.ToString();
                        dr["TextColorG"] = drop.defaultText.color.g.ToString();
                        dr["TextColorB"] = drop.defaultText.color.b.ToString();
                        dr["TextColorA"] = drop.defaultText.color.a.ToString();

                        dr["alignHorizontal"] = drop.defaultText.horizontalAlignment.ToString();
                        dr["alignVertical"] = drop.defaultText.verticalAlignment.ToString();

                        dr["Bold"] = (bool)((drop.defaultText.fontStyle & TMPro.FontStyles.Bold) != 0);
                        dr["Italic"] = (bool)((drop.defaultText.fontStyle & TMPro.FontStyles.Italic) != 0);
                        dr["Underline"] = (bool)((drop.defaultText.fontStyle & TMPro.FontStyles.Underline) != 0);
                        dr["Strike"] = (bool)((drop.defaultText.fontStyle & TMPro.FontStyles.Strikethrough) != 0);
                        dr["Lowercase"] = (bool)((drop.defaultText.fontStyle & TMPro.FontStyles.LowerCase) != 0);
                        dr["Uppercase"] = (bool)((drop.defaultText.fontStyle & TMPro.FontStyles.UpperCase) != 0);
                        dr["SmallCaps"] = (bool)((drop.defaultText.fontStyle & TMPro.FontStyles.SmallCaps) != 0);

                        dr["OnPointerEnterHandler"] = drop.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = drop.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = drop.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = drop.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = drop.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = drop.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = drop.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = drop.ScriptOnScrollUp;
                        dr["OnScrollDown"] = drop.ScriptOnScrollDown;

                        dr["name"] = drop.gameObject.name;
                        dr["enabled"] = drop.gameObject.activeSelf;
                        _set.Tables["DropDown"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.InputField":
                        #region

                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Scrollview":
                        #region
                        MUUIScrollview sv = (MUUIScrollview)c;
                        dr = _set.Tables["Scrollview"].NewRow();
                        dr["OwnerInstanceID"] = sv.transform.GetInstanceID();
                        if (sv.transform.parent != null)
                        { dr["ChildOf"] = sv.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }

                        dr["ContentPrefab"] = sv.ContentPrefab;
                        dr["MovementType"] = sv.ScrollRect.movementType.ToString();
                        dr["Inertia"] = sv.ScrollRect.inertia;
                        dr["DecelerationRate"] = sv.ScrollRect.decelerationRate;
                        dr["ScrollSensitivity"] = sv.ScrollRect.scrollSensitivity;
                        dr["showScrollbarHorizontal"] = sv.ScrollRect.horizontalScrollbar.enabled;
                        dr["showScrollbarVertical"] = sv.ScrollRect.verticalScrollbar.enabled;

                        dr["sizeFitterHorizontal"] = sv.fitter.horizontalFit.ToString();
                        dr["sizeFitterVertical"] = sv.fitter.verticalFit.ToString();

                        dr["layoutPaddingLeft"] = sv.layoutgroup.padding.left;
                        dr["layoutPaddingRight"] = sv.layoutgroup.padding.right;
                        dr["layoutPaddingTop"] = sv.layoutgroup.padding.top;
                        dr["layoutPaddingBottom"] = sv.layoutgroup.padding.bottom;

                        dr["layoutSpacing"] = sv.layoutgroup.spacing;
                        dr["layoutReverseArrangement"] = sv.layoutgroup.reverseArrangement;
                        dr["layoutControlHeight"] = sv.layoutgroup.childControlHeight;
                        dr["layoutControlWidth"] = sv.layoutgroup.childControlWidth;
                        dr["layoutUseChildScaleHeight"] = sv.layoutgroup.childScaleHeight;
                        dr["layoutUseChildScaleWidth"] = sv.layoutgroup.childScaleWidth;

                        dr["layoutForceExpandHeight"] = sv.layoutgroup.childForceExpandHeight;
                        dr["layoutForceExpandWidth"] = sv.layoutgroup.childForceExpandWidth;
                        dr["layoutChildAlignment"] = sv.layoutgroup.childAlignment.ToString();

                        dr["OnPointerEnterHandler"] = sv.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = sv.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = sv.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = sv.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = sv.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = sv.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = sv.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = sv.ScriptOnScrollUp;
                        dr["OnScrollDown"] = sv.ScriptOnScrollDown;

                        if (sv.ScrollBackground.sprite.texture.isReadable)
                        {
                            dr["Sprite"] = System.Convert.ToBase64String(sv.ScrollBackground.sprite.texture.EncodeToPNG());
                        }
                        else
                        {
                            dr["Sprite"] = string.Empty;
                        }

                        dr["ColorR"] = sv.ScrollBackground.color.r.ToString();
                        dr["ColorG"] = sv.ScrollBackground.color.g.ToString();
                        dr["ColorB"] = sv.ScrollBackground.color.b.ToString();
                        dr["ColorA"] = sv.ScrollBackground.color.a.ToString();

                        if(sv.HorizontalBar.sprite != null)
                        {
                            if (sv.HorizontalBar.sprite.texture.isReadable)
                            {
                                dr["HorizontalSprite"] = System.Convert.ToBase64String(sv.HorizontalBar.sprite.texture.EncodeToPNG());
                            }
                            else
                            {
                                dr["HorizontalSprite"] = string.Empty;
                            }
                        }
                        else
                        {
                            dr["HorizontalSprite"] = string.Empty;
                        }
                        
                        dr["HorizontalColorR"] = sv.HorizontalBar.color.r.ToString();
                        dr["HorizontalColorG"] = sv.HorizontalBar.color.g.ToString();
                        dr["HorizontalColorB"] = sv.HorizontalBar.color.b.ToString();
                        dr["HorizontalColorA"] = sv.HorizontalBar.color.a.ToString();

                        if(sv.HorizontalHandle.sprite != null)
                        {
                            if (sv.HorizontalHandle.sprite.texture.isReadable)
                            {
                                dr["HorizontalHandleSprite"] = System.Convert.ToBase64String(sv.HorizontalHandle.sprite.texture.EncodeToPNG());
                            }
                            else
                            {
                                dr["HorizontalHandleSprite"] = string.Empty;
                            }
                        }
                        else
                        {
                            dr["HorizontalHandleSprite"] = string.Empty;
                        }
                        
                        dr["HorizontalHandleColorR"] = sv.HorizontalHandle.color.r.ToString();
                        dr["HorizontalHandleColorG"] = sv.HorizontalHandle.color.g.ToString();
                        dr["HorizontalHandleColorB"] = sv.HorizontalHandle.color.b.ToString();
                        dr["HorizontalHandleColorA"] = sv.HorizontalHandle.color.a.ToString();

                        if(sv.VerticalBar.sprite != null)
                        {
                            if (sv.VerticalBar.sprite.texture.isReadable)
                            {
                                dr["VerticalSprite"] = System.Convert.ToBase64String(sv.VerticalBar.sprite.texture.EncodeToPNG());
                            }
                            else
                            {
                                dr["VerticalSprite"] = string.Empty;
                            }
                        }
                        else
                        {
                            dr["VerticalSprite"] = string.Empty;
                        }
                        
                        dr["VerticalColorR"] = sv.VerticalBar.color.r.ToString();
                        dr["VerticalColorG"] = sv.VerticalBar.color.g.ToString();
                        dr["VerticalColorB"] = sv.VerticalBar.color.b.ToString();
                        dr["VerticalColorA"] = sv.VerticalBar.color.a.ToString();

                        if(sv.VerticalHandle.sprite != null)
                        {
                            if (sv.VerticalHandle.sprite.texture.isReadable)
                            {
                                dr["VerticalHandleSprite"] = System.Convert.ToBase64String(sv.VerticalHandle.sprite.texture.EncodeToPNG());
                            }
                            else
                            {
                                dr["VerticalHandleSprite"] = string.Empty;
                            }
                        }
                        else
                        {
                            dr["VerticalHandleSprite"] = string.Empty;
                        }
                        
                        dr["VerticalHandleColorR"] = sv.VerticalHandle.color.r.ToString();
                        dr["VerticalHandleColorG"] = sv.VerticalHandle.color.g.ToString();
                        dr["VerticalHandleColorB"] = sv.VerticalHandle.color.b.ToString();
                        dr["VerticalHandleColorA"] = sv.VerticalHandle.color.a.ToString();

                        dr["name"] = sv.gameObject.name;
                        dr["enabled"] = sv.gameObject.activeSelf;
                        _set.Tables["Scrollview"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Slider":
                        #region
                        MiniScript.MSGS.MUUI.TwoDimensional.MUUISlider sl = (MiniScript.MSGS.MUUI.TwoDimensional.MUUISlider)c;
                        dr = _set.Tables["Slider"].NewRow();
                        dr["name"] = sl.name;
                        dr["enabled"] = sl.gameObject.activeSelf;
                        dr["OwnerInstanceID"] = sl.transform.GetInstanceID();
                        if (sl.transform.parent != null)
                        { dr["ChildOf"] = sl.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }

                        dr["OnPointerEnterHandler"] = sl.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = sl.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = sl.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = sl.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = sl.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = sl.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = sl.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = sl.ScriptOnScrollUp;
                        dr["OnScrollDown"] = sl.ScriptOnScrollDown;

                        _set.Tables["Slider"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "MiniScript.MSGS.MUUI.TwoDimensional.Toggle":
                        #region
                        MiniScript.MSGS.MUUI.TwoDimensional.MUUIToggle togg = (MiniScript.MSGS.MUUI.TwoDimensional.MUUIToggle)c;
                        dr = _set.Tables["Toggle"].NewRow();
                        dr["OwnerInstanceID"] = togg.transform.GetInstanceID();
                        if (togg.transform.parent != null)
                        { dr["ChildOf"] = togg.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }

                        dr["OnToggleChanged"] = togg.ScriptOnToggle;
                        dr["OnPointerEnterHandler"] = togg.ScriptOnEnter;
                        dr["OnPointerExitHandler"] = togg.ScriptOnExit;
                        dr["OnPointerLeftClickHandler"] = togg.ScriptOnLeftClick;
                        dr["OnPointerDoubleLeftClickHandler"] = togg.ScriptOnDoubleLeftClick;
                        dr["OnPointerRightClickHandler"] = togg.ScriptOnRightClick;
                        dr["OnPointerDoubleRightClickHandler"] = togg.ScriptOnDoubleRightClick;
                        dr["OnPointerMiddleClickHandler"] = togg.ScriptOnMiddleClick;
                        dr["OnScrollUp"] = togg.ScriptOnScrollUp;
                        dr["OnScrollDown"] = togg.ScriptOnScrollDown;

                        dr["name"] = togg.gameObject.name;
                        dr["enabled"] = togg.gameObject.activeSelf;
                        _set.Tables["Toggle"].Rows.Add(dr);
                        parseChildren = false;
                        break;
                    #endregion
                    case "UnityEngine.RectTransform":
                        #region
                        RectTransform rt = (RectTransform)c;

                        dr = _set.Tables["RectTransform"].NewRow();
                        dr["OwnerInstanceID"] = c.transform.GetInstanceID();
                        if (rt.transform.parent != null)
                        { dr["ChildOf"] = rt.transform.parent.GetInstanceID(); }
                        else { dr["ChildOf"] = -1; }
                        dr["name"] = c.name;
                        dr["enabled"] = c.gameObject.activeSelf;
                        dr["anchoredpositionX"] = rt.anchoredPosition.x.ToString();
                        dr["anchoredpositionY"] = rt.anchoredPosition.y.ToString();
                        dr["xMin"] = rt.rect.xMin.ToString();
                        dr["yMin"] = rt.rect.yMin.ToString();
                        dr["xMax"] = rt.rect.xMax.ToString();
                        dr["yMax"] = rt.rect.yMax.ToString();

                        dr["localpositionX"] = rt.localPosition.x.ToString();
                        dr["localpositionY"] = rt.localPosition.y.ToString();
                        dr["localpositionZ"] = rt.localPosition.z.ToString();

                        dr["localscaleX"] = rt.localScale.x.ToString();
                        dr["localscaleY"] = rt.localScale.y.ToString();
                        dr["localscaleZ"] = rt.localScale.z.ToString();

                        dr["offsetMaxX"] = rt.offsetMax.x.ToString();
                        dr["offsetMinX"] = rt.offsetMin.x.ToString();
                        dr["offsetMaxY"] = rt.offsetMax.y.ToString();
                        dr["offsetMinY"] = rt.offsetMin.y.ToString();
                        dr["anchorMinX"] = rt.anchorMin.x.ToString();
                        dr["anchorMinY"] = rt.anchorMin.y.ToString();
                        dr["anchorMaxX"] = rt.anchorMax.x.ToString();
                        dr["anchorMaxY"] = rt.anchorMax.y.ToString();
                        dr["sizeDeltaX"] = rt.sizeDelta.x.ToString();
                        dr["sizeDeltaY"] = rt.sizeDelta.y.ToString();
                        dr["pivotX"] = rt.pivot.x.ToString();
                        dr["pivotY"] = rt.pivot.y.ToString();

                        _set.Tables["RectTransform"].Rows.Add(dr);
                        break;
                    #endregion
                    default:
                        //Debug.Log("PrefabCreator: " + c.GetType().FullName + " is unhandled.");
                        break;
                }
            }

            return parseChildren;
        }

        public static DataSet CreateDefaultDataSet(string prefabname)
        {
            DataSet set = new DataSet();
            set.DataSetName = prefabname;

            DataTable dt = new DataTable("Config");
            DataColumn dc = new DataColumn("Type", typeof(string));
            dc.DefaultValue = false; dt.Columns.Add(dc);
            dc = new DataColumn("Name", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);

            dt = new DataTable("GameObjects");
            dc = new DataColumn("Name", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("childof", typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn("instanceid", typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn("hasType", typeof(string));
            dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);

            #region RectTransform
            dt = new DataTable("RectTransform");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("anchoredpositionX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("anchoredpositionY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("xMin", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("xMax", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("yMin", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("yMax", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("localpositionX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("localpositionY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("localpositionZ", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("localscaleX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("localscaleY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("localscaleZ", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("offsetMaxX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("offsetMaxY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("offsetMinX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("offsetMinY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("anchorMinX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("anchorMinY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("anchorMaxX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("anchorMaxY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("sizeDeltaX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("sizeDeltaY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("sizeDeltaMagnitude", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("sizeDeltaSqrMagnitude", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("pivotX", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("pivotY", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Button
            dt = new DataTable("Button");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("Sprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorA", typeof(string)); dt.Columns.Add(dc);
            //dc = new DataColumn("RaycastTarget", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Maskable", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("PreserveAspect", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("TextValue", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSize", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSizeMin", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSizeMax", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("AutoSize", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorA", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("alignHorizontal", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("alignVertical", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("Bold", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Italic", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Underline", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Strike", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Lowercase", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Uppercase", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("SmallCaps", typeof(bool)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region ButtonAnimated
            dt = new DataTable("ButtonAnimated");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("SpriteIndices", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Sprites", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("CycleTime", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("DoAnimate", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("StartFrame", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("Sprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorA", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Maskable", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("PreserveAspect", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("TextValue", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSize", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSizeMin", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSizeMax", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("AutoSize", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorA", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("alignHorizontal", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("alignVertical", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("Bold", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Italic", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Underline", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Strike", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Lowercase", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Uppercase", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("SmallCaps", typeof(bool)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Image
            dt = new DataTable("Image");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("Sprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorA", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("RaycastTarget", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Maskable", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("PreserveAspect", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("AlphaThreshold", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("fillAmount", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("fillCenter", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("fillClockwise", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("fillMethod", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("type", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region ImageAnimated
            dt = new DataTable("ImageAnimated");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("SpriteIndices", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Sprites", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("CycleTime", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("DoAnimate", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("StartFrame", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("Sprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorA", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("RaycastTarget", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Maskable", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("PreserveAspect", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("AlphaThreshold", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("fillAmount", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("fillCenter", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("fillClockwise", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("fillMethod", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("type", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Canvas
            dt = new DataTable("Canvas");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Dropdown
            dt = new DataTable("Dropdown");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region InputField
            dt = new DataTable("InputField");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Panel
            dt = new DataTable("Panel");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("draggable", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);

            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region RawImage
            dt = new DataTable("RawImage");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Scrollview
            dt = new DataTable("Scrollview");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);

            dc = new DataColumn("ContentPrefab", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("MovementType", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Inertia", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("DecelerationRate", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("ScrollSensitivity", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("showScrollbarHorizontal", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("showScrollbarVertical", typeof(bool)); dt.Columns.Add(dc);

            dc = new DataColumn("sizeFitterHorizontal", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("sizeFitterVertical", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("layoutPaddingLeft", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutPaddingRight", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutPaddingTop", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutPaddingBottom", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutSpacing", typeof(float)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutReverseArrangement", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutControlHeight", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutControlWidth", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutUseChildScaleHeight", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutUseChildScaleWidth", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutForceExpandHeight", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutForceExpandWidth", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("layoutChildAlignment", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("Sprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("ColorA", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("HorizontalSprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalColorA", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalHandleSprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalHandleColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalHandleColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalHandleColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("HorizontalHandleColorA", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("VerticalSprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalColorA", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalHandleSprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalHandleColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalHandleColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalHandleColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VerticalHandleColorA", typeof(string)); dt.Columns.Add(dc);

            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Slider
            dt = new DataTable("Slider");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Text
            dt = new DataTable("Text");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("TextValue", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSize", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSizeMin", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("FontSizeMax", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("AutoSize", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorR", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorG", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorB", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TextColorA", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("alignHorizontal", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("alignVertical", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("Bold", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Italic", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Underline", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Strike", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Lowercase", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("Uppercase", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("SmallCaps", typeof(bool)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Toggle
            dt = new DataTable("Toggle");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("ChildOf", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("enabled", typeof(bool)); dt.Columns.Add(dc);
            dc = new DataColumn("name", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("OnToggleChanged", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerEnterHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerExitHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleLeftClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerDoubleRightClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnPointerMiddleClickHandler", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollUp", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dc = new DataColumn("OnScrollDown", typeof(string)); dc.DefaultValue = string.Empty; dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            #region Sprites Table
            dt = new DataTable("SpritesTable");
            dc = new DataColumn("OwnerInstanceID", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("Sprite", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Name", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt);
            #endregion

            set.AcceptChanges();

            return set;
        }
    }
}



