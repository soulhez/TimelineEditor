/*
	Author:			小何
	CreateDate:		2019-06-13 19:43:53
	Desc:			MonoBehaviour脚本类.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace CutcenesEditor
{
    /// <summary>
    /// 在像素级别上进行图片处理的类，用于图片Pixels级别的处理
    /// </summary>
	public class ProcessPixels : MonoBehaviour 
	{
        [SerializeField]
        Texture2D texture;

		// Use this for initialization
		void Start () 
		{

		}
	
		// Update is called once per frame
		void Update () 
		{
			
		}

        private void OnGUI()
        {
            if(GUILayout.Button("双倍放大"))
            {
                StartCoroutine(ScalePixels());
            }
            if(GUILayout.Button("水平翻转"))
            {

            }
            GUILayout.Box(texture);
        }

        IEnumerator ScalePixels()
        {
            yield return new WaitForEndOfFrame();
        }

        IEnumerator ScalePixels(float factor)
        {
            yield return new WaitForEndOfFrame();
            //Rect rect = new Rect(0, 0, texture.width * 2, texture.height * 2);
            //Color32[] color = Te
            //Texture2D tex = new Texture2D(texture.width * 2, texture.height * 2);
            //tex.ReadPixels(rect, 0, 0, false);
            //tex.Apply();
            //byte[] readByteArrary = tex.EncodeToPNG(texture.GetPixels32())
        }
        /*
        /// <summary>
        /// 水平翻转
        /// </summary>
        /// <param name="orig"></param>
        /// <returns></returns>
        */
        //Texture2D FlipTexture(Texture2D orig)
        //{
        //    Texture2D flip = new Texture2D(orig.width, orig.height);
        //    int xN = orig.width;
        //    int yN = orig.height;

        //    for (int i = 0; i < xN; i++)
        //    {
        //        for (int j = 0; j < yN; j++)
        //        {
        //            flip.SetPixel(xN - i - 1, j, orig.GetPixel(i, j));
        //        }
        //    }

        //    flip.Apply();
        //    return flip;
        //}

        /// <summary>
        /// 垂直翻转
        /// </summary>
        /// <param name="orig"></param>
        /// <returns></returns>
        Texture2D FlipTexture(Texture2D orig)
        {
            Texture2D flip = new Texture2D(orig.width, orig.height);
            int xN = orig.width;
            int yN = orig.height;

            for (int i = 0; i < xN; i++)
            {
                for (int j = 0; j < yN; j++)
                {
                    flip.SetPixel(i, yN - j - 1, orig.GetPixel(i, j));
                }
            }

            flip.Apply();
            return flip;
        }
    }

    internal class TextureScale : MonoBehaviour
    {
        public class ThreadData
        {
            public int start;
            public int end;
            public ThreadData(int s, int e)
            {
                start = s;
                end = e;
            }
        }

        private static Color[] texColors;
        private static Color[] newColors;
        private static int w;
        private static float ratioX;
        private static float ratioY;
        private static int w2;
        private static int finishCount;
        private static Mutex mutex;

        public static Texture2D Point(Texture2D tex, int newWidth, int newHeight, bool flipH, bool flipV)
        {
            return ThreadedScale(tex, newWidth, newHeight, false, flipH, flipV);
        }

        public static Texture2D Bilinear(Texture2D tex, int newWidth, int newHeight, bool flipH, bool flipV)
        {
            return ThreadedScale(tex, newWidth, newHeight, true, flipH, flipV);
        }

        private static Texture2D ThreadedScale(Texture2D tex, int newWidth, int newHeight, bool useBilinear, bool flipH, bool flipV)
        {
            texColors = tex.GetPixels();
            newColors = new Color[newWidth * newHeight];
            if (useBilinear)
            {
                ratioX = 1.0f / ((float)newWidth / (tex.width - 1));
                ratioY = 1.0f / ((float)newHeight / (tex.height - 1));
            }
            else
            {
                ratioX = ((float)tex.width) / newWidth;
                ratioY = ((float)tex.height) / newHeight;
            }
            w = tex.width;
            w2 = newWidth;
            var cores = Mathf.Min(SystemInfo.processorCount, newHeight);
            var slice = newHeight / cores;

            finishCount = 0;
            if (mutex == null)
            {
                mutex = new Mutex(false);
            }
            if (cores > 1)
            {
                int i = 0;
                ThreadData threadData;
                for (i = 0; i < cores - 1; i++)
                {
                    threadData = new ThreadData(slice * i, slice * (i + 1));
                    ParameterizedThreadStart ts = useBilinear ? new ParameterizedThreadStart(BilinearScale) : new ParameterizedThreadStart(PointScale);
                    Thread thread = new Thread(ts);
                    thread.Start(threadData);
                }
                threadData = new ThreadData(slice * i, newHeight);
                if (useBilinear)
                {
                    BilinearScale(threadData);
                }
                else
                {
                    PointScale(threadData);
                }
                while (finishCount < cores)
                {
                    Thread.Sleep(1);
                }
            }
            else
            {
                ThreadData threadData = new ThreadData(0, newHeight);
                if (useBilinear)
                {
                    BilinearScale(threadData);
                }
                else
                {
                    PointScale(threadData);
                }
            }




            tex.Resize(newWidth, newHeight);
            tex.SetPixels(newColors);
            tex.Apply();
            Texture2D orig = new Texture2D(tex.width, tex.height);
            if (flipV)
            {
                int xN = tex.width;
                int yN = tex.width;

                for (int i = 0; i < xN; i++)
                {
                    for (int j = 0; j < yN; j++)
                    {
                        // tex.SetPixel(xN - i - 1, j, orig.GetPixel(i, j));
                        orig.SetPixel(i, yN - j - 1, tex.GetPixel(i, j));
                    }
                }
                orig.Apply();

            }
            else if (flipH)
            {
                int xN = tex.width;
                int yN = tex.width;

                for (int i = 0; i < xN; i++)
                {
                    for (int j = 0; j < yN; j++)
                    {
                        // tex.SetPixel(xN - i - 1, j, orig.GetPixel(i, j));
                        orig.SetPixel(xN - i - 1, j, tex.GetPixel(i, j));
                    }
                }
                orig.Apply();

            }
            else
            {
                orig = tex;
            }
            return orig;
        }

        public static void BilinearScale(System.Object obj)
        {
            ThreadData threadData = (ThreadData)obj;
            for (var y = threadData.start; y < threadData.end; y++)
            {
                int yFloor = (int)Mathf.Floor(y * ratioY);
                var y1 = yFloor * w;
                var y2 = (yFloor + 1) * w;
                var yw = y * w2;

                for (var x = 0; x < w2; x++)
                {
                    int xFloor = (int)Mathf.Floor(x * ratioX);
                    var xLerp = x * ratioX - xFloor;
                    newColors[yw + x] = ColorLerpUnclamped(ColorLerpUnclamped(texColors[y1 + xFloor], texColors[y1 + xFloor + 1], xLerp),
                                                           ColorLerpUnclamped(texColors[y2 + xFloor], texColors[y2 + xFloor + 1], xLerp),
                                                           y * ratioY - yFloor);
                }
            }

            mutex.WaitOne();
            finishCount++;
            mutex.ReleaseMutex();
        }

        public static void PointScale(System.Object obj)
        {
            ThreadData threadData = (ThreadData)obj;
            for (var y = threadData.start; y < threadData.end; y++)
            {
                var thisY = (int)(ratioY * y) * w;
                var yw = y * w2;
                for (var x = 0; x < w2; x++)
                {
                    newColors[yw + x] = texColors[(int)(thisY + ratioX * x)];
                }
            }

            mutex.WaitOne();
            finishCount++;
            mutex.ReleaseMutex();
        }

        private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
        {
            return new Color(c1.r + (c2.r - c1.r) * value,
                              c1.g + (c2.g - c1.g) * value,
                              c1.b + (c2.b - c1.b) * value,
                              c1.a + (c2.a - c1.a) * value);
        }
    }
}