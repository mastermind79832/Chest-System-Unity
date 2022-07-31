using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ChestSystem.Item
{
    public class ItemManager : MonoBehaviour
    {
        private int m_coins;
        [SerializeField]
        private TextMeshProUGUI m_CoinText;

        private int m_Gems;
        [SerializeField]
        private TextMeshProUGUI m_GemText;

        public void AddCoin(int value)
        {
            m_coins += value;
            m_CoinText.text = value.ToString();
        }

        public void AddGem(int value)
        {
            m_Gems += value;
            m_GemText.text = value.ToString();
        }

        public bool CheckCoins(int value) => m_coins > value;
        public bool CheckGems(int value) => m_Gems > value;

    }
}
