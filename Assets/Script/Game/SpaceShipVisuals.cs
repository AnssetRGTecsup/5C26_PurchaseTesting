using UnityEngine;

namespace Assets.Script.Game
{
    public class SpaceShipVisuals : MonoBehaviour
    {
        [SerializeField] private SpaceShipData spaceShipData;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetUpData(SpaceShipData newData)
        {
            spaceShipData = newData;

            spriteRenderer.sprite = spaceShipData.Sprite;
            spriteRenderer.color = spaceShipData.Color;
        }
    }
}