using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.UI;

namespace ChestSystem.Chest
{
    public class ChestSpawner : MonoBehaviour
    {
        [SerializeField]
        private ChestTypeSO[] m_ChestTypes;

		private UIService UIService;

		private void Start()
		{
			UIService = UIService.Instance;
		}

		public void Explore()
		{
			if (UIService.SlotManager.IsSlotAvailabile())
			{
				int index = GetRandomChestIndex();
				UIService.SlotManager.AddChest(m_ChestTypes[index]);
			}
		}

		private int GetRandomChestIndex()
		{
			return Random.Range(0, m_ChestTypes.Length);
		}
	}
}
