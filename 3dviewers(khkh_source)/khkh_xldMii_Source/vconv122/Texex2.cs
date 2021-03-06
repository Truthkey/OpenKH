using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace vconv122
{
    public class Texex2
    {
        public List<Patc> alp = new List<Patc>();
        public List<STim> alt = new List<STim>();

        public Bitmap GetTex(int w)
        {
            if (w < 0 || alt.Count <= w)
            {
                return null;
            }
            return alt[w].pic;
        }

        public Bitmap GetTex2(int w, byte[] al)
        {
            Bitmap tex = GetTex(w);
            if (tex != null)
            {
                var bitmap = (Bitmap) tex.Clone();
                int num = 0;
                while (num < alp.Count && num < al.Length)
                {
                    Patc patc = alp[num];
                    int num2 = al[num];
                    if (num2 < patc.ycnt && patc.texi == w)
                    {
                        BitmapData bitmapData = bitmap.LockBits(new Rectangle(patc.px, patc.py, patc.pcx, patc.pcy),
                            ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                        try
                        {
                            int num3 = patc.pcx*patc.pcy*num2;
                            for (int i = 0; i < patc.pcy; i++)
                            {
                                Marshal.Copy(patc.bits, num3 + patc.pcx*i,
                                    new IntPtr(bitmapData.Scan0.ToInt64() + i*bitmapData.Stride), patc.pcx);
                            }
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }
                    }
                    num++;
                }
                return bitmap;
            }
            return tex;
        }

        public Bitmap GetPattex(int p, int pi)
        {
            if (p < 0 || alp.Count <= p)
            {
                return null;
            }
            Patc patc = alp[p];
            if (pi < 0 || patc.ycnt <= pi)
            {
                return null;
            }
            Bitmap tex = GetTex(patc.texi);
            if (tex != null)
            {
                var bitmap = new Bitmap(tex.Width, tex.Height, PixelFormat.Format32bppArgb);
                using (var bitmap2 = new Bitmap(patc.pcx, patc.pcy, PixelFormat.Format8bppIndexed))
                {
                    BitmapData bitmapData = bitmap2.LockBits(new Rectangle(0, 0, patc.pcx, patc.pcy),
                        ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                    try
                    {
                        int num = patc.pcx*patc.pcy*pi;
                        for (int i = 0; i < patc.pcy; i++)
                        {
                            Marshal.Copy(patc.bits, num + patc.pcx*i,
                                new IntPtr(bitmapData.Scan0.ToInt64() + i*bitmapData.Stride), patc.pcx);
                        }
                    }
                    finally
                    {
                        bitmap2.UnlockBits(bitmapData);
                    }
                    bitmap2.Palette = tex.Palette;
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(Color.Empty);
                        graphics.DrawImageUnscaled(bitmap2, patc.px, patc.py);
                    }
                }
                return bitmap;
            }
            return null;
        }
    }
}