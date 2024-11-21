using UnityEngine;

namespace Assets.Script.Game
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private SpaceShipData spaceShipData;

        [SerializeField] private float xModifier = .1f;
        [SerializeField] private float yModifier = .25f;

        [SerializeField] private Material material;

        private float xOffSet = 0f;
        private float yOffSet = 0f;

        public void SetUp(SpaceShipData newData)
        {
            spaceShipData = newData;
            yModifier *= spaceShipData.Speed;
        }

        private void Update()
        {
            xOffSet = Time.time * xModifier;
            yOffSet = Time.time * yModifier;
            material.SetTextureOffset("_MainTex", new Vector2(xOffSet, yOffSet));
        }
    }
}