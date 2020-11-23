using System;
using UnityEngine;
using Zenject;

namespace FizerFox
{
    public abstract class Mediator<T> : MonoBehaviour, IDisposable
           where T : MonoBehaviour
    {
        protected T View;
        protected bool Registered;
        protected bool Unregistered;

        private bool _filled;
        private bool _started;

        [Inject]
        private void PostInject(DisposableManager disposableManager)
        {
            _filled = true;
            if (_started)
            {
                OnRegister();
                Registered = true;
            }
            disposableManager.Add(this);
        }

        protected virtual void Awake()
        {
            View = GetComponent<T>();
        }

        private void Start()
        {
            _started = true;
            if (_filled)
            {
                OnRegister();
                Registered = true;
            }
        }

        private void OnDestroy()
        {
            if (Registered && !Unregistered)
            {
                OnRemove();
                Unregistered = true;
            }
        }

        public virtual void OnRegister() { }

        public virtual void OnRemove() { }

        public virtual void OnEnable() { }

        public virtual void OnDisable() { }

        public void Dispose()
        {
            if (Registered && !Unregistered)
            {
                OnRemove();
                Unregistered = true;
            }
        }
    }
}