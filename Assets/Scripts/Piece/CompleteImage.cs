using UnityEngine;
using UnityEngine.UI;

public class CompleteImage : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private void Awake()
    {
        image.color = new Color(1, 1, 1, 0);
    }

    public void ShowImage()
    {
        image.color = new Color(1, 1, 1, 1);
        int childCout = gameObject.transform.parent.transform.parent.childCount;
        image.transform.parent = gameObject.transform.parent.transform.parent;
        image.transform.SetSiblingIndex(childCout-1);
    }
}
