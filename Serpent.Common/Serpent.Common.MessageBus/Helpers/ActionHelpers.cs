﻿namespace Serpent.Common.MessageBus.Helpers
{
    using System;

    internal static class ActionHelpers
    {
        public static Action NoAction { get; } = () => { };
    }
}