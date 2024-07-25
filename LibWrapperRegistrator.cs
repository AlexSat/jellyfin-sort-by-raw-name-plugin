using MediaBrowser.Controller;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SortRawNamePlugin
{
    public class LibWrapperRegistrator : IPluginServiceRegistrator
    {
        public LibWrapperRegistrator() { }

        void IPluginServiceRegistrator.RegisterServices(IServiceCollection serviceCollection, IServerApplicationHost applicationHost)
        {
            serviceCollection.AddSingleton<ILibraryManager>(f =>
            {
                var originalType = applicationHost.GetExportTypes<ILibraryManager>().Last();
                var originalLibraryManager = (ILibraryManager)ActivatorUtilities.CreateInstance(f, originalType);
                var logger = f.GetRequiredService<ILogger<LibWrapperRegistrator>>();
                logger.LogInformation($"{nameof(LibWrapperRegistrator)} created original ILibraryManager");
                //var sorter = new LibWrapper((ILibraryManager)originalLibraryManager);
                logger.LogInformation($"{nameof(LibWrapperRegistrator)} created replacer for original ILibraryManager with force sorting by SortName");
                var originalMethod = originalType.GetMethod("GetItemsResult");
                var overrider = new LibOverrider(originalLibraryManager, originalMethod);
                var overrideMethod = typeof(LibOverrider).GetMethod("GetItemsResult");
                Injection.RedirectTo(originalMethod, overrideMethod);
                return originalLibraryManager;
            }
            );
        }
    }

    public static class Injection
    {
        public static void RedirectTo(this MethodInfo origin, MethodInfo target)
        {
            IntPtr ori = GetMethodAddress(origin);
            IntPtr tar = GetMethodAddress(target);

            Marshal.Copy(new IntPtr[] { Marshal.ReadIntPtr(tar) }, 0, ori, 1);
        }

        private static IntPtr GetMethodAddress(MethodInfo mi)
        {
            const ushort SLOT_NUMBER_MASK = 0xffff; // 2 bytes mask
            const int MT_OFFSET_32BIT = 0x28;       // 40 bytes offset
            const int MT_OFFSET_64BIT = 0x40;       // 64 bytes offset

            IntPtr address;

            // JIT compilation of the method
            RuntimeHelpers.PrepareMethod(mi.MethodHandle);

            IntPtr md = mi.MethodHandle.Value;             // MethodDescriptor address
            IntPtr mt = mi.DeclaringType.TypeHandle.Value; // MethodTable address

            if (mi.IsVirtual)
            {
                // The fixed-size portion of the MethodTable structure depends on the process type
                int offset = IntPtr.Size == 4 ? MT_OFFSET_32BIT : MT_OFFSET_64BIT;

                // First method slot = MethodTable address + fixed-size offset
                // This is the address of the first method of any type (i.e. ToString)
                IntPtr ms = Marshal.ReadIntPtr(mt + offset);

                // Get the slot number of the virtual method entry from the MethodDesc data structure
                long shift = Marshal.ReadInt64(md) >> 32;
                int slot = (int)(shift & SLOT_NUMBER_MASK);

                // Get the virtual method address relative to the first method slot
                address = ms + (slot * IntPtr.Size);
            }
            else
            {
                // Bypass default MethodDescriptor padding (8 bytes) 
                // Reach the CodeOrIL field which contains the address of the JIT-compiled code
                address = md + 8;
            }

            return address;
        }
    }
}
