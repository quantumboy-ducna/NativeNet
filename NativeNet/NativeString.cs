/// <summary>
///		Legal & Licensing Information
/// </summary>
/// <remarks>
///		Required Notice: Copyright@2026 QuantumBoy1010 (https://www.github.com/QuantumBoy1010/)
/// </remarks>

/** Inclusion(s) of the standard C# namespace(s).**/
using System.Runtime.InteropServices;
using System.Text;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# class: `NativeString`.
	/// </summary>
	/// <typeparam name="GenericTypeOfCharacter"></typeparam>
	public unsafe sealed class NativeString<GenericTypeOfCharacter> : IDisposable where GenericTypeOfCharacter : unmanaged
	{
		private GenericTypeOfCharacter* bufferPointer;
		private int length;


		/// <summary>
		///		Constructor of `NativeString`.
		/// </summary>
		public NativeString()
		{
			this.bufferPointer = null;
			this.length = 0;
		}

		/// <summary>
		///		Constructor of `NativeString`.
		/// </summary>
		/// <param name="primitiveString"></param>
		/// <exception cref="Exception"></exception>
		public NativeString(string primitiveString)
		{
			if (primitiveString is null)
			{
				throw new Exception("Can't instantiate an instance of `NativeString` by the constructor of `NativeString` from a nulled argument `primitiveString`!");
			}
			else
			{
				this.length = primitiveString.Length;
				this.bufferPointer = (GenericTypeOfCharacter*)NativeMemory.Alloc((uint)(sizeof(GenericTypeOfCharacter) * this.length),8);

				if (typeof(GenericTypeOfCharacter) == typeof(char8_t))
				{
					byte[] byteArray = Encoding.UTF8.GetBytes([.. primitiveString]);

					fixed (byte* characterPointer = byteArray)
					{
						Buffer.MemoryCopy(characterPointer,this.bufferPointer,sizeof(GenericTypeOfCharacter) * this.length,sizeof(char) * primitiveString.Length);
					}
				}
				else if (typeof(GenericTypeOfCharacter) == typeof(char16_t))
				{
					fixed (char* characterPointer = primitiveString)
					{
						Buffer.MemoryCopy(characterPointer,this.bufferPointer,sizeof(GenericTypeOfCharacter) * this.length,sizeof(char) * primitiveString.Length);
					}
				}
				else if (typeof(GenericTypeOfCharacter) == typeof(char32_t))
				{
					byte[] byteArray = Encoding.UTF8.GetBytes([.. primitiveString]);

					fixed (byte* characterPointer = byteArray)
					{
						Buffer.MemoryCopy(characterPointer,this.bufferPointer,sizeof(GenericTypeOfCharacter) * this.length,sizeof(char) * primitiveString.Length);
					}
				}
				else
				{
					throw new Exception("Can't instantiate an instance of `NativeString` by the constructor of `NativeString` from an invalid generic type argument: `" + typeof(GenericTypeOfCharacter).Name + "`!");
				}
			}
		}

		/// <summary>
		///		Copy constructor of `NativeString`.
		/// </summary>
		/// <param name="other"></param>
		/// <exception cref="Exception"></exception>
		public NativeString(NativeString<GenericTypeOfCharacter> other)
		{
			if (other is null)
			{
				throw new Exception("Can't instantiate an instance of `NativeString` by the copy constructor of `NativeString` from a nulled argument `other`!");
			}
			else
			{
				this.length = other.length;
				this.bufferPointer = (GenericTypeOfCharacter*)Marshal.AllocHGlobal(sizeof(GenericTypeOfCharacter) * this.length);
				Buffer.MemoryCopy(other.bufferPointer,this.bufferPointer,sizeof(GenericTypeOfCharacter) * this.length,sizeof(GenericTypeOfCharacter) * other.length);
			}
		}

		/// <summary>
		///		Destructor of `NativeString`.
		/// </summary>
		~NativeString()
		{
			NativeMemory.Free(this.bufferPointer);
			this.length = 0;
		}

		/// <summary>
		///		static
		///		operator==
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator==(NativeString<GenericTypeOfCharacter> first,NativeString<GenericTypeOfCharacter> second)
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
				if (first.length != second.length)
				{
					return false;
				}
				else
				{
					int i = 0;

					for (i = 0;i < first.length;i++)
					{
						if ((first.bufferPointer)[i].Equals((second.bufferPointer)[i]) == false)
						{
							return false;
						}
					}

					return true;
				}
			}
		}

		/// <summary>
		///		static
		///		operator!=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator!=(NativeString<GenericTypeOfCharacter> first,NativeString<GenericTypeOfCharacter> second)
		{
			if (ReferenceEquals(first,second) == true)
			{
				return false;
			}
			else if (((first is null) && (second is not null)) || ((first is not null) && (second is null)))
			{
				return true;
			}
			else
			{
				if (first.length != second.length)
				{
					return true;
				}
				else
				{
					int i = 0;

					for (i = 0;i < first.length;i++)
					{
						if ((first.bufferPointer)[i].Equals((second.bufferPointer)[i]) == false)
						{
							return true;
						}
					}

					return false;
				}
			}
		}

		/// <summary>
		///		static
		///		operator+
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>NativeString&lt;GenericTypeOfCharacter&gt;</returns>
		public static NativeString<GenericTypeOfCharacter> operator+(NativeString<GenericTypeOfCharacter> first,NativeString<GenericTypeOfCharacter> second)
		{
			if ((first is null) || (second is null))
			{
				return null;
			}
			else
			{
				NativeString<GenericTypeOfCharacter> result = new NativeString<GenericTypeOfCharacter>();
				result.length = first.length + second.length;
				result.bufferPointer = (GenericTypeOfCharacter*)NativeMemory.Alloc((nuint)(sizeof(GenericTypeOfCharacter) * result.length));
				Buffer.MemoryCopy(first.bufferPointer,result.bufferPointer,sizeof(GenericTypeOfCharacter) * first.length,sizeof(GenericTypeOfCharacter) * first.length);
				Buffer.MemoryCopy(second.bufferPointer,result.bufferPointer + first.length,sizeof(GenericTypeOfCharacter) * second.length,sizeof(GenericTypeOfCharacter) * second.length);

				return result;
			}
		}

		/// <summary>
		///		dynamic
		///		operator[]
		/// </summary>
		/// <param name="index"></param>
		/// <returns>GenericTypeOfCharacter</returns>
		/// <exception cref="Exception"></exception>
		public GenericTypeOfCharacter this[int index]
		{
			get
			{
				if ((index >= this.length) || (index < 0))
				{
					throw new Exception("Argument `index` is out of bound: `" + Convert.ToString(index) + "`.");
				}
				else
				{
					return (this.bufferPointer)[index];
				}
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
			if (other is NativeString<GenericTypeOfCharacter> data)
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
			return HashCode.Combine(this.length,(UIntPtr)(this.bufferPointer));
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>void</returns>
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			NativeMemory.Free(this.bufferPointer);
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>int</returns>
		public int getLength()
		{
			return this.length;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="character"></param>
		/// <returns>void</returns>
		public void append(GenericTypeOfCharacter character)
		{
			NativeMemory.Realloc(this.bufferPointer,(nuint)(this.length + 1));
			(this.bufferPointer)[this.length] = character;
		}
	};
};