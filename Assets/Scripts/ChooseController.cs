using UnityEngine;
using TMPro;

public class ChooseController : MonoBehaviour
{
    public ChooseLabelController Label;
    public GameController GameController;

    private RectTransform _rectTransform;
    private Animator _animator;
    private float labelHeight = -1;

    private void Start() 
    {
        _animator = GetComponent<Animator>();
        _rectTransform = GetComponent<RectTransform>();    
    }

    public void SetupChoose(ChooseScene scene)
    {
        DestroyLabels();
        _animator.SetTrigger("Show");
        
        for (int index = 0; index < scene.Labels.Count; index++)
        {
            ChooseLabelController newLabel = Instantiate(Label.gameObject, transform).GetComponent<ChooseLabelController>();

            if (labelHeight == -1)
            {
                labelHeight = newLabel.GetHeight();
            }

            newLabel.Setup(scene.Labels[index], this, CalculateLabelPosition(index, scene.Labels.Count));
        }

        Vector2 size = _rectTransform.sizeDelta;
        size.y = (scene.Labels.Count + 2) * labelHeight;
        _rectTransform.sizeDelta = size;
    }

    public void PerformChoose(StoryScene scene)
    {
        GameController.PlayScene(scene);
        _animator.SetTrigger("Hide");
    }

    private float CalculateLabelPosition(int labelIndex, int labelCount)
    {
        if (labelCount %2 == 0)
        {
            if (labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex - 1) + labelHeight / 2;
            }
            else
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2) + labelHeight / 2);
            }
        }
        else
        {
            if (labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex);
            }
            else if (labelIndex > labelCount / 2)
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2));
            }
            else 
            {
                return 0;
            }
        }
    }

    private void DestroyLabels()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
    }

    
}
