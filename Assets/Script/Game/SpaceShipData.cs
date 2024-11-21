using UnityEditor;
using UnityEngine;

namespace Assets.Script.Game
{
    [CreateAssetMenu(fileName = "SpaceShip Data", menuName = "Scriptable Objects/SpaceShip Data")]
    public class SpaceShipData : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;

        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float maxVelocity;
        [SerializeField] private int maxHP;
        [SerializeField] private int recoveryHP;
        
        public Sprite Sprite => sprite;
        public Color Color => color;
        public float Speed => speed;
        public float Acceleration => acceleration;
        public float MaxVelocity => maxVelocity;
        public int MaxHP => maxHP;
        public int MaxRecoveryHP => recoveryHP;
    }
}