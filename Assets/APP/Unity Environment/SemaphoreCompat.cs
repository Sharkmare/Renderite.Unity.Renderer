// IL2CPP compatibility shim for named Win32 semaphores.
//
// Unity IL2CPP does not implement System.Threading.Semaphore with a name --
// the BCL icall CreateSemaphore_internal throws NotSupportedException for
// any non-null name parameter. Cloudtoid.Interprocess requires named kernel
// semaphores for cross-process synchronisation between Resonite and this renderer.
//
// IMPORTANT - why SafeWaitHandle swap does NOT work in IL2CPP:
//   In IL2CPP, SafeWaitHandle.handle stores an il2cpp::os::Handle* (a C++ heap
//   pointer to a runtime wrapper object), NOT a Win32 HANDLE value.
//   Setting SafeWaitHandle to a new SafeWaitHandle(win32Handle, ...) puts a
//   raw Win32 HANDLE (e.g. 0x1A8) into a field that Wait_internal reads as a
//   C++ pointer -> immediate access violation.
//
// Correct approach:
//   Store the real Win32 HANDLE as an IntPtr in a custom field on SemaphoreWindows,
//   then use WaitForSingleObject / ReleaseSemaphore / CloseHandle P/Invoke directly,
//   bypassing IL2CPP's WaitHandle machinery entirely.
//
// Cecil patches:
//   patch-semaphorewindows-v2.ps1 replaces SemaphoreWindows..ctor / Wait / Release /
//   Dispose to call SemaphoreCompat.CreateNamedHandle / Wait / Release / Close.

using System;
using System.Runtime.InteropServices;

namespace Renderite.IL2CPPCompat
{
    internal static class SemaphoreCompat
    {
        // ── Win32 API P/Invoke ────────────────────────────────────────────────

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateSemaphoreW(
            IntPtr lpSemaphoreAttributes,
            int    lInitialCount,
            int    lMaximumCount,
            string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReleaseSemaphore(
            IntPtr hSemaphore,
            int    lReleaseCount,
            out int lpPreviousCount);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        private const uint WAIT_OBJECT_0  = 0x00000000u;
        private const uint WAIT_TIMEOUT   = 0x00000102u;
        private const uint WAIT_INFINITE  = 0xFFFFFFFFu;

        // ── Public API called by the patched SemaphoreWindows ─────────────────

        /// <summary>
        /// Creates or opens a named Win32 kernel semaphore.
        /// Returns the raw Win32 HANDLE as IntPtr.
        /// Stored in SemaphoreWindows._rawHandle by the patched .ctor.
        /// </summary>
        public static IntPtr CreateNamedHandle(string name)
        {
            IntPtr h = CreateSemaphoreW(IntPtr.Zero, 0, int.MaxValue, name);
            if (h == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception(
                    Marshal.GetLastWin32Error(),
                    "CreateSemaphoreW failed for: " + name);
            return h;
        }

        /// <summary>
        /// Waits on the raw Win32 semaphore handle.
        /// Returns true if signalled, false if timed out.
        /// milliseconds == -1 (or Timeout.Infinite) -> wait forever.
        /// </summary>
        public static bool Wait(IntPtr handle, int milliseconds)
        {
            uint ms = (milliseconds < 0) ? WAIT_INFINITE : (uint)milliseconds;
            uint result = WaitForSingleObject(handle, ms);
            return result == WAIT_OBJECT_0;
        }

        /// <summary>
        /// Releases the semaphore once (Release(1)).
        /// Equivalent to Semaphore.Release().
        /// </summary>
        public static void Release(IntPtr handle)
        {
            int prev;
            ReleaseSemaphore(handle, 1, out prev);
        }

        /// <summary>
        /// Closes the Win32 HANDLE.  Called from the patched SemaphoreWindows.Dispose.
        /// </summary>
        public static void Close(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
                CloseHandle(handle);
        }
    }
}
