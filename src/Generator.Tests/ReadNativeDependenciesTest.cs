﻿using System.Collections.Generic;
using CppSharp.Utils;
using NUnit.Framework;

namespace CppSharp.Generator.Tests
{
    [TestFixture]
    public class ReadNativeDependenciesTest
    {
        [Test]
        public void TestReadDependenciesWindows()
        {
            var dependencies = GetDependencies("ls-windows");
            Assert.AreEqual("msys-intl-8.dll", dependencies[0]);
            Assert.AreEqual("msys-2.0.dll", dependencies[1]);
            Assert.AreEqual("KERNEL32.dll", dependencies[2]);
        }

        [Test]
        public void TestReadDependenciesLinux()
        {
            var dependencies = GetDependencies("ls-linux");
            Assert.AreEqual("libselinux.so.1", dependencies[0]);
            Assert.AreEqual("librt.so.1", dependencies[1]);
            Assert.AreEqual("libacl.so.1", dependencies[2]);
            Assert.AreEqual("libc.so.6", dependencies[3]);
        }

        [Test]
        public void TestReadDependenciesOSX()
        {
            var dependencies = GetDependencies("ls-osx");
            Assert.AreEqual("libutil.dylib", dependencies[0]);
            Assert.AreEqual("libncurses.5.4.dylib", dependencies[1]);
            Assert.AreEqual("libSystem.B.dylib", dependencies[2]);
        }

        private static IList<string> GetDependencies(string library)
        {
            var driverOptions = new DriverOptions();
            driverOptions.addLibraryDirs(GeneratorTest.GetTestsDirectory("Native"));
            driverOptions.Libraries.Add(library);
            var driver = new Driver(driverOptions, new TextDiagnosticPrinter());
            Assert.IsTrue(driver.ParseLibraries());
            var dependencies = driver.Symbols.Libraries[0].Dependencies;
            return dependencies;
        }
    }
}
