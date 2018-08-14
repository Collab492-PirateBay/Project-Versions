/* WAVES : ANIMATE MATERIAL OFFSET */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour 
{
    public int materialIndex = 0;

    [SerializeField] private float m_TextureSpeedX = 0.0f;
    [SerializeField] private float m_TextureSpeedY = 0.0f;

    public Vector2 m_TextureOffsetSpeed;
    public string textureName = "_MainTex";

    Vector2 textureOffset = Vector2.zero;

    public Renderer m_WaveRenderer;

	void Start () 
    {
        m_WaveRenderer = GetComponent<Renderer>();

        m_TextureOffsetSpeed = new Vector2(m_TextureSpeedX, m_TextureSpeedY);
	}
	
	void Update () 
    {
        textureOffset += (m_TextureOffsetSpeed * Time.deltaTime);
        if (m_WaveRenderer.enabled)
        {
            m_WaveRenderer.materials[materialIndex].SetTextureOffset(textureName, textureOffset);
        }
	}
}
