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
using System.Text;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# generic interface: `NativeCharacterTraits`.
	/// </summary>
	public interface NativeCharacterTraits<GenericTypeOfCharacter> where GenericTypeOfCharacter : NativeCharacterTraits<GenericTypeOfCharacter>
	{
		/// <summary>
		///		static
		///		abstract
		///		explicit
		///		operator uint()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>int</returns>
		public abstract static explicit operator uint(GenericTypeOfCharacter instance);

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
	};

	/// <summary>
	///		C# structure: `NativeUTF8Character`.
	/// </summary>
	public readonly struct NativeUTF8Character : NativeCharacterTraits<NativeUTF8Character>
	{
		private readonly byte composedData;


		/// <summary>
		/// 	Constructor of `NativeUTF8Character`.
		/// </summary>
		/// <param name="utf8CharacterData"></param>
		public NativeUTF8Character(byte utf8CharacterData)
		{
			this.composedData = utf8CharacterData;
		}

		/// <summary>
		/// 	Constructor of `NativeUTF8Character`.
		/// </summary>
		/// <param name="utf16CharacterData"></param>
		public NativeUTF8Character(char utf16CharacterData)
		{
			this.composedData = (byte)((new Rune(utf16CharacterData)).Value);
		}

		/// <summary>
		///		Copy constructor of `NativeUTF8Character`.
		/// </summary>
		/// <param name="other"></param>
		public NativeUTF8Character(NativeUTF8Character other)
		{
			this.composedData = other.composedData;
		}

		/// <summary>
		///		static
		///		implicit
		///		operator NativeUTF8Character()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>NativeUTF8Character</returns>
		public static implicit operator NativeUTF8Character(byte instance)
		{
			return new NativeUTF8Character(instance);
		}

		/// <summary>
		///		static
		///		explicit
		///		operator NativeUTF8Character()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>NativeUTF8Character</returns>
		public static explicit operator NativeUTF8Character(char instance)
		{
			return new NativeUTF8Character(instance);
		}

		/// <summary>
		///		static
		///		implicit
		///		operator byte()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>byte</returns>
		public static implicit operator byte(NativeUTF8Character instance)
		{
			return instance.composedData;
		}

		/// <summary>
		///		static
		///		explicit
		///		operator uint()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>uint</returns>
		public static explicit operator uint(NativeUTF8Character instance)
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
		public static bool operator==(NativeUTF8Character first,NativeUTF8Character second)
		{
			return (first.composedData == second.composedData);
		}

		/// <summary>
		///		static
		///		operator==
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator!=(NativeUTF8Character first,NativeUTF8Character second)
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
		public static bool operator<(NativeUTF8Character first,NativeUTF8Character second)
		{
			return (first.composedData < second.composedData);
		}

		/// <summary>
		///		static
		///		operator&gt;
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static bool operator>(NativeUTF8Character first,NativeUTF8Character second)
		{
			return (first.composedData > second.composedData);
		}

		/// <summary>
		///		static
		///		operator&lt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static bool operator<=(NativeUTF8Character first,NativeUTF8Character second)
		{
			return (first.composedData <= second.composedData);
		}

		/// <summary>
		///		static
		///		operator&gt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static bool operator>=(NativeUTF8Character first,NativeUTF8Character second)
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
			return ((other is NativeUTF8Character instance) && (this == instance));
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
			return Convert.ToString(this.composedData);
		}
	};

	/// <summary>
	///		C# structure: `NativeUTF16Character`.
	/// </summary>
	public readonly struct NativeUTF16Character : NativeCharacterTraits<NativeUTF16Character>
	{
		private readonly char composedData;


		/// <summary>
		/// 	Constructor of `NativeUTF16Character`.
		/// </summary>
		/// <param name="utf8CharacterData"></param>
		public NativeUTF16Character(byte utf8CharacterData)
		{
			this.composedData = (char)utf8CharacterData;
		}

		/// <summary>
		/// 	Constructor of `NativeUTF16Character`.
		/// </summary>
		/// <param name="utf16CharacterData"></param>
		public NativeUTF16Character(char utf16CharacterData)
		{
			this.composedData = utf16CharacterData;
		}

		/// <summary>
		///		Copy constructor of `NativeUTF16Character`.
		/// </summary>
		/// <param name="other"></param>
		public NativeUTF16Character(NativeUTF16Character other)
		{
			this.composedData = other.composedData;
		}

		/// <summary>
		///		static
		///		explicit
		///		operator NativeUTF16Character()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>NativeUTF16Character</returns>
		public static explicit operator NativeUTF16Character(byte instance)
		{
			return new NativeUTF16Character(instance);
		}

		/// <summary>
		///		static
		///		implicit
		///		operator NativeUTF16Character()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>NativeUTF16Character</returns>
		public static implicit operator NativeUTF16Character(char instance)
		{
			return new NativeUTF16Character(instance);
		}

		/// <summary>
		///		static
		///		implicit
		///		operator char()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>char</returns>
		public static implicit operator char(NativeUTF16Character instance)
		{
			return instance.composedData;
		}

		/// <summary>
		///		static
		///		explicit
		///		operator uint()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>uint</returns>
		public static explicit operator uint(NativeUTF16Character instance)
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
		public static bool operator==(NativeUTF16Character first,NativeUTF16Character second)
		{
			return (first.composedData == second.composedData);
		}

		/// <summary>
		///		static
		///		operator==
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator!=(NativeUTF16Character first,NativeUTF16Character second)
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
		public static bool operator<(NativeUTF16Character first,NativeUTF16Character second)
		{
			return (first.composedData < second.composedData);
		}

		/// <summary>
		///		static
		///		operator&gt;
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static bool operator>(NativeUTF16Character first,NativeUTF16Character second)
		{
			return (first.composedData > second.composedData);
		}

		/// <summary>
		///		static
		///		operator&lt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static bool operator<=(NativeUTF16Character first,NativeUTF16Character second)
		{
			return (first.composedData <= second.composedData);
		}

		/// <summary>
		///		static
		///		operator&gt;=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static bool operator>=(NativeUTF16Character first,NativeUTF16Character second)
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
			return ((other is NativeUTF16Character instance) && (this == instance));
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
			return Convert.ToString(this.composedData);
		}
	};

	/// <summary>
	///		C# structure: `NativeUTF32Character`.
	/// </summary>
	public readonly struct NativeUTF32Character : NativeCharacterTraits<NativeUTF32Character>
	{
		private readonly uint composedData;


		/// <summary>
		/// 	Constructor of `NativeUTF32Character`.
		/// </summary>
		/// <param name="utf8CharacterData"></param>
		public NativeUTF32Character(byte utf8CharacterData)
		{
			this.composedData = (uint)((new Rune(utf8CharacterData)).Value);
		}

		/// <summary>
		/// 	Constructor of `NativeUTF32Character`.
		/// </summary>
		/// <param name="utf16CharacterData"></param>
		public NativeUTF32Character(char utf16CharacterData)
		{
			this.composedData = (uint)((new Rune(utf16CharacterData)).Value);
		}

		/// <summary>
		///		Constructor of `NativeUTF32Character`.
		/// </summary>
		/// <param name="codePoint"></param>
		public NativeUTF32Character(uint codePoint)
		{
			this.composedData = codePoint;
		}

		/// <summary>
		///		Copy constructor of `NativeUTF32Character`.
		/// </summary>
		/// <param name="other"></param>
		public NativeUTF32Character(NativeUTF32Character other)
		{
			this.composedData = other.composedData;
		}

		/// <summary>
		///		static
		///		implicit
		///		operator NativeUTF32Character()
		/// </summary>
		/// <param name="byteData"></param>
		/// <returns>NativeUTF32Character</returns>
		public static explicit operator NativeUTF32Character(byte byteData)
		{
			return new NativeUTF32Character(byteData);
		}

		/// <summary>
		///		static
		///		implicit
		///		operator NativeUTF32Character()
		/// </summary>
		/// <param name="character"></param>
		/// <returns>NativeUTF32Character</returns>
		public static implicit operator NativeUTF32Character(char character)
		{
			return new NativeUTF32Character(character);
		}

		/// <summary>
		/// 	static
		/// 	explicit
		/// 	operator NativeUTF32Character()
		/// </summary>
		/// <param name="codePoint"></param>
		/// <returns>NativeUTF32Character</returns>
		public static explicit operator NativeUTF32Character(uint codePoint)
		{
			return new NativeUTF32Character(codePoint);
		}

		/// <summary>
		///		static
		///		explicit
		///		operator int()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>int</returns>
		public static explicit operator uint(NativeUTF32Character instance)
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
		public static bool operator==(NativeUTF32Character first,NativeUTF32Character second)
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
		public static bool operator!=(NativeUTF32Character first,NativeUTF32Character second)
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
		public static bool operator<(NativeUTF32Character first,NativeUTF32Character second)
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
		public static bool operator>(NativeUTF32Character first,NativeUTF32Character second)
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
		public static bool operator<=(NativeUTF32Character first,NativeUTF32Character second)
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
		public static bool operator>=(NativeUTF32Character first,NativeUTF32Character second)
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
			return ((other is NativeUTF32Character instance) && (this == instance));
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