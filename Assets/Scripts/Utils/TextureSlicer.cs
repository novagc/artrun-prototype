using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TextureSlicer
{
    public static Texture2D[][] Slice(Texture2D texture, int rowCount, int columnCount, CellTextureSettings settings)
    {
        var r = Enumerable.Range(0, rowCount).Select(x => new Texture2D[columnCount]).ToArray();
        
        
        var cellHeight = texture.height / rowCount;
        var cellWidth = texture.width / columnCount;
        
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                r[i][j] = new Texture2D(cellWidth, cellHeight)
                {
                    filterMode = FilterMode.Bilinear
                };

                r[i][j]
                    .CopySegmentReverse(texture, j * cellWidth, i * cellHeight, cellWidth, cellHeight)
                    .DrawRows(settings.BorderSize, settings.BorderColor)
                    .DrawColumns(settings.BorderSize, settings.BorderColor)
                    .Apply();
            }
        }

        return r;
    }

    private static Texture2D CopySegmentReverse(this Texture2D tex, Texture2D source, int x, int y, int width, int height)
    {
        var clrs = source.GetPixels(x, y, width, height).Reverse().ToArray();
        tex.SetPixels(clrs);
        return tex;
    }

    private static Texture2D DrawRows(this Texture2D tex, int rowHeight, Color color)
    {
        if (rowHeight > 0)
        {

            var colors = Enumerable.Repeat(color, tex.width * rowHeight).ToArray();

            tex.SetPixels(0, 0,
                tex.width, rowHeight,
                colors);

            tex.SetPixels(0, tex.height - rowHeight,
                tex.width, rowHeight,
                colors);
        }

        return tex;
    }
    
    private static Texture2D DrawColumns(this Texture2D tex, int columnWidth, Color color)
    {
        if (columnWidth > 0)
        {
            var colors = Enumerable.Repeat(color, tex.height * columnWidth).ToArray();
        
            tex.SetPixels(0, 0, 
                columnWidth, tex.height, 
                colors);
        
            tex.SetPixels(tex.height - columnWidth, 0, 
                columnWidth, tex.height, 
                colors);
        }
        
        return tex;
    }
}
