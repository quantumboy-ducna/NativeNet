/// <summary>
///		Legal & Licensing Information
/// </summary>
/// <remarks>
///		Required Notice: Copyright@2026 QuantumBoy1010 (https://www.github.com/QuantumBoy1010/)
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
	///		C# interface: `CharacterTraits`.
	/// </summary>
	internal interface CharacterTraits<GenericTypeOfCharacter> where GenericTypeOfCharacter : CharacterTraits<GenericTypeOfCharacter>
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
	};

	/// <summary>
	///		C# structure: `UTF32Character`.
	/// </summary>
	public readonly struct UTF32Character : CharacterTraits<UTF32Character>
	{
		private readonly int composedData;


		/// <summary>
		///		Constructor of `UTF32Character`.
		/// </summary>
		/// <param name="codePoint"></param>
		public UTF32Character(int codePoint)
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
		/// <param name="utf16Character"></param>
		public static implicit operator UTF32Character(char utf16Character)
		{
			return new UTF32Character((new Rune(utf16Character)).Value);
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
			if (other is UTF32Character data)
			{
				return (this == data);
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
			return char.ConvertFromUtf32(this.composedData);
		}
	};
};