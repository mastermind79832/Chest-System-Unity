using ChestSystem.Chest;
using ChestSystem.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField]
        private SlotController[] m_Slots;

        private SlotController m_CurrentUnlocking;
		private Queue<SlotController> m_freeSlots;

		private ModalWindow window;
		public ItemManager ItemManager { get; private set; }

		private void Start()
		{
			window = UIService.Instance.ModalWindow;
			ItemManager = GetComponent<ItemManager>();
			m_freeSlots = new Queue<SlotController>();

		}

		public void SetUnlocking(SlotController slot) => m_CurrentUnlocking = slot;
		public bool IsAlreadyUnlocking()
		{
			if (m_CurrentUnlocking == null)
				return false;

			window.ShowConfirmation("OCCUPIED", "Something is Currently unlocking\nTry to free the slot","Unlock Now",m_CurrentUnlocking.QuickUnlock,"Later",null);
			return true;
		}

		public bool IsSlotAvailabile()
		{
			if(m_freeSlots.Count > 0)
				return true;

			window.ShowMessage("OOPS!", "You dont have any free slot\nGo unlock Chest to free slots", "On It!");
			return false;
		}

		public void AddChest(ChestTypeSO chestType)
		{
			window.ShowMessage("New Chest",
				$"You Have gotten {chestType.name} Chest",
				chestType.BottomSprite,
				chestType.TopSprite,
				$"{chestType.CoinRange.Min}-{chestType.CoinRange.Max}",
				$"{chestType.GemRange.Min}-{chestType.GemRange.Max}", 
				"YES!");
			SlotController freeSlot = m_freeSlots.Dequeue();
			freeSlot.SetChest(chestType);
		}

		public void FreeSlot(SlotController slot)
		{
			m_freeSlots.Enqueue(slot);
		}
	}
}
