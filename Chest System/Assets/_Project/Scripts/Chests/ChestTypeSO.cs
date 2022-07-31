using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
	[System.Serializable]
    public struct RangePacketInt
	{
        public int Min;
        public int Max;
	}

    [CreateAssetMenu(menuName = "Chest/New Chest Type", fileName = "New Chest Type")]
    public class ChestTypeSO : ScriptableObject
    {
        public string ChestName;

		[Header("Goodies")]
        public RangePacketInt CoinRange;
        public RangePacketInt GemRange;

		[Header("Time")]
		[Tooltip("Time needed to unlock in sec")]
        public float UnlockTime;

		[Header("Image")]
        public Sprite TopSprite;
        public Sprite BottomSprite;
    }
}
