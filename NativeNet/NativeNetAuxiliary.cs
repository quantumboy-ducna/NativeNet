/// <summary>
///		Legal & Licensing Information
/// </summary>
/// <remarks>
///		Required Notice: Copyright©2026, Nguyễn Anh Đức (workofduc@gmail.com). All Rights Reserved.
///
///		DUAL-LICENSING MODEL:
///		This software is dual-licensed to accommodate both open-source development and proprietary commercial use.
///
///		OPEN-SOURCE TRACK (GPLv3):
///		This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
///
///		COMMERCIAL TRACK:
///		For commercial entities wishing to embed this software into proprietary, closed-source software, a separate commercial license is required. This grants the legal right to use the library without being bound by the GPLv3 copyleft requirements.
///
///		CONTACT:
///		For commercial licensing inquiries, pricing, or to obtain a proprietary license agreement, please contact: workofduc@gmail.com
/// </remarks>

/** Inclusion(s) of the standard C# namespace(s).**/
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# class: `NativeNetAuxiliary`.
	/// </summary>
	public static class NativeNetAuxiliary
	{
		/// <summary>
		///		static
		/// </summary>
		/// <param name="index"></param>
		/// <returns>void</returns>
		/// <exception cref="Exception"></exception>
		[DoesNotReturn]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void throwOutOfBoundException(uint index)
		{
			throw new NativeNetException("Argument `index` is out of bound: `" + Convert.ToString(index) + "`.");
		}
	};
};