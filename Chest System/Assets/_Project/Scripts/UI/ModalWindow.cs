using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChestSystem.Core;

namespace ChestSystem.UI
{
    public class ModalWindow : MonoBehaviour
    {
		[SerializeField]
        private GameObject m_ModelPanel;

        [Header("Header")]
        [SerializeField]
        private GameObject m_Header;
        [SerializeField]
        private TextMeshProUGUI t_HeaderText;

        [Header("Content")]
        [SerializeField]
        private GameObject m_Content;
        [SerializeField]
        private TextMeshProUGUI t_ContentText;
        [SerializeField]

		[Header("Coin")]
        private GameObject m_CoinDisplay;
        [SerializeField]
        private TextMeshProUGUI t_CoinAmount;

		[Header("Gem")]
        [SerializeField]
        private GameObject m_GemDisplay;
        [SerializeField]
        private TextMeshProUGUI t_GemAmount;

		[Header("Chest")]
        [SerializeField]
        private GameObject m_Chest;
        [SerializeField]
        private Image m_ChestTop;
        [SerializeField]
        private Image m_ChestBottom;

        [Header("Footer")]
        [SerializeField]
        private GameObject m_Footer;
        [SerializeField]
        private GameObject m_ConfirmButton;
        [SerializeField]
        private TextMeshProUGUI t_ConfirmButtonText;
        [SerializeField]
        private GameObject m_DeclineButton;
        [SerializeField]
        private TextMeshProUGUI t_DeclineButtonText;

        private Action m_OnConfirm;
        private Action m_OnDecline;

        public void Confirm()
		{
            Close();
            m_OnConfirm?.Invoke();
		}
        public void Decline()
		{
            Close();
            m_OnDecline?.Invoke();
		}

        private void Close()
		{
			m_DeclineButton.SetActive(false);
			m_ConfirmButton.SetActive(false);
			m_Footer.SetActive(false);
			m_GemDisplay.SetActive(false);
			m_CoinDisplay.SetActive(false);
			m_Content.SetActive(false);
			m_Header.SetActive(false);
			m_Chest.SetActive(false);
			m_ModelPanel.SetActive(false);
		}

		private void ResetAction()
		{
			m_OnConfirm = null;
			m_OnDecline = null;
		}

		private void SetHeader(string headerText)
		{
            m_ModelPanel.SetActive(true);
			m_Header.SetActive(true);
			t_HeaderText.text = headerText;
		}
		private void SetConfirmButton(string confrirmText, Action onConfirm)
		{
			m_ConfirmButton.SetActive(true);
			t_ConfirmButtonText.text = confrirmText;
			m_OnConfirm = onConfirm;
		}
		private void SetDeclineButton(string declineText, Action onDecline)
		{
			m_DeclineButton.SetActive(true);
			t_DeclineButtonText.text = declineText;
			m_OnDecline = onDecline;
		}
		private void SetChest(Sprite bottom, Sprite top)
		{
			m_Chest.SetActive(true);
			m_ChestBottom.sprite = bottom;
			m_ChestTop.sprite = top;
		}
		private void SetContent(string contentText)
		{
			m_Content.SetActive(true);
			t_ContentText.text = contentText;
		}
		private void SetContent(string contentText, string coinAmount, string GemAmount)
		{
			SetContent(contentText);
			m_CoinDisplay.SetActive(true);
			t_CoinAmount.text = coinAmount;
			m_GemDisplay.SetActive(true);
			t_GemAmount.text = GemAmount;
		}
        private void SetFooter(string confrirmText, Action onConfirm)
        {
            m_Footer.SetActive(true);
            SetConfirmButton(confrirmText, onConfirm);
        }
		private void SetFooter(string confrirmText, Action onConfirm, string declineText, Action onDecline)
		{
			SetFooter(confrirmText, onConfirm);
			SetDeclineButton(declineText, onDecline);
		}

        /// <summary>
        /// To show Confirmation
        /// </summary>
        public void ShowConfirmation(string headerText, string contentText, string confrirmText, Action onConfirm, string declineText, Action onDecline)
		{
			SetHeader(headerText);
			SetContent(contentText);
			SetFooter(confrirmText, onConfirm, declineText, onDecline);
		}

		/// <summary>
		/// To show General Message
		/// </summary>
		public void ShowMessage(string headerText, string contentText, string confrirmText = "Yeay", Action onConfrim = null)
		{
            SetHeader(headerText);
            SetContent(contentText);
            SetFooter(confrirmText, onConfrim);
		}

        /// <summary>
        /// To show Message with items
        /// </summary>
        public void ShowMessage(string headerText, string contentText, string coinAmount, string GemAmount, string confrirmText = "Yeay", Action onConfrim = null)
		{
			SetHeader(headerText);
			SetContent(contentText, coinAmount, GemAmount);
			SetFooter(confrirmText, onConfrim);
		}

        /// <summary>
        /// To show Message with Chest
        /// </summary>
        public void ShowMessage(string headerText, string contentText, Sprite bottom, Sprite top, string coinAmount, string GemAmount, string confrirmText = "Yeay", Action onConfrim = null)
		{
			SetHeader(headerText);
			SetChest(bottom, top);
			SetContent(contentText, coinAmount, GemAmount);
			SetFooter(confrirmText, onConfrim);
		}

	}
}
