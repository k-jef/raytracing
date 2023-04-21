using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;


namespace RayTracing_TWO
{
    public partial class Form1 : Form
    {
        int BasicProgramID;
        int BasicVertexShader;
        int BasicFragmentShader;
        OpenTK.Vector3 CubeColor;
        OpenTK.Vector3 CameraPosition;
        OpenTK.Vector3 CubeCoord2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        void InitShader()
        {
            BasicProgramID = GL.CreateProgram();
            loadShader("C:\\Users\\demin\\source\\repos\\RayTracing TWO\\raytracing.vert", ShaderType.VertexShader, BasicProgramID,
            out BasicVertexShader);
            loadShader("C:\\Users\\demin\\source\\repos\\RayTracing TWO\\raytracing.frag", ShaderType.FragmentShader, BasicProgramID,
            out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
        }

        private static bool Init()
        {
            GL.Enable(EnableCap.ColorMaterial);
            GL.ShadeModel(ShadingModel.Smooth);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            return true;
        }

        void SetUniformVec3(string name, OpenTK.Vector3 value)
        {
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, name), value);
        }

        private void DrawingShader()
        {
            GL.ClearColor(Color.AliceBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(BasicProgramID);
            SetUniformVec3("cube_color", CubeColor);
            SetUniformVec3("camera_position", CameraPosition);

            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1);
            GL.Vertex2(-1, -1);

            GL.TexCoord2(1, 1);
            GL.Vertex2(1, -1);

            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 1);

            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, 1);

            GL.End();
            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }


       

        private void glControl1_Load(object sender, EventArgs e)
        {
             Init();
             InitShader();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            DrawingShader();
        }
    }
}
