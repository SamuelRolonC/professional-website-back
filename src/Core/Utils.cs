using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class Utils
    {
        private static readonly int InnerExceptionRecursiveLimit = 4;

        public static StringBuilder GetDeepException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(exception.Message);

            var i = 0;
            while (exception.InnerException != null && i < InnerExceptionRecursiveLimit)
            {
                stringBuilder.AppendLine(exception.InnerException.Message);
                i++;
            }

            return stringBuilder;
        }
    }
}
