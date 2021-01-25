using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TarkovToolBox.Utils;
using System.Timers;

namespace TarkovToolBox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainWindow ToolBoxOverlay { get; set; }
        public LowLevelKeyboardListener keyboardListener { get; set; }

        public Timer orphanedTimer { get; set; }

        void App_Startup(object sender, StartupEventArgs e)
        {

            if (!e.Args.Contains("/WelcomeToTarkov"))
                Shutdown();
            else
            {
                keyboardListener = new LowLevelKeyboardListener();
                keyboardListener.HookKeyboard();
                keyboardListener.OnKeyPressed += KeyboardListener_OnKeyPressed;

                ToolBoxOverlay = (MainWindow)this.MainWindow;

                ProcessWatcher.StartWatcher();

                orphanedTimer = new Timer();
                orphanedTimer.Elapsed += OrphanedTimer_Elapsed;
                orphanedTimer.Enabled = true;
                orphanedTimer.Interval = 250;
                orphanedTimer.Start();


            }
        }

        private void ToolBoxOverlay_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("WINDOW DED APP");
        }

        private void OrphanedTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ToolBoxOverlay.Dispatcher.Invoke((Action) (() =>
            {
                if (ToolBoxOverlay.WindowState == WindowState.Maximized)
                {
                    if (!TarkovIsFocused() && !ProcessWatcher.LastActiveWindowWasTarkov())
                        ToolBoxOverlay.WindowState = WindowState.Normal; 
                }
            }));
        }

        private void KeyboardListener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            ToggleOverlay(e, TarkovIsFocused());
        }

        private void ToggleOverlay(KeyPressedArgs e, bool tIsFocused)
        {
            if (e.KeyPressed == System.Windows.Input.Key.M
                && ToolBoxOverlay.WindowState == WindowState.Normal
                && tIsFocused)
                PerformToggle(WindowState.Maximized);
            else if (e.KeyPressed == System.Windows.Input.Key.M
                && ToolBoxOverlay.WindowState == WindowState.Maximized
                && tIsFocused)
                PerformToggle(WindowState.Normal);
            else if(e.KeyPressed == System.Windows.Input.Key.M
                && ToolBoxOverlay.WindowState == WindowState.Maximized
                && !tIsFocused && ProcessWatcher.LastActiveWindowWasTarkov())
                PerformToggle(WindowState.Normal);

            void PerformToggle(WindowState windowState)
            {
                ToolBoxOverlay.WindowState = windowState;

                switch (windowState)
                {
                    case WindowState.Maximized:
                        ToolBoxOverlay.Focus();
                        ToolBoxOverlay.Activate();
                        break;
                    default:
                        TarkovActivator.ActivateTarkov();
                        break;
                }
            }
        }

        private bool TarkovIsFocused()
        {
            return TarkovStateChecker.IsTarkovActive();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //Console.WriteLine("STEP3");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //Console.WriteLine("STEP1");
            CleanUp();
            base.OnExit(e);
        }

        private void CleanUp()
        {
            Console.WriteLine("Cleaing Keyboard Listener...");
            keyboardListener.UnHookKeyboard();
            Console.WriteLine("Cleaing Process watcher...");
            ProcessWatcher.StopWatcher();
            Console.WriteLine("Cleaing Orphan Timer...");
            orphanedTimer.Stop();
            orphanedTimer.Dispose();
        }

        private void Application_SessionEnding_1(object sender, SessionEndingCancelEventArgs e)
        {
            ////Doesn't fire for some reason:
            //Console.WriteLine("STEP2");
        }
    }
}
