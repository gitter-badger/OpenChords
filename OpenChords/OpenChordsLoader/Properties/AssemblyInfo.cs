﻿#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("OpenChords")]
[assembly: AssemblyDescription("OpenChords is a guitar chord management tool. It allows the user to present songs with guitar chords to a computer screen, so printing pages and pages of guitar chords is a thing of the past. Its opensource, easy to use, clean, and has a clutter-free interface.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("OpenChords")]
[assembly: AssemblyCopyright("GNU General Public License version 3.0 (GPLv3)")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("1.3.0.1")]

[assembly: log4net.Config.XmlConfigurator(ConfigFile =
                "Log4Net.config", Watch = true)]
