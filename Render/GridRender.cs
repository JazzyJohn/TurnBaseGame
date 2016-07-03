using UnityEngine;
using System.Collections;
namespace Grid
{
    public class GridRender : MonoBehaviour
    {


        public float normalBorderHeight = 0.25f;
        public float selectedBorderHeight = 0.5f;
        float borderH;
        static Material gridMaterial;
        static void CreateGridMaterial()
        {
            if (!gridMaterial)
            {
                // Unity has a built-in shader that is useful for drawing
                // simple colored things.
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                gridMaterial = new Material(shader);
                gridMaterial.hideFlags = HideFlags.HideAndDontSave;
                // Turn on alpha blending
                gridMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                gridMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                // Turn backface culling off
                gridMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                // Turn off depth writes
                gridMaterial.SetInt("_ZWrite", 0);
            }
        }

        // Will be called after all regular rendering is done
        public void OnRenderObject()
        {
            CreateGridMaterial();
            // Apply the line material
            gridMaterial.SetPass(0);

            GL.PushMatrix();
            // Set transformation matrix for drawing to
            // match our transform
            GL.MultMatrix(transform.localToWorldMatrix);

            // Draw lines
            GL.Begin(GL.TRIANGLES);
            GridController controller = GridController.GetInstance();
            int i = 0;
            int j = 0;
            float cellSize = controller.cellSize;
            for (; controller.IsExist(i,0,0); ++i)
            {
                for(j = 0; controller.IsExist(i,j,0); ++j)
                {
                    if (controller[i][j][0].selected)
                    {
                        borderH = selectedBorderHeight;
                        GL.Color(new Color(0, 0, 0.8f, 0.8F));
                        DrawBorder(i * cellSize, j * cellSize, i * cellSize, (j + 1) * cellSize);
                        DrawBorder(i * cellSize, j * cellSize, (i + 1) * cellSize, j * cellSize);
                        DrawBorder(i * cellSize, (j + 1) * cellSize, (i + 1) * cellSize, (j + 1) * cellSize);

                        DrawBorder((i+1) * controller.cellSize, j  * controller.cellSize, (i+1) * controller.cellSize, (j +1)* controller.cellSize);
                    }
                     
                    borderH = normalBorderHeight;
                    GL.Color(new Color(0, 0, 0.6f, 0.8F));
                    DrawBorder(i * cellSize, j * cellSize, i * cellSize, (j + 1) * cellSize);
                    DrawBorder(i * cellSize, j * cellSize, (i + 1) * cellSize, j * cellSize);

                
                    if (i > 110 || j > 110)
                    {
                        Debug.Log("Infinite-");
                        return;
                    }
                }
                DrawBorder(i * cellSize, j * cellSize, (i+1) * cellSize, j * cellSize);
                
            }

            for (; j > 0;j-- )
            {
                DrawBorder(i * controller.cellSize, (j-1) * controller.cellSize, i * controller.cellSize, j  * controller.cellSize);
            }
            GL.End();
            GL.PopMatrix();
        }
        void DrawBorder(float xStart, float yStart, float xFinish, float yFinish)
        {
       
            GL.Vertex3(xStart, 0 , yStart);
            GL.Vertex3(xFinish, 0, yFinish);
            GL.Vertex3(xStart, borderH , yStart);
            GL.Vertex3(xStart, borderH, yStart);
            GL.Vertex3(xFinish, 0, yFinish);
            GL.Vertex3(xFinish, borderH, yFinish);
        }
    }
}