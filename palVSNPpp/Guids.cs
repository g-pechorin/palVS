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
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

// Guids.cs
// MUST match guids.h
using System;

namespace peterlavallecom.palVSNPpp
{
	static class GuidList
	{
		public const string guidpalVSNPppPkgString = "bb7305ce-bd9e-4d98-90c5-e1b225df3774";
		public const string guidpalVSNPppCmdSetString = "e3c8bf61-5fbb-46e1-a08f-e417914e3efb";

		public static readonly Guid guidpalVSNPppCmdSet = new Guid(guidpalVSNPppCmdSetString);
	};
}
