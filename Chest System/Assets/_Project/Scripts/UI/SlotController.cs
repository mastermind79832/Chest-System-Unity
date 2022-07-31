using System;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chest;
using TMPro;

namespace ChestSystem.UI
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField]
        private ChestController m_Chest;
        [SerializeField]
        private TextMeshProUGUI m_Time;
        [SerializeField]
        private GameObject m_UnlockButton;

        private ModalWindow window;
		private SlotManager manager;

		private void Start()
		{
            window = UIService.Instance.ModalWindow;
			manager = UIService.Instance.SlotManager;
			m_Chest.OnTimerUpdated += UpdateTime;
			m_Chest.OnChestTimerOver += ReadyForUnlock;
            EmptySlot();
		}

        public void UpdateTime(float value)
		{
			m_Time.text = TimeToString(value);
		}

		public void ReadyForUnlock()
		{
			manager.SetUnlocking(null);
			m_Time.text = "READY";
		}

		private static string TimeToString(float value)
		{
			TimeSpan time = TimeSpan.FromSeconds(value);
			string timeString = time.ToString(@"hh\:mm\:ss");
			return timeString;
		}

		public void Unlock()
		{
            switch(m_Chest.State)
			{
                case ChestState.Unlocking:
					QuickUnlock();
                    break;
				case ChestState.Unlocked:
					OpenChest();
					m_UnlockButton.SetActive(false);
					break;
				default:
					CheckCanUnlock();
					break;
			}
		}

		private void CheckCanUnlock()
		{
			if(!manager.IsAlreadyUnlocking())
			{
				manager.SetUnlocking(this);
				m_Chest.StartUnlocking();
			}
		}

		public void QuickUnlock()
		{
			window.ShowConfirmation("Unlocking Cost", $"Do You Want To unlock now for {(int)m_Chest.RemainingTime} ?","Unlock Now",ConfirmQuickUnlock,"Later",null);
		}
		private void ConfirmQuickUnlock()
		{
			if(!manager.ItemManager.CheckGems((int)m_Chest.RemainingTime))
			{
				window.ShowMessage("OOPS!", "You don't have enough Gems!", "Earn More");
				return;
			}
			manager.ItemManager.AddGem((int)m_Chest.RemainingTime * -1);
			m_Chest.QuickUnlock();
		}

		public void SetChest(ChestTypeSO chestType)
		{
			m_Chest.Initialize(chestType);
			m_Time.text = $"{TimeToString(m_Chest.RemainingTime)}";
			m_UnlockButton.SetActive(true);
		}

		private void OpenChest()
		{
			m_Chest.Open(out int CoinAmount, out int GemAmount);
			window.ShowMessage("OPENED", "Congradulations", CoinAmount.ToString(), GemAmount.ToString(), "Aquire");

			manager.ItemManager.AddCoin(CoinAmount);
			manager.ItemManager.AddGem(GemAmount);
			EmptySlot();
		}
		private void EmptySlot()
		{
            m_Chest.ResetChest();
			manager.SetUnlocking(null);
			manager.FreeSlot(this);
			m_UnlockButton.SetActive(false);
            m_Time.text = "EMPTY";
		}
	}
}
