using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.UI;

namespace ChestSystem.Chest
{
    public enum ChestState
	{
        Locked = 0,
        Unlocking = 1,
        Unlocked = 2,
	}

	public class ChestController : MonoBehaviour
	{
		[Header("ChestVisual")]
		[SerializeField]
		private Image m_ChestTop;
		[SerializeField]
		private Image m_Chestbottom;

        private ChestTypeSO chestModel;

        private float m_Timer;
		public float RemainingTime { get { return m_Timer; } }
		public Action<float> OnTimerUpdated;
		public Action OnChestTimerOver;

        private ChestState m_State;
		public ChestState State { get { return m_State; } }

		private void Start()
		{
			ChangeState(ChestState.Locked);
			m_ChestTop.sprite = null;
			m_Chestbottom.sprite = null;
		}

		public bool IsState(ChestState state) => m_State == state;
		public void Initialize(ChestTypeSO newChestType)
		{
			chestModel = newChestType;
			ChangeState(ChestState.Locked);
			m_Timer = chestModel.UnlockTime;
			InitChest();
		}

		private void InitChest()
		{
			ChestSpriteEnable(true);
			m_ChestTop.sprite = chestModel.TopSprite;
			m_Chestbottom.sprite = chestModel.BottomSprite;
		}

		public void StartUnlocking()
		{
            ChangeState(ChestState.Unlocking);
		}

		private void Update()
		{
			if (m_State == ChestState.Unlocking)
			{
				DecreaseTimer();
				if (IsTimerOver())
				{
					ChangeState(ChestState.Unlocked);
					OnChestTimerOver();
				}
			}
		}

		private void DecreaseTimer()
		{
			m_Timer -= Time.deltaTime;
			OnTimerUpdated(m_Timer);

		}
		private bool IsTimerOver() => m_Timer <= 0;
		private void ChangeState(ChestState state) => m_State = state;
		public void QuickUnlock()
		{
			ChangeState(ChestState.Unlocked);
			OnChestTimerOver();
		}

		public void Open(out int coinAmount, out int gemAmount)
		{
			coinAmount = UnityEngine.Random.Range(chestModel.CoinRange.Min, chestModel.CoinRange.Max);
			gemAmount = UnityEngine.Random.Range(chestModel.GemRange.Min, chestModel.GemRange.Max);
		}

		public void ResetChest()
		{
			chestModel = null;
			ChestSpriteEnable(false);
			ChangeState(ChestState.Locked);
		}

		private void ChestSpriteEnable(bool enable)
		{
			m_ChestTop.enabled = enable;
			m_Chestbottom.enabled = enable;
		}
	}
}
