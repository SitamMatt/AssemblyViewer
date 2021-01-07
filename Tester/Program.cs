﻿using System.IO;
using System.Reflection;
using System.Xml;
using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;
using Model.Data;

namespace Tester
{
    public class Program
    {
        public int prop;

        public int Prop { get; set; }
        public int ReadProp { get; }
        
        static void Main(string[] args)
        {
            var asm = Assembly.GetExecutingAssembly();
            System.Reflection.PropertyInfo[] props = typeof(Program).GetProperties();
            var p = props[0];
            var p1 = props[1];
            //var info = new Converter().Convert(asm);
            //IExtendedXmlSerializer serializer = new ConfigurationContainer()
            //    .UseAutoFormatting()
            //    .UseOptimizedNamespaces()
            //    .EnableReferences()
            //    .Create();
            //using (var fs = File.Create(@"F:/res.xml"))
            //{
            //    serializer.Serialize(new XmlWriterSettings { Indent = true }, fs, info);
            //}
            //using var readFs = File.OpenRead(@"F:/res.xml");
            //var info2 = serializer.Deserialize<AssemblyInfo>(readFs);
        }
    }

    public class A
    {
        public B b;
    }

    public class B
    {
        public A a;
    }
}
