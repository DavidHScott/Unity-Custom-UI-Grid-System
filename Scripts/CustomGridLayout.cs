using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGridLayout : LayoutGroup
{

    public enum FitType
    {
        Auto,
        FixedRows,
        FixedColumns
    }

    public FitType fitType;
    public bool scrollable;

    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 margin;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float cellWidth = 0;
        float cellHeight = 0;

        if (fitType == FitType.Auto)
        {
            float squareRoot = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(squareRoot);
            columns = Mathf.CeilToInt(squareRoot);
        }
        if (fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / rows);
        }
        if (fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / columns);
        }


        if (scrollable == false)
        {
            cellWidth = (rectTransform.rect.width / columns) - ((margin.x / columns) * (columns - 1)) - (padding.left / columns) - (padding.right / columns);
            cellHeight = (rectTransform.rect.height / rows) - ((margin.y / rows) * 2) - (padding.top / rows) - (padding.bottom / rows);
        }
        else
        {
            Vector2 parentSize = new Vector2();

            parentSize.x = (columns * cellSize.x) + padding.horizontal + (margin.x * (columns - 1));
            parentSize.y = (rows * cellSize.y) + padding.vertical + (margin.y * (rows - 1));

            rectTransform.sizeDelta = parentSize;
        }

        cellSize.x = !scrollable ? cellWidth : cellSize.x;
        cellSize.y = !scrollable ? cellHeight : cellSize.y;


        int rowCount = 0;
        int columnCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var gridElement = rectChildren[i];

            float xPosition = (cellSize.x * columnCount) + padding.left + (margin.x * columnCount);
            float yPosition = (cellSize.y * rowCount) + padding.top + (margin.y * rowCount);

            SetChildAlongAxis(gridElement, 0, xPosition, cellSize.x);
            SetChildAlongAxis(gridElement, 1, yPosition, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
