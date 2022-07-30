using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Core
{
    public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>
    {
		private static T instance;
		public static T Instance { get { return instance; }}

		public virtual void Awake()
		{
			if (instance != null)
				Destroy(instance);
			else
				instance = (T)this;
		}
	}
}
