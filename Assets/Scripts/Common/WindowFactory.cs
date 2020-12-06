using System;
using UnityEngine;
using Zenject;

namespace FizerFox
{
	public class WindowFactory : IFactory<Type, BaseWindow>
	{
		private BaseWindow[] _prefabs;
		private DiContainer _container;

		public WindowFactory(BaseWindow[] prefabs, DiContainer container)
		{
			_prefabs = prefabs;
			_container = container;
		}

		public BaseWindow Create(Type windowType)
		{
			foreach (var prefab in _prefabs)
			{
				if (prefab?.GetType() == windowType)
					return _container.InstantiatePrefabForComponent<BaseWindow>(prefab);
				if (prefab == null)
					Debug.LogError("Empty element in windows prefabs list");
			}

			Debug.LogErrorFormat("Unable to get window with type {0}", windowType.GetType().Name);
			throw new Exception("Unknown window type");
		}
	}
}