using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

using GR = OnePushSnap.Properties.Resources;
using GS = OnePushSnap.Properties.Settings;

namespace OnePushSnap
{
    internal class KeyHook
    {
        delegate int delegateHookCallback(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, delegateHookCallback lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        private static delegateHookCallback dhc;
        private static IntPtr hookPtr = IntPtr.Zero;

        public void start_stop_switch()
        {
            switch (GS.Default.working_flg)
            {
                case 0:
                    HookEnd();
                    break;
                case 1:
                    HookFor1PushSnap();
                    break;
                case 2:
                    HookFor1PushSnap();
                    break;
                case 102:
                    HookForIggKeyboard();
                    break;
                case 103:
                    HookForIggMouse();
                    break;
            }
        }

        public void HookFor1PushSnap()
        {
            int WH_KEYBOARD_LL = 13;

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                dhc = new delegateHookCallback(HookCallbackFor1PushSnap);

                hookPtr = SetWindowsHookEx(
                    WH_KEYBOARD_LL,
                    dhc,
                    GetModuleHandle(curModule.ModuleName),
                    0);
            }
        }

        public void HookForIggKeyboard()
        {
            int WH_KEYBOARD_LL = 13;

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                dhc = new delegateHookCallback(HookCallbackForIgg);

                hookPtr = SetWindowsHookEx(
                    WH_KEYBOARD_LL,
                    dhc,
                    GetModuleHandle(curModule.ModuleName),
                    0);
            }
        }

        public void HookForIggMouse()
        {
            int WH_MOUSE_LL = 14;

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                dhc = new delegateHookCallback(HookCallbackForIgg);

                hookPtr = SetWindowsHookEx(
                    WH_MOUSE_LL,
                    dhc,
                    GetModuleHandle(curModule.ModuleName),
                    0);
            }
        }

        private int HookCallbackFor1PushSnap(int nCode, IntPtr wParam, IntPtr lParam)
        {
            /// key event and key
            int snap_trigger = (int)wParam;
            Keys key = (Keys)(short)Marshal.ReadInt32(lParam);

            /// debug
            //Console.WriteLine(snap_trigger);
            //Console.WriteLine(key);

            Task.Factory.StartNew(() => CallbackTaskFor1PushSnap(snap_trigger, key));
            return 0;
        }

        private int HookCallbackForIgg(int nCode, IntPtr wParam, IntPtr lParam)
        {
            /// "return 1;" drops hooked keys
            return 1;
        }

        private void CallbackTaskFor1PushSnap(int snap_trigger, Keys key)
        {
            if (snap_trigger == GS.Default.trigger_event)
            {
                /// when PrintScreen was pressed
                if (key.ToString() == GS.Default.key_snap)
                {
                    Capture capt = new Capture();

                    switch (GS.Default.working_flg)
                    {
                        case 1:
                            capt.snapActiveWindow();
                            break;
                        case 2:
                            capt.snapCrop();
                            break;
                        default:
                            //capt.snapScreen();
                            break;
                    }
                }

                /// when Pause was pressed
                if (key.ToString() == GS.Default.key_pause)
                {
                    configuration_form.clickStop();
                }
            }
        }

        public void HookEnd()
        {
            UnhookWindowsHookEx(hookPtr);
            hookPtr = IntPtr.Zero;
        }
    }
}
