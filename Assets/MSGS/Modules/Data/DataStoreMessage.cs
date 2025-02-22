using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataStoreMessage
    {
        readonly DataStoreMessagePool _parent;

        /// <summary>
        /// 
        /// </summary>
        public DataStoreMessageType MsgType = DataStoreMessageType.None;
        /// <summary>
        /// 
        /// </summary>
        public string TypeName;

        public Guid PrimaryKey;

        public string Attribute;

        public Value value;

        public void Release()
        {
            _parent.Return(this);
        }

        public DataStoreMessage(DataStoreMessagePool parent)
        {
            _parent = parent;
        }        
    }

    public enum DataStoreMessageType
    {
        None,

        //its CRUD!
        Create,
        Read,
        Update,
        Delete,
    }
}

