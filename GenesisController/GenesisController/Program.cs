using System;
using System.Runtime.CompilerServices;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX.XInput;

namespace GenesisController
{
   
    public static class Program
    {
        public static readonly Controller MyController = new Controller(UserIndex.One);
        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
            
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Initialize the classes that we need
            Config.Initialize();

            if (MyController.IsConnected)
            {
                Chat.Print("Controller found!");
                ControllerManager.Initialize();
            }
            else
            {
                Chat.Print("Controller not found!");
            }

        }


    }
}
