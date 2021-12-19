using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
                // フックを行う
                // 第1引数   フックするイベントの種類
                //   13はキーボードフックを表す
                // 第2引数 フック時のメソッドのアドレス
                //   フックメソッドを登録する
                // 第3引数   インスタンスハンドル
                //   現在実行中のハンドルを渡す
                // 第4引数   スレッドID
                //   0を指定すると、すべてのスレッドでフックされる
                hookPtr = SetWindowsHookEx(
                    13,
                    HookCallback,
                    GetModuleHandle(curModule.ModuleName),
                    0
                );
            }
        }

        int HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // フックしたキー
            int snap_trigger = (int)wParam;
            Keys key = (Keys)(short)Marshal.ReadInt32(lParam);

            //Console.WriteLine(snap_trigger);
            //Console.WriteLine(key);

            if (snap_trigger == Properties.Settings.Default.trigger_event && key.ToString() == Properties.Settings.Default.trigger_key)
            {
                Capture capt = new Capture();
                capt.snapActiveWindow();
                //capt.snapScreen();
            }

            // 1を戻すとフックしたキーが捨てられます
            //return 1;
            return 0;
        }

        public void HookEnd()
        {
            UnhookWindowsHookEx(hookPtr);
            hookPtr = IntPtr.Zero;
        }
    }
}
