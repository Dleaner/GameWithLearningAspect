using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChooseLabelController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _hoverColor;

    private StoryScene _scene;
    private TextMeshProUGUI _textMesh;
    private ChooseController _controller;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.color = _defaultColor;
    }

    public float GetHeight()
    {
        return _textMesh.rectTransform.sizeDelta.y * _textMesh.rectTransform.localScale.y;
    }

    public void Setup(ChooseScene.ChooseLabel label, ChooseController controller, float y)
    {
        _scene = label.NextScene;
        _textMesh.text = label.Text;
        this._controller = controller;

        Vector3 position = _textMesh.rectTransform.localPosition;
        position.y = y;
        _textMesh.rectTransform.localPosition = position;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _controller.PerformChoose(_scene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textMesh.color = _hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textMesh.color = _defaultColor;
    }

}
