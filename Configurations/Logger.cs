﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurations
{
    public static class Logger
    {
        public static void InfoFormat(string message)
        {
            Console.WriteLine("<<<<<<  "+message+"  >>>>>>>>");
        }
    }
}
