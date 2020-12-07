﻿using OpenCvSharp;
using System;
using System.Windows.Forms;
using Zack.CameraLib.Core;

namespace WinFormCoreDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var cameras = CameraUtils.ListCameras();
            foreach(CameraInfo camera in cameras)
            {
                Console.WriteLine(camera.FriendlyName+","+camera.Index);
                foreach(VideoCapabilities v in camera.VideoCapabilities)
                {
                    Console.WriteLine($"{v.FrameRate},{v.Height},{v.Width},{v.VideoFormat}");
                }
            }

            var vc = cameras[0].VideoCapabilities[3];
            player.Start(1, new System.Drawing.Size(vc.Width, vc.Height));
            
            player.SetFrameFilter(srcMat=> {
                var dest = new Mat();
                Cv2.BilateralFilter(srcMat, dest, 10, 60, 60);
                return dest;
            });
        }
    }
}
