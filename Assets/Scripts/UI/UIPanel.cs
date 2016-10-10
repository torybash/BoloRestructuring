using UnityEngine;
using System.Collections;

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

	public void Enable(bool enable)
	{
		gameObject.SetActive(enable);
	}
}
