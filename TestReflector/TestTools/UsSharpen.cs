using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;


namespace TestTools
{
    public class UsSharpen
    {
        #region 应用相关dll
        [DllImport("Gdi32.dll", EntryPoint = "GetPixel")]
        public static extern int GetPixel(IntPtr hDC, int x, int y);
        [DllImport("Gdi32.dll", EntryPoint = "SetPixel")]
        public static extern int SetPixel(IntPtr hDC, int x, int y, int color);
        #endregion
        public void LayAnalysis(string xmlpath)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Model_CXInputArgs model = Model_CXInputArgs.Deserialize(xmlpath);
            Bitmap bitmap = new Bitmap(Image.FromFile(model.InputFileName));
            Console.WriteLine("start layer analysis.......");
            #region 层析处理
            int iWidth = bitmap.Width;
            int iHigh = bitmap.Height;
            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHigh; j++)
                {
                    //白色为255，黑色为0
                    Color cc = bitmap.GetPixel(i, j);
                    int gcolor = (cc.R + cc.G + cc.B) / 3;
                    if (gcolor < 32)//黑
                    {
                        bitmap.SetPixel(i, j, Color.Black);

                    }
                    else if (gcolor >= 32 && gcolor < 64)//紫
                    {
                        bitmap.SetPixel(i, j, Color.Purple);

                    }
                    else if (gcolor >= 64 && gcolor < 96)//蓝
                    {
                        bitmap.SetPixel(i, j, Color.Blue);

                    }
                    else if (gcolor >= 96 && gcolor < 128)//青
                    {
                        bitmap.SetPixel(i, j, Color.Cyan);

                    }
                    else if (gcolor >= 128 && gcolor < 160)//绿
                    {
                        bitmap.SetPixel(i, j, Color.Green);

                    }
                    else if (gcolor >= 160 && gcolor < 192)//黄
                    {
                        bitmap.SetPixel(i, j, Color.Yellow);

                    }
                    else if (gcolor >= 192 && gcolor < 224)//橙
                    {
                        bitmap.SetPixel(i, j, Color.Orange);

                    }
                    else if (gcolor >= 224)//红
                    {
                        bitmap.SetPixel(i, j, Color.Red);

                    }
                }

            }
            bitmap.Save(model.OutPutFileName, System.Drawing.Imaging.ImageFormat.Bmp);
            bitmap.Dispose();

            #endregion
            sw.Stop();
            Console.WriteLine("layer analysis used {0} ms", sw.Elapsed.TotalMilliseconds);
        }
        public void UsharpAnalysis(string xmlpath)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Model_RHInputArgs model = Model_RHInputArgs.Deserialize(xmlpath);
            Bitmap bitmap = new Bitmap(Image.FromFile(model.InputFileName));
            Console.WriteLine("开始进行锐化处理……");
            #region 锐化处理
            int iHigh = bitmap.Height;
            int iWidth = bitmap.Width;
            Bitmap newBitmap = new Bitmap(iWidth, iHigh);
            Color pixel;
            //拉普拉斯模板
            int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int x = 1; x < iWidth - 1; x++)
            {
                for (int y = 1; y < iHigh - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int Index = 0;
                    for (int col = -1; col <= 1; col++)
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = bitmap.GetPixel(x + row, y + col);
                            r += pixel.R * Laplacian[Index];
                            g += pixel.G * Laplacian[Index];
                            b += pixel.B * Laplacian[Index];
                            Index++;
                        }
                    }
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
            }
            newBitmap.Save(model.OutPutFileName, System.Drawing.Imaging.ImageFormat.Bmp);
            newBitmap.Dispose();
            #endregion
            sw.Stop();
            Console.WriteLine("锐化处理用时 {0} ms", sw.Elapsed.TotalMilliseconds);
        }
        //中值滤波平滑处理
        public Bitmap ColorfulBitmapMedianFilterFunction(string str, int windowRadius, bool IsColorfulBitmap)
        {
            if (windowRadius < 1)
            {
                throw new Exception("过滤半径小于1没有意义");
            }
            Bitmap srcBmp = new Bitmap(Image.FromFile(str));
            //创建一个新的位图对象
            Bitmap bmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            //存储该图片所有点的RGB值
            byte[,] mR, mG, mB;
            mR = new byte[srcBmp.Width, srcBmp.Height];
            if (IsColorfulBitmap)
            {
                mG = new byte[srcBmp.Width, srcBmp.Height];
                mB = new byte[srcBmp.Width, srcBmp.Height];
            }
            else
            {
                mG = mR;
                mB = mR;
            }

            for (int i = 0; i <= srcBmp.Width - 1; i++)
            {
                for (int j = 0; j <= srcBmp.Height - 1; j++)
                {
                    mR[i, j] = srcBmp.GetPixel(i, j).R;
                    if (IsColorfulBitmap)
                    {
                        mG[i, j] = srcBmp.GetPixel(i, j).G;
                        mB[i, j] = srcBmp.GetPixel(i, j).B;
                    }
                }
            }

            mR = MedianFilterFunction(mR, windowRadius);
            if (IsColorfulBitmap)
            {
                mG = MedianFilterFunction(mG, windowRadius);
                mB = MedianFilterFunction(mB, windowRadius);
            }
            else
            {
                mG = mR;
                mB = mR;
            }
            for (int i = 0; i <= bmp.Width - 1; i++)
            {
                for (int j = 0; j <= bmp.Height - 1; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(mR[i, j], mG[i, j], mB[i, j]));
                }
            }
            return bmp;
        }
        private byte[,] MedianFilterFunction(byte[,] m, int windowRadius)
        {
            int width = m.GetLength(0);
            int height = m.GetLength(1);

            byte[,] lightArray = new byte[width, height];

            //开始滤波
            for (int i = 0; i <= width - 1; i++)
            {
                for (int j = 0; j <= height - 1; j++)
                {
                    //得到过滤窗口矩形
                    Rectangle rectWindow = new Rectangle(i - windowRadius, j - windowRadius, 2 * windowRadius + 1, 2 * windowRadius + 1);
                    if (rectWindow.Left < 0) rectWindow.X = 0;
                    if (rectWindow.Top < 0) rectWindow.Y = 0;
                    if (rectWindow.Right > width - 1) rectWindow.Width = width - 1 - rectWindow.Left;
                    if (rectWindow.Bottom > height - 1) rectWindow.Height = height - 1 - rectWindow.Top;
                    //将窗口中的颜色取到列表中
                    List<byte> windowPixelColorList = new List<byte>();
                    for (int oi = rectWindow.Left; oi <= rectWindow.Right - 1; oi++)
                    {
                        for (int oj = rectWindow.Top; oj <= rectWindow.Bottom - 1; oj++)
                        {
                            windowPixelColorList.Add(m[oi, oj]);
                        }
                    }
                    //排序
                    windowPixelColorList.Sort();
                    //取中值
                    byte middleValue = 0;
                    if ((windowRadius * windowRadius) % 2 == 0)
                    {
                        //如果是偶数
                        middleValue = Convert.ToByte((windowPixelColorList[windowPixelColorList.Count / 2] + windowPixelColorList[windowPixelColorList.Count / 2 - 1]) / 2);
                    }
                    else
                    {
                        //如果是奇数
                        middleValue = windowPixelColorList[(windowPixelColorList.Count - 1) / 2];
                    }
                    //设置为中值
                    lightArray[i, j] = middleValue;
                }
            }
            return lightArray;
        }
    }
}
