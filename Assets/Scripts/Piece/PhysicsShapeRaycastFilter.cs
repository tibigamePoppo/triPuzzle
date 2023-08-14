using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sprite��PhysicsShape�̓����̂ݓ����蔻���L���ɂ���RaycastFilter
/// </summary>
[RequireComponent(typeof(Image))]
public class PhysicsShapeRaycastFilter : MonoBehaviour, ICanvasRaycastFilter //
{
    private readonly List<Vector2> _verts = new List<Vector2>();

    private Image _image;

    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
        var image = _image;
        var rectTransform = transform as RectTransform;
        Vector2 local;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out local))
        {
            return false;
        }

        var rect = rectTransform.rect;

        // �X�v���C�g���̍��W��Ԃł̈ʒu���v�Z����
        var pivot = rectTransform.pivot;
        var sprite = image.sprite;
        var x = (local.x / rect.width + pivot.x - 0.5f) * sprite.rect.width / sprite.pixelsPerUnit;
        var y = (local.y / rect.height + pivot.y - 0.5f) * sprite.rect.height / sprite.pixelsPerUnit;
        var p = new Vector2(x, y);

        var physicsShapeCount = sprite.GetPhysicsShapeCount();
        for (var i = 0; i < physicsShapeCount; i++)
        {
            sprite.GetPhysicsShape(i, _verts);
            if (IsInPolygon(_verts, p))
            {
                // �ǂꂩ�̑��p�`�̓����ɂ����true��Ԃ�
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// ��ʑ��p�`�̓����ɓ_�����݂��邩�ǂ���
    /// </summary>
    private static bool IsInPolygon(List<Vector2> polygon, Vector2 p)
    {
        // p����x���̐������ւ̖����Ȕ��������l���āA���p�`�Ƃ̌����񐔂ɂ���Ĕ��肷��
        var n = polygon.Count;
        var isIn = false;
        for (var i = 0; i < n; i++)
        {
            var nxt = (i + 1);
            if (nxt >= n) nxt = 0;
            var a = polygon[i] - p;
            var b = polygon[nxt] - p;
            if (a.y > b.y)
            {
                // swap
                var t = a;
                a = b;
                b = t;
            }

            if (a.y <= 0 && 0 < b.y && CrossProduct(a, b) > 0)
            {
                isIn = !isIn;
            }
        }

        return isIn;
    }

    /// <summary>
    /// �O��
    /// </summary>
    private static float CrossProduct(Vector2 u, Vector2 v)
    {
        return u.x * v.y - u.y * v.x;
    }
}