using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        IntPtr hookPtr = IntPtr.Zero;

        public void Hook()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                hookPtr = SetWindowsHookEx(
                    13,
                    HookCallback,
                    GetModuleHandle(curModule.ModuleName),
                    0
                );
            }
        }

        void CallbackTask(int snap_trigger, Keys key)
        {
            if (snap_trigger == Properties.Settings.Default.trigger_event && key.ToString() == Properties.Settings.Default.trigger_key)
            {
                Capture capt = new Capture();
                capt.snapActiveWindow();
                //capt.snapScreen();
            }
        }

        int HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            /// key event and key
            int snap_trigger = (int)wParam;
            Keys key = (Keys)(short)Marshal.ReadInt32(lParam);

            /// debug
            //Console.WriteLine(snap_trigger);
            //Console.WriteLine(key);

            Task.Factory.StartNew(() => CallbackTask(snap_trigger, key));

            return 0;

            /// "return 1;" drops hooked keys
            //return 1;
        }

        public void HookEnd()
        {
            UnhookWindowsHookEx(hookPtr);
            hookPtr = IntPtr.Zero;
        }
    }
}
