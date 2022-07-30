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

        private ChestSO chestModel;

		private float m_PrevTime; // used to store the time just before decreasing the timer
        private float m_Timer;
		public float RemainingTime { get { return m_Timer; } }
		public Action<float> OnTimerUpdated;

        private ChestState m_State;
		public ChestState State { get { return m_State; } }

		private void Start()
		{
			ChangeState(ChestState.Locked);
			m_ChestTop.sprite = null;
			m_Chestbottom.sprite = null;
		}

		public bool IsState(ChestState state) => m_State == state;

		public void Initialize(ChestSO newChestType)
		{
			chestModel = newChestType;
			ChangeState(ChestState.Locked);
			m_Timer = chestModel.UnlockTime;
			m_PrevTime = m_Timer;
			InitChest();
		}

		private void InitChest()
		{
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
					ChangeState(ChestState.Unlocked);
			}
		}

		private void DecreaseTimer()
		{
			m_Timer -= Time.deltaTime;
			
			if((m_Timer - m_PrevTime) > 1)
			{
				m_PrevTime = m_Timer;
				OnTimerUpdated(m_Timer);
			}
		}
		private bool IsTimerOver() => m_Timer <= 0;
		private void ChangeState(ChestState state) => m_State = state;
		public void QuickUnlock() => ChangeState(ChestState.Unlocked);

		public void Open(out int coinAmount, out int gemAmount)
		{
			coinAmount = UnityEngine.Random.Range(chestModel.CoinRange.Min, chestModel.CoinRange.Max);
			gemAmount = UnityEngine.Random.Range(chestModel.GemRange.Min, chestModel.GemRange.Max);
		}

		public void ResetChest()
		{
			chestModel = null;
			m_ChestTop.sprite = null;
			m_Chestbottom.sprite = null;
		}
	}
}
