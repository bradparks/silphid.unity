﻿using System;

namespace Silphid.Showzup.Injection
{
    public static class IContainerExtensions
    {
        public static void Inject(this IContainer This, object obj, Action<IBinder> bind)
        {
            var child = This.CreateChild();
            bind(child);
            This.Inject(obj, child);
        }
    }
}