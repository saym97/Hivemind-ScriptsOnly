using UnityEngine;

[CreateAssetMenu(fileName = "buttonData_SwapScene", menuName = "UI/Button Data/Swap Scene")]
public class ButtonDataSwapScene : ButtonData
{
    [SerializeField] private string m_scene; 

    public override void Submit(ButtonJose button)
    {
        SceneManager.SwapAdditiveScene(button.gameObject.scene.name, m_scene);
    }
}