using UnityEngine;

[CreateAssetMenu(fileName = "buttonData_NewScene", menuName = "UI/Button Data/New Scene")]
public class ButtonDataNewScene : ButtonData
{
    [SerializeField] private string m_scene;

    public override void Submit(ButtonJose button)
    {
        SceneManager.NewScene(m_scene);
    }
}