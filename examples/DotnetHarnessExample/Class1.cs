﻿using System;

namespace DotnetHarnessExample
{
    public class Class1
    {
        public void Main(Request request)
        {
            Console.WriteLine(request.Prop);
        }
    }

    public class Request
    {
        public string Prop { get; set; }
    }
}
