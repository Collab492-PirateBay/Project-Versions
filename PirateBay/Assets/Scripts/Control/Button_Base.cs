using UnityEngine.UI;
using UnityEngine;

public class Button_Base : MonoBehaviour
{
    /// <summary>
    /// Reference to any UI button.
    /// </summary>
    [SerializeField]
    public Button m_anyButton = null;

    public Button m_button;

	public string sceneName;

    /// <summary>
    /// Reference to the audio source attached to any UI button.
    /// </summary>
    [SerializeField]
    private AudioSource
        m_buttonSFX = null;

    [SerializeField]
    public GameManager Game_manager;

    private void Awake()
    {
        m_button = m_anyButton.GetComponent<Button>();

        //m_buttonSFX = GetComponent<AudioSource>();

        m_button.onClick.AddListener(TaskOnClick);

        Game_manager = GameManager.GameManagerInstance;

    }
	
    private void TaskOnClick()
    {
        //m_buttonSFX.Play();
		Game_manager.setCurrentScene(sceneName);
    }

}
