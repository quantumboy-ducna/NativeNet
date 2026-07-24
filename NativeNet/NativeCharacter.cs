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

#pragma warning disable IDE0001
#pragma warning disable IDE0003
#pragma warning disable IDE0047

/** Inclusion(s) of the standard C# namespace(s).**/
using System.Text;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# interface: `NativeCharacterTraits`.
	/// </summary>
	public interface NativeCharacterTraits<GenericTypeOfCharacter> where GenericTypeOfCharacter : NativeCharacterTraits<GenericTypeOfCharacter>
	{
		/// <summary>
		///		static
		///		abstract
		///		operator==
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public abstract static bool operator==(GenericTypeOfCharacter first,GenericTypeOfCharacter second);

		/// <summary>
		///		static
		///		abstract
		///		operator!=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public abstract static bool operator!=(GenericTypeOfCharacter first,GenericTypeOfCharacter second);

		/// <summary>
		///		static
		///		abstract
		///		operator&lt;
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public abstract static bool operator<(GenericTypeOfCharacter first,GenericTypeOfCharacter second);

		/// <summary>
		///		static
		///		abstract
		///		operator&gt;
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public abstract static bool operator>(GenericTypeOfCharacter first,GenericTypeOfCharacter second);

		/// <summary>
		///		static
		///		abstract
		///		operator&lt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public abstract static bool operator<=(GenericTypeOfCharacter first,GenericTypeOfCharacter second);

		/// <summary>
		///		static
		///		abstract
		///		operator&gt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public abstract static bool operator>=(GenericTypeOfCharacter first,GenericTypeOfCharacter second);

		/// <summary>
		///		static
		///		abstract
		///		explicit
		///		operator uint()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>int</returns>
		public abstract static explicit operator uint(GenericTypeOfCharacter instance);
	};

	/// <summary>
	///		C# structure: `UTF32Character`.
	/// </summary>
	public readonly struct UTF32Character : NativeCharacterTraits<UTF32Character>
	{
		private readonly uint composedData;


		/// <summary>
		/// 	Constructor of `UTF32Character`.
		/// </summary>
		/// <param name="utf8CharacterData"></param>
		public UTF32Character(byte utf8CharacterData)
		{
			this.composedData = (uint)((new Rune(utf8CharacterData)).Value);
		}

		/// <summary>
		/// 	Constructor of `UTF32Character`.
		/// </summary>
		/// <param name="utf16CharacterData"></param>
		public UTF32Character(char utf16CharacterData)
		{
			this.composedData = (uint)((new Rune(utf16CharacterData)).Value);
		}

		/// <summary>
		///		Constructor of `UTF32Character`.
		/// </summary>
		/// <param name="codePoint"></param>
		public UTF32Character(uint codePoint)
		{
			this.composedData = codePoint;
		}

		/// <summary>
		///		Copy constructor of `UTF32Character`.
		/// </summary>
		/// <param name="other"></param>
		public UTF32Character(UTF32Character other)
		{
			this.composedData = other.composedData;
		}

		/// <summary>
		///		static
		///		implicit
		///		operator UTF32Character()
		/// </summary>
		/// <param name="byteData"></param>
		/// <returns>UTF32Character</returns>
		public static explicit operator UTF32Character(byte byteData)
		{
			return new UTF32Character(byteData);
		}

		/// <summary>
		///		static
		///		implicit
		///		operator UTF32Character()
		/// </summary>
		/// <param name="character"></param>
		/// <returns>UTF32Character</returns>
		public static implicit operator UTF32Character(char character)
		{
			return new UTF32Character(character);
		}

		/// <summary>
		/// 	static
		/// 	explicit
		/// 	operator UTF32Character()
		/// </summary>
		/// <param name="codePoint"></param>
		/// <returns>UTF32Character</returns>
		public static explicit operator UTF32Character(uint codePoint)
		{
			return new UTF32Character(codePoint);
		}

		/// <summary>
		///		static
		///		explicit
		///		operator int()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>int</returns>
		public static explicit operator uint(UTF32Character instance)
		{
			return instance.composedData;
		}

		/// <summary>
		///		static
		///		operator==
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator==(UTF32Character first,UTF32Character second)
		{
			return (first.composedData == second.composedData);
		}

		/// <summary>
		///		static
		///		operator!=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator!=(UTF32Character first,UTF32Character second)
		{
			return (first.composedData != second.composedData);
		}

		/// <summary>
		///		static
		///		operator&lt;
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator<(UTF32Character first,UTF32Character second)
		{
			return (first.composedData < second.composedData);
		}

		/// <summary>
		///		static
		///		operator&gt;
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator>(UTF32Character first,UTF32Character second)
		{
			return (first.composedData > second.composedData);
		}

		/// <summary>
		///		static
		///		operator&lt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator<=(UTF32Character first,UTF32Character second)
		{
			return (first.composedData <= second.composedData);
		}

		/// <summary>
		///		static
		///		operator&gt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator>=(UTF32Character first,UTF32Character second)
		{
			return (first.composedData >= second.composedData);
		}

		/// <summary>
		///		dynamic
		///		override
		/// </summary>
		/// <param name="other"></param>
		/// <returns>bool</returns>
		public override bool Equals(object other)
		{
			if (other is UTF32Character instance)
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
			return (this.composedData).GetHashCode();
		}

		/// <summary>
		///		dynamic
		///		override
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return char.ConvertFromUtf32((int)(this.composedData));
		}
	};
};