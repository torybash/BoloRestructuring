using UnityEngine;
using System.Collections;

namespace Bolo.Player
{
	[RequireComponent(typeof(CanvasGroup))]
	public class UIPanel : MonoBehaviour
	{

		private CanvasGroup _canvasGroup;
		private CanvasGroup CanvasGroup
		{
			get
			{
				if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
				return _canvasGroup;
			}
		}

		private bool _isEnabled = false;

		public bool IsEnabled { get { return _isEnabled; } }

		public void Enable(bool enable)
		{
			_isEnabled = enable;

			CanvasGroup.alpha = enable ? 1 : 0;
			CanvasGroup.blocksRaycasts = enable;
			CanvasGroup.interactable = enable;

			if (enable) Opening();
			else Closing();
		}

		protected virtual void Opening()
		{

		}

		protected virtual void Closing()
		{

		}
	}
}