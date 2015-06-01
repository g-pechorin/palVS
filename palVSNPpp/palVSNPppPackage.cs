/**
 * Peter LaValle
 *
 * 2015-05-30
 * - added THG abilities
 * - added better icons
 * - removed noisy comments
 * 2015-05-23
 * - This is mostly the template for a plugin, I changed it to open NotePad++
 * 
 * 

	//// palVS - Visual Studio Extensions
	//// Copyright (C) 2015 Peter LaValle
	//// 
	//// This program is free software: you can redistribute it and/or modify
	//// it under the terms of the GNU Affero General Public License as
	//// published by the Free Software Foundation, either version 3 of the
	//// License, or (at your option) any later version.
	//// 
	//// This program is distributed in the hope that it will be useful,
	//// but WITHOUT ANY WARRANTY; without even the implied warranty of
	//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	//// GNU Affero General Public License for more details.
	//// 
	//// You should have received a copy of the GNU Affero General Public License
	//// along with this program.  If not, see <http://www.gnu.org/licenses/>. */

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace peterlavallecom.palVSNPpp
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	// This attribute is needed to let the shell know that this package exposes some menus.
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[Guid(GuidList.guidpalVSNPppPkgString)]
	public sealed class palVSNPppPackage : Package
	{
		#region Package Members

		protected override void Initialize()
		{
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
			base.Initialize();

			OleMenuCommandService oleMenuCommandService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			if (null != oleMenuCommandService)
			{
				oleMenuCommandService.AddCommand(new MenuCommand(open_in_notepad_p_p__callback, new CommandID(GuidList.guidpalVSNPppCmdSet, (int)PkgCmdIDList.open_in_notepad_p_p)));
				oleMenuCommandService.AddCommand(new MenuCommand(open_in_thg_workspace__callback, new CommandID(GuidList.guidpalVSNPppCmdSet, (int)PkgCmdIDList.open_in_thg_workspace)));
			}
		}
		#endregion

		private EnvDTE.Document ActiveDocument
		{
			get
			{
				return ((EnvDTE80.DTE2)Package.GetGlobalService(typeof(EnvDTE._DTE))).ActiveDocument;
			}
		}

		private void open_in_thg_workspace__callback(object sender, EventArgs e)
		{			
			// create a process
			var process = new Process();
			process.StartInfo.FileName = "thg";
			process.StartInfo.WorkingDirectory = ActiveDocument.Path;

			process.Start();
		}

		private string _NotePadPPPath = null;
		private string NotePadPPPath
		{
			get
			{
				if (null != _NotePadPPPath)
				{
					return _NotePadPPPath;
				}

				string[] paths = new string[]{
                    "Notepad++\\notepad++.exe",
                    "opt\\notepad++.exe",
                    "Program Files (x86)\\Notepad++\\notepad++.exe", // probably you
                    "Program Files\\Notepad++\\notepad++.exe",
                    "ProgramFiles\\Notepad++\\notepad++.exe",
                    "ProgramFiles(x86)\\Notepad++\\notepad++.exe", // me!
                };

				// begin a search
				for (char drive = 'A'; drive <= 'Z'; ++drive)
				{
					foreach (var path in paths)
					{
						var next = drive + ":\\" + path;

						if (!System.IO.File.Exists(next))
						{
							continue;
						}

						// found it
						_NotePadPPPath = next;
						goto done;
					}
				}
			done:
				return _NotePadPPPath;
			}
		}

		private void open_in_notepad_p_p__callback(object sender, EventArgs e)
		{
			// get the ActiveDocument
			var activeDocument = ActiveDocument;

			// create a process
			var process = new Process();

			// TODO : make this configurable
			process.StartInfo.FileName = NotePadPPPath;

			// if we found an active doucment - get notpad++ to open that
			if (null != activeDocument)
			{
				// tell notepad++ to open this guy exactly
				process.StartInfo.Arguments += activeDocument.FullName;

				// save the file (just in case)
				if (!activeDocument.Saved)
				{
					activeDocument.Save();
				}

				// TODO ; work out command line arguments to move to the proper line
			}

			// start notepad++
			process.Start();
		}
	}
}
