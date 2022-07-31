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
            EmptySlot();
		}

        public void UpdateTime(float value)
		{
			m_Time.text = TimeToString(value);
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
			window.ShowConfirmation("Unlocking Cost", $"Do You Want To unlock now for {m_Chest.RemainingTime} ?","Unlock Now",ConfirmQuickUnlock,"Later",null);
		}
		private void ConfirmQuickUnlock()
		{
			// Check if gems are available
			// if not
			window.ShowMessage("OOPS!", "You don't have enough Gems!", "Earn More");

			// if awailable
			// reduce gems
			// Open Chest
		}

		public void SetChest(ChestTypeSO chestType)
		{
			m_Chest.Initialize(chestType);
			m_Time.text = $"{TimeToString(m_Chest.RemainingTime)}";
		}

		private void OpenChest()
		{
			m_Chest.Open(out int CoinAmount, out int GemAmount);
			window.ShowMessage("OPENED", "Congradulations", CoinAmount.ToString(), GemAmount.ToString(), "Aquire");

			//Add items to their storage
			EmptySlot();
		}
		private void EmptySlot()
		{
            m_Time.text = "EMPTY";
            m_Chest.ResetChest();
			manager.SetUnlocking(null);
			manager.freeSlot(this);
		}
	}
}
