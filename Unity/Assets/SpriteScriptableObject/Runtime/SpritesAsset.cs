
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coo.Client
{
    [Serializable]
    public class SpriteItem
    {
        /// <summary>
        /// 精灵名称
        /// </summary>
        public string SpriteName;
 
        /// <summary>
        /// 精灵
        /// </summary>
        public Sprite Sprite;
    }
    public class SpritesAsset : ScriptableObject
    {
        /// <summary>
        /// 精灵数组
        /// </summary>
        [SerializeField] public List<SpriteItem> m_SpritesAssets = new List<SpriteItem>();
    }
}