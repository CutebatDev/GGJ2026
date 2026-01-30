using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DestructibleTerrain : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        private ModifiableTexture m_modifiableTexture;

        [SerializeField] private TilemapColliderGenerator m_colliderGenerator;

        private TilemapColliderGenerator m_nonChunkedCollider;

        [SerializeField] private Grid m_grid;

        private Vector2 m_lastClickedWorldPosition = Vector2.zero;
        private void Start()
        {
            m_modifiableTexture = ModifiableTexture.CreateFromSprite(m_spriteRenderer.sprite);
            m_modifiableTexture.SaveFallback();
            m_spriteRenderer.sprite = m_modifiableTexture.Sprite;
            
            float pixelSize = 1 / m_modifiableTexture.Sprite.pixelsPerUnit;
            m_grid.cellSize = new Vector2(pixelSize, pixelSize);

            Vector2 size = m_modifiableTexture.Sprite.bounds.size;
            Vector2 bottomLeftCorner = -size * m_modifiableTexture.Pivot;
            Vector2 bottomLeftWorld = m_spriteRenderer.transform.TransformPoint(bottomLeftCorner);
            m_nonChunkedCollider =
                Instantiate(m_colliderGenerator, bottomLeftWorld, Quaternion.identity, m_grid.transform);
            m_nonChunkedCollider.PrepeareCollider(m_modifiableTexture.GetPixelsState());
        }   

        public void RemoveTerrainAt(Vector2 worldPosition, float radius)
        {
            float pixelSize = 1 / m_modifiableTexture.Sprite.pixelsPerUnit;
            int radiusInPixels = Mathf.RoundToInt(radius / pixelSize);
            List<Vector2Int> affectedPixelsAsOffset = GetCircleOffsets(radiusInPixels);
            
            Vector2Int circleCenterInPixelSpace = m_modifiableTexture.WorldToTexturePosition(m_lastClickedWorldPosition, m_spriteRenderer.transform);
            RestoreTextureAt(circleCenterInPixelSpace, affectedPixelsAsOffset);
            
            circleCenterInPixelSpace = m_modifiableTexture.WorldToTexturePosition(worldPosition, m_spriteRenderer.transform);
            ModifyTextureAt(circleCenterInPixelSpace, Color.clear, affectedPixelsAsOffset);
            
            bool[][] pixelState = m_modifiableTexture.GetPixelsState();
            m_nonChunkedCollider.RestoreCollider(m_lastClickedWorldPosition, affectedPixelsAsOffset, pixelState);
            
            m_nonChunkedCollider.DestroyCollider(worldPosition, affectedPixelsAsOffset);
            
            m_lastClickedWorldPosition = worldPosition;
        }

        private void ModifyTextureAt(Vector2Int circleCenterInPixelSpace, Color color, List<Vector2Int> affectedPixelAsOffset)
        {
            foreach (Vector2Int offset in affectedPixelAsOffset)
            {
                Vector2Int pos = circleCenterInPixelSpace + offset;
                m_modifiableTexture.SetPixel(pos, color);
            }

            m_modifiableTexture.ApplyChanges();
        }
        
        private void RestoreTextureAt(Vector2Int circleCenterInPixelSpace, List<Vector2Int> affectedPixelAsOffset)
        {
            foreach (Vector2Int offset in affectedPixelAsOffset)
            {
                Vector2Int pos = circleCenterInPixelSpace + offset;
                m_modifiableTexture.SetPixel(pos, m_modifiableTexture.GetOriginalPixel(pos));
            }

            m_modifiableTexture.ApplyChanges();
        }

        private List<Vector2Int> GetCircleOffsets(int radiusInPixel)
        {
            List<Vector2Int> affectedPixelAsOffset = new List<Vector2Int>();
            for (int x = -radiusInPixel; x <= radiusInPixel; x++)
            {
                for (int y = -radiusInPixel; y <= radiusInPixel; y++)
                {
                    if (x * x + y * y <= radiusInPixel * radiusInPixel)
                    {
                        affectedPixelAsOffset.Add(new Vector2Int(x, y));
                    }
                }
            }
            return affectedPixelAsOffset;
        }
    }
}