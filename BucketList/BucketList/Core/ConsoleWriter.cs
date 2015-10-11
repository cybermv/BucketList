namespace BucketList.Core
{
    using System;

    /// <summary>
    /// Extension for the <see cref="Console"/> class that enable
    /// writing in color
    /// </summary>
    public static class ConsoleWriter
    {
        private static readonly object Locker = new object();
        private static readonly object[] EmptyParameters = new object[0];

        private const ConsoleColor DefaultForeColor = ConsoleColor.Gray;

        static ConsoleWriter()
        {
            Console.ForegroundColor = DefaultForeColor;
        }

        public static void WriteLine(string message)
        {
            WriteLine(message, EmptyParameters);
        }

        public static void WriteLine(string message, ConsoleColor color)
        {
            WriteLine(message, color, EmptyParameters);
        }

        public static void WriteLine(string format, params object[] parameters)
        {
            WriteLine(format, DefaultForeColor, parameters);
        }

        public static void WriteLine(string format, ConsoleColor color, params object[] parameters)
        {
            Write(format + Environment.NewLine, color, parameters);
        }

        public static void Write(string message)
        {
            Write(message, EmptyParameters);
        }

        public static void Write(string message, ConsoleColor color)
        {
            Write(message, color, EmptyParameters);
        }

        public static void Write(string format, params object[] parameters)
        {
            Write(format, DefaultForeColor, parameters);
        }

        public static void Write(string format, ConsoleColor color, params object[] parameters)
        {
            lock (Locker)
            {
                if (color != DefaultForeColor)
                {
                    Console.ForegroundColor = color;
                    Console.Write(format, parameters);
                    Console.ForegroundColor = DefaultForeColor;
                }
                else
                {
                    Console.Write(format, parameters);
                }
            }
        }
    }
}