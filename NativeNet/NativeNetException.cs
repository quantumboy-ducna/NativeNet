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

#pragma warning disable IDE0003
#pragma warning disable IDE0017
#pragma warning disable IDE0047
#pragma warning disable IDE0090

/** Inclusion(s) of the standard C# namespace(s).**/
using System;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
    /// <summary>
    ///     C# class: `NativeNetException`.
    /// </summary>
    public sealed class NativeNetException : Exception
    {
        /// <summary>
        ///     Constructor of `NativeNetException`.
        /// </summary>
        /// <param name="message"></param>
        public NativeNetException(string message) : base(message)
        {
            
        }

        /// <summary>
        ///     Copy constructor of `NativeNetException`.
        /// </summary>
        /// <param name="other"></param> 
        public NativeNetException(NativeNetException other) : base((other is null) ? "Exception thrown!" : other.Message)
        {
            
        }

        /// <summary>
        ///     Destructor of `NativeNetException`.
        /// </summary>
        ~NativeNetException()
        {
            
        }

        /// <summary>
        ///     static
        ///     operator==
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>bool</returns>
        public static bool operator==(NativeNetException first,NativeNetException second)
        {
            if (ReferenceEquals(first,second) == true)
            {
                return true;
            }
            else if (((first is null) && (second is not null)) || ((first is not null) && (second is null)))
            {
                return false;
            }
            else
            {
                return (first.Message == second.Message);
            }
        }

        /// <summary>
        ///     static
        ///     operator!=
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>bool</returns>
        public static bool operator!=(NativeNetException first, NativeNetException second)
        {
            if (ReferenceEquals(first, second) == true)
            {
                return false;
            }
            else if (((first is null) && (second is not null)) || ((first is not null) && (second is null)))
            {
                return true;
            }
            else
            {
                return (first.Message != second.Message);
            }
        }

        /// <summary>
		///		dynamic
		///		override
		/// </summary>
		/// <param name="other"></param>
		/// <returns>bool</returns>
		public override bool Equals(object other)
        {
            if (other is NativeNetException instance)
            {
                return (this == instance);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///		dynamic
        ///		override
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     dynamic
        /// </summary>
        /// <returns>string</returns>
        public string getMessage()
        {
            return this.Message;
        }
    };
};