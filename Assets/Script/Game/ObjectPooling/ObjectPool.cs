using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Script.Game.ObjectPooling
{
    [CreateAssetMenu(fileName = "Object Pool", menuName = "Scriptable Objects/Object Pool", order = 0)]
    public class ObjectPool : ScriptableObject
    {
        [SerializeField] private GameObject objectPrefab;

        public event Action OnEnableObject;

        private Queue<IPooleable> _objectPool;
        private Transform _parentTransform;

        public void SetUp(Transform parent)
        {
            if (_objectPool == null)
            {
                _objectPool = new Queue<IPooleable>();
            }

            _objectPool.Clear();
            _parentTransform = parent;
        }

        public IPooleable GetObject()
        {
            IPooleable objectInstance;

            if (_objectPool.Count > 0)
            {
                objectInstance = _objectPool.Dequeue();
                objectInstance.ActivateObject();
                OnEnableObject?.Invoke();
            }
            else
            {
                objectInstance = Instantiate(objectPrefab).GetComponent<IPooleable>();
                objectInstance.Instantiate(this, _parentTransform);
                OnEnableObject?.Invoke();
            }

            return objectInstance;
        }

        public void ObjectReturn(IPooleable objectInstance)
        {
            objectInstance.DeactivateObject();
            _objectPool.Enqueue(objectInstance);
        }
    }

    public class Proyectile : MonoBehaviour, IPooleable
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private Vector2 direction;
        [SerializeField] private string objectiveTag;

        private ObjectPool _pool;
        private Transform _parent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(objectiveTag) || collision.CompareTag("Collector"))
            {
                _pool.ObjectReturn(this);
            }
        }

        public void ActivateObject()
        {
            transform.position = _parent.position;

            rb.velocity = direction * speed;
        }

        public void DeactivateObject()
        {
            rb.velocity = Vector2.zero;
        }

        public void Instantiate(ObjectPool poolReference, Transform parent)
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
            }

            _pool = poolReference;
            _parent = parent;

            ActivateObject();
        }
    }

    public interface IPooleable
    {
        void Instantiate(ObjectPool poolReference, Transform parent);

        void ActivateObject();

        void DeactivateObject();
    }
}