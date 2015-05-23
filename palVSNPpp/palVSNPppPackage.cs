/**
 * Peter LaValle
 * 
 * This is mostly the template for a plugin, I changed it to open NotePad++
 * 
 * 2015-05-23
 *  
 * 
 * 
License:
   This software is in the public domain. Where that dedication is not
   recognized, you are granted a perpetual, irrevocable license to copy
   and modify this file however you want.
 */

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
		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public palVSNPppPackage()
		{
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
		}



		/////////////////////////////////////////////////////////////////////////////
		// Overridden Package Implementation
		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
			base.Initialize();

			// Add our command handlers for menu (commands must exist in the .vsct file)
			OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			if (null != mcs)
			{
				// Create the command for the menu item.
				CommandID menuCommandID = new CommandID(GuidList.guidpalVSNPppCmdSet, (int)PkgCmdIDList.openInNPpp);
				MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
				mcs.AddCommand(menuItem);
			}
		}
		#endregion


		private string _ProgramPath = null;
		private string ProgramPath
		{
			get
			{
				if (null != _ProgramPath)
				{
					return _ProgramPath;
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
						_ProgramPath = next;
						goto done;
					}
				}
			done:
				return _ProgramPath;
			}
		}

		/// <summary>
		/// This callback opens NotePad++
		/// </summary>
		private void MenuItemCallback(object sender, EventArgs e)
		{
			// get the ActiveDocument
			var activeDocument = ((EnvDTE80.DTE2)Package.GetGlobalService(typeof(EnvDTE._DTE))).ActiveDocument;

			// create a process
			var process = new Process();

			// TODO : make this configurable
			process.StartInfo.FileName = ProgramPath;

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
