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
