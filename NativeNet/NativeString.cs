/// <summary>
///		Legal & Licensing Information
/// </summary>
/// <remarks>
///		Required Notice: Copyright©2026,Nguyễn Anh Đức (workofduc@gmail.com). All Rights Reserved.
///
///		DUAL-LICENSING MODEL:
///		This software is dual-licensed to accommodate both open-source development and proprietary commercial use.
///
///		OPEN-SOURCE TRACK (GPLv3):
///		This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,either version 3 of the License,or (at your option) any later version.
///
///		COMMERCIAL TRACK:
///		For commercial entities wishing to embed this software into proprietary,closed-source software,a separate commercial license is required. This grants the legal right to use the library without being bound by the GPLv3 copyleft requirements.
///
///		CONTACT:
///		For commercial licensing inquiries,pricing,or to obtain a proprietary license agreement,please contact: workofduc@gmail.com
/// </remarks>

/** Inclusion(s) of the standard C# namespace(s).**/
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# generic class: `NativeString`.
	/// </summary>
	/// <typeparam name="GenericTypeOfCharacter"></typeparam>
	public unsafe sealed class NativeString<GenericTypeOfCharacter> : IDisposable where GenericTypeOfCharacter : unmanaged,NativeCharacterTraits<GenericTypeOfCharacter>
	{
		private GenericTypeOfCharacter* bufferPointer;
		private uint length;
		private uint capacity;


		/// <summary>
		///		Static constructor of `NativeString`.
		/// </summary>
		/// <exception cref="NativeNetException"></exception>
		static NativeString()
		{
			if ((typeof(GenericTypeOfCharacter) != typeof(NativeUTF8Character)) && (typeof(GenericTypeOfCharacter) != typeof(NativeUTF16Character)) && (typeof(GenericTypeOfCharacter) != typeof(NativeUTF32Character)))
			{
				throw new NativeNetException("Can't instantiate an instance of `NativeString` by the constructor of `NativeString` from an invalid generic type argument: `" + typeof(GenericTypeOfCharacter).Name + "`!");
			}
		}

		/// <summary>
		///		Constructor of `NativeString`.
		/// </summary>
		public NativeString()
		{
			this.bufferPointer = null;
			this.length = 0;
			this.capacity = 0;
		}

		/// <summary>
		///		Constructor of `NativeString`.
		/// </summary>
		/// <param name="byteSpan"></param>
		/// <exception cref="NativeNetException"></exception>
		public NativeString(ReadOnlySpan<byte> byteSpan)
		{
			if (byteSpan.Length == 0)
			{
				this.bufferPointer = null;
				this.length = 0;
				this.capacity = 0;
			}
			else
			{
				if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF8Character))
				{
					this.length = (uint)(byteSpan.Length);
					this.capacity = this.length;
					uint byteCount = (uint)this.length;
					this.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc(byteCount));

					fixed (byte* bytePointer = byteSpan)
					{
						Unsafe.CopyBlock(this.bufferPointer,bytePointer,byteCount);
					}
				}
				else if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF16Character))
				{
					char[] utf16Bytes = new char[Encoding.UTF8.GetCharCount(byteSpan)];
					Encoding.UTF8.GetChars(byteSpan,utf16Bytes);
					this.length = (uint)(byteSpan.Length);
					this.capacity = this.length;
					uint byteCount = (uint)(sizeof(GenericTypeOfCharacter) * this.length);
					this.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc(byteCount));

					fixed (char* characterPointer = utf16Bytes)
					{
						Unsafe.CopyBlock(this.bufferPointer,characterPointer,byteCount);
					}
				}
				else if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF32Character))
				{
					Span<char> characterSpan = stackalloc char[Encoding.UTF8.GetCharCount(byteSpan)];
					Encoding.UTF8.GetChars(byteSpan,characterSpan);
					byte[] utf32Bytes = new byte[Encoding.UTF32.GetByteCount(characterSpan)];
					Encoding.UTF32.GetBytes(characterSpan,utf32Bytes);
					this.length = (uint)(utf32Bytes.Length / 4);
					this.capacity = this.length;
					uint byteCount = (uint)(sizeof(GenericTypeOfCharacter) * this.length);
					this.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc(byteCount));

					fixed (byte* bytePointer = utf32Bytes)
					{
						Unsafe.CopyBlock(this.bufferPointer,bytePointer,byteCount);
					}
				}
				else
				{
					throw new NativeNetException("Can't instantiate an instance of `NativeString` by the constructor of `NativeString` from an invalid generic type argument: `" + typeof(GenericTypeOfCharacter).Name + "`!");
				}
			}
		}

		/// <summary>
		///		Constructor of `NativeString`.
		/// </summary>
		/// <param name="primitiveString"></param>
		/// <exception cref="NativeNetException"></exception>
		public NativeString(string primitiveString)
		{
			if (primitiveString is null)
			{
				throw new NativeNetException("Can't instantiate an instance of `NativeString` by the constructor of `NativeString` from a nulled argument `primitiveString`!");
			}
			else
			{
				if (primitiveString.Length == 0)
				{
					this.bufferPointer = null;
					this.length = 0;
					this.capacity = 0;
				}
				else
				{
					if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF8Character))
					{
						byte[] utf8Bytes = Encoding.UTF8.GetBytes(primitiveString);
						this.length = (uint)(utf8Bytes.Length);
						this.capacity = this.length;
						uint byteCount = (uint)this.length;
						this.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc(byteCount));

						fixed (byte* bytePointer = utf8Bytes)
						{
							Unsafe.CopyBlock(this.bufferPointer,bytePointer,byteCount);
						}
					}
					else if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF16Character))
					{
						this.length = (uint)(primitiveString.Length);
						this.capacity = this.length;
						uint byteCount = (uint)(sizeof(GenericTypeOfCharacter) * this.length);
						this.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc(byteCount));

						fixed (char* characterPointer = primitiveString)
						{
							Unsafe.CopyBlock(this.bufferPointer,characterPointer,byteCount);
						}
					}
					else if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF32Character))
					{
						byte[] utf32Bytes = Encoding.UTF32.GetBytes(primitiveString);
						this.length = (uint)(utf32Bytes.Length / 4);
						this.capacity = this.length;
						uint byteCount = (uint)(sizeof(GenericTypeOfCharacter) * this.length);
						this.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc(byteCount));

						fixed (byte* bytePointer = utf32Bytes)
						{
							Unsafe.CopyBlock(this.bufferPointer,bytePointer,byteCount);
						}
					}
					else
					{
						throw new NativeNetException("Can't instantiate an instance of `NativeString` by the constructor of `NativeString` from an invalid generic type argument: `" + typeof(GenericTypeOfCharacter).Name + "`!");
					}
				}
			}
		}

		/// <summary>
		///		Copy constructor of `NativeString`.
		/// </summary>
		/// <param name="other"></param>
		/// <exception cref="NativeNetException"></exception>
		public NativeString(NativeString<GenericTypeOfCharacter> other)
		{
			if (other is null)
			{
				throw new NativeNetException("Can't instantiate an instance of `NativeString` by the copy constructor of `NativeString` from a nulled argument `other`!");
			}
			else
			{
				this.capacity = other.capacity;
				this.length = other.length;
				this.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc((nuint)(sizeof(GenericTypeOfCharacter) * this.length)));
				Unsafe.CopyBlock(this.bufferPointer,other.bufferPointer,(uint)(sizeof(GenericTypeOfCharacter) * this.length));
			}
		}

		/// <summary>
		///		Destructor of `NativeString`.
		/// </summary>
		~NativeString()
		{
			if (this.bufferPointer is not null)
			{
				NativeMemory.Free(this.bufferPointer);
				this.bufferPointer = null;
				this.length = 0;
				this.capacity = 0;
			}
		}

		/// <summary>
		///		static
		///		implicit
		///		operator NativeString()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>string</returns>
		public static implicit operator NativeString<GenericTypeOfCharacter>(ReadOnlySpan<byte> instance)
		{
			return new NativeString<GenericTypeOfCharacter>(instance);
		}

		/// <summary>
		///		static
		///		implicit
		///		operator NativeString()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>string</returns>
		public static implicit operator NativeString<GenericTypeOfCharacter>(string instance)
		{
			if (instance is null)
			{
				return null;
			}
			else
			{
				return new NativeString<GenericTypeOfCharacter>(instance);
			}
		}

		/// <summary>
		///		static
		///		implicit
		///		operator string()
		/// </summary>
		/// <param name="instance"></param>
		/// <returns>string</returns>
		public static implicit operator string(NativeString<GenericTypeOfCharacter> instance)
		{
			if (instance is null)
			{
				return null;
			}
			else
			{
				return instance.ToString();
			}
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
					return ((new ReadOnlySpan<byte>(first.bufferPointer,(int)(first.length * sizeof(GenericTypeOfCharacter)))).SequenceEqual(new ReadOnlySpan<byte>(second.bufferPointer,(int)(second.length * sizeof(GenericTypeOfCharacter)))) == true);
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
					return ((new ReadOnlySpan<byte>(first.bufferPointer,(int)(first.length * sizeof(GenericTypeOfCharacter)))).SequenceEqual(new ReadOnlySpan<byte>(second.bufferPointer,(int)(second.length * sizeof(GenericTypeOfCharacter)))) == false);
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
				result.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc((nuint)(sizeof(GenericTypeOfCharacter) * result.length)));
				Buffer.MemoryCopy(first.bufferPointer,result.bufferPointer,sizeof(GenericTypeOfCharacter) * first.length,sizeof(GenericTypeOfCharacter) * first.length);
				Buffer.MemoryCopy(second.bufferPointer,result.bufferPointer + first.length,sizeof(GenericTypeOfCharacter) * second.length,sizeof(GenericTypeOfCharacter) * second.length);

				return result;
			}
		}

		/// <summary>
		///		static
		///		operator-
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>NativeString&lt;GenericTypeOfCharacter&gt;</returns>
		public static NativeString<GenericTypeOfCharacter> operator-(NativeString<GenericTypeOfCharacter> first,NativeString<GenericTypeOfCharacter> second)
		{
			if ((first is null) || (second is null))
			{
				return null;
			}
			else
			{
				int foundedIndex = (new ReadOnlySpan<GenericTypeOfCharacter>(first.bufferPointer,(int)(first.length))).IndexOf(new ReadOnlySpan<GenericTypeOfCharacter>(second.bufferPointer,(int)(second.length)));

				if ((foundedIndex < 0) || (first.length < second.length))
				{
					throw new NativeNetException("Can't subtract argument `second` from argument `first` because the data of the latter instance doesn't contain the data of the former one!");
				}
				else
				{
					NativeString<GenericTypeOfCharacter> result = new NativeString<GenericTypeOfCharacter>();
					result.length = first.length - second.length;
					result.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc((nuint)(sizeof(GenericTypeOfCharacter) * result.length)));
					Buffer.MemoryCopy(first.bufferPointer,result.bufferPointer,sizeof(GenericTypeOfCharacter) * foundedIndex,sizeof(GenericTypeOfCharacter) * foundedIndex);
					Buffer.MemoryCopy(first.bufferPointer + foundedIndex + second.length,result.bufferPointer + foundedIndex,sizeof(GenericTypeOfCharacter) * (result.length - foundedIndex),sizeof(GenericTypeOfCharacter) * (result.length - foundedIndex));

					return result;
				}
			}
		}

		/// <summary>
		///		dynamic
		///		operator[]
		/// </summary>
		/// <param name="index"></param>
		/// <returns>GenericTypeOfCharacter</returns>
		/// <exception cref="NativeNetException"></exception>
		public GenericTypeOfCharacter this[uint index]
		{
			get
			{
				if ((index >= this.length) || (index < 0) || (this.bufferPointer == null))
				{
					NativeNetAuxiliary.throwOutOfBoundException(index);

					return default;
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
			return ((other is NativeString<GenericTypeOfCharacter> instance) && (this == instance));
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
		///		override
		/// </summary>
		/// <returns>string</returns>
		/// <exception cref="NativeNetException"></exception>
		public override string ToString()
		{
			if ((this.bufferPointer == null) || (this.length == 0))
			{
				return string.Empty;
			}
			else
			{
				if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF8Character))
				{
					return Encoding.UTF8.GetString((byte*)(this.bufferPointer),(int)(this.length));
				}
				else if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF16Character))
				{
					return new string((char*)(this.bufferPointer),0,(int)(this.length));
				}
				else if (typeof(GenericTypeOfCharacter) == typeof(NativeUTF32Character))
				{
					return Encoding.UTF32.GetString((byte*)(this.bufferPointer),(int)(this.length * sizeof(NativeUTF32Character)));
				}
				else
				{
					throw new NativeNetException($"Unsupported character type: `{typeof(GenericTypeOfCharacter).Name}`!");
				}
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>void</returns>
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);

			if (this.bufferPointer is not null)
			{
				NativeMemory.Free(this.bufferPointer);
				this.bufferPointer = null;
				this.length = 0;
				this.capacity = 0;
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>int</returns>
		public uint getLength()
		{
			return this.length;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="other"></param>
		/// <returns>bool</returns>
		public bool contains(NativeString<GenericTypeOfCharacter> other)
		{
			if (other is null)
			{
				return false;
			}
			else
			{
				return ((new ReadOnlySpan<GenericTypeOfCharacter>(this.bufferPointer,(int)(this.length))).IndexOf(new ReadOnlySpan<GenericTypeOfCharacter>(other.bufferPointer,(int)(other.length))) >= 0);
			}
		}

		/// <summary>
		/// 	dynamic
		/// </summary>
		/// <param name="startingIndex"></param>
		/// <param name="length"></param>
		/// <returns>NativeString&lt;GenericTypeOfCharacter&gt;</returns>
		public NativeString<GenericTypeOfCharacter> substring(uint startingIndex,uint length)
		{
			NativeString<GenericTypeOfCharacter> result = new NativeString<GenericTypeOfCharacter>();
			result.length = length;
			result.bufferPointer = (GenericTypeOfCharacter*)(NativeMemory.Alloc((nuint)(sizeof(GenericTypeOfCharacter) * length)));
			Unsafe.CopyBlock(result.bufferPointer,this.bufferPointer + startingIndex,(uint)(sizeof(GenericTypeOfCharacter) * length));

			return result;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="character"></param>
		/// <returns>void</returns>
		public void append(GenericTypeOfCharacter character)
		{
			(this.length)++;

			if (this.length > this.capacity)
			{
				this.increaseCapacity();
			}

			(this.bufferPointer)[this.length - 1] = character;
		}

		/// <summary>
		/// 	dynamic
		/// </summary>
		/// <param name="other"></param>
		/// <returns>void</returns>
		/// <exception cref="NativeNetException"></exception>
		public void append(NativeString<GenericTypeOfCharacter> other)
		{
			if (other is null)
			{
				throw new NativeNetException("Argument `other` is null!");
			}
			else if (other.length > 0)
			{
				uint oldLength = this.length;
				this.length += other.length;

				if (this.length > this.capacity)
				{
					this.reserveCapacity(this.length * 2);
				}

				Unsafe.CopyBlock(this.bufferPointer + oldLength,other.bufferPointer,(uint)(sizeof(GenericTypeOfCharacter) * other.length));
			}
		}

		/// <summary>
		/// 	dynamic
		/// </summary>
		/// <param name="index"></param>
		/// <param name="character"></param>
		/// <returns>void</returns>
		public void insert(uint index,GenericTypeOfCharacter character)
		{
			if ((index < 0) || (index >= this.length))
			{
				NativeNetAuxiliary.throwOutOfBoundException(index);
			}
			else
			{
				(this.length)++;

				if (this.length > this.capacity)
				{
					this.increaseCapacity();
				}

				uint i = 0;

				for (i = (this.length - 1);i > index;i--)
				{
					(this.bufferPointer)[i] = (this.bufferPointer)[i - 1];
				}

				(this.bufferPointer)[index] = character;
			}
		}

		/// <summary>
		/// 	dynamic
		/// </summary>
		/// <param name="index"></param>
		/// <param name="other"></param>
		/// <returns>void</returns>
		/// <exception cref="NativeNetException"></exception>
		public void insert(uint index,NativeString<GenericTypeOfCharacter> other)
		{
			if (other is null)
			{
				throw new NativeNetException("Argument `other` is null!");
			}
			else if ((index < 0) || (index > this.length))
			{
				NativeNetAuxiliary.throwOutOfBoundException(index);
			}
			else if (other.length > 0)
			{
				uint oldLength = this.length;
				uint newLength = oldLength + other.length;

				if (newLength > this.capacity)
				{
					this.reserveCapacity(newLength * 2);
				}

				uint elementsToShift = oldLength - index;

				if (elementsToShift > 0)
				{
					Buffer.MemoryCopy(this.bufferPointer + index,this.bufferPointer + index + other.length,(ulong)(sizeof(GenericTypeOfCharacter) * elementsToShift),(ulong)(sizeof(GenericTypeOfCharacter) * elementsToShift));
				}

				Unsafe.CopyBlock(this.bufferPointer + index,other.bufferPointer,(uint)(sizeof(GenericTypeOfCharacter) * other.length));
				this.length = newLength;
			}
		}

		/// <summary>
		/// 	dynamic
		/// </summary>
		/// <param name="index"></param>
		/// <returns>void</returns>
		public void remove(uint index)
		{
			if ((index < 0) || (index > this.length) || (this.length == 0))
			{
				NativeNetAuxiliary.throwOutOfBoundException(index);
			}
			else
			{
				uint i = 0;

				for (i = index;i < (this.length - 1);i++)
				{
					(this.bufferPointer)[i] = (this.bufferPointer)[i + 1];
				}

				(this.length)--;
			}
		}

		/// <summary>
		/// 	dynamic
		/// </summary>
		/// <returns>void</returns>
		/// <exception cref="NativeNetException"></exception>
		private void increaseCapacity()
		{
			try
			{
				if (this.capacity <= 0)
				{
					this.capacity = 1;
				}
				
				uint newCapacity = this.capacity * 2;
				void* reallocatedMemoryPointer = NativeMemory.Realloc(this.bufferPointer,(nuint)(sizeof(GenericTypeOfCharacter) * newCapacity));

				if (reallocatedMemoryPointer == null)
				{
					throw new NativeNetException("Can't increase the capacity of the current instance of `NativeString`!");
				}
				else
				{
					this.bufferPointer = (GenericTypeOfCharacter*)reallocatedMemoryPointer;
					this.capacity = newCapacity;
				}
			}
			catch (OutOfMemoryException exception)
			{
				throw new NativeNetException(exception.Message);
			}
		}

		/// <summary>
		/// 	dynamic
		/// </summary>
		/// <param name="newCapacity"></param>
		/// <returns>void</returns>
		/// <exception cref="NativeNetException"></exception>
		private void reserveCapacity(uint newCapacity)
		{
			try
			{
				void* reallocatedMemoryPointer = NativeMemory.Realloc(this.bufferPointer,(nuint)(sizeof(GenericTypeOfCharacter) * newCapacity));

				if (reallocatedMemoryPointer == null)
				{
					throw new NativeNetException("Can't increase the capacity of the current instance of `NativeString`!");
				}
				else
				{
					this.bufferPointer = (GenericTypeOfCharacter*)reallocatedMemoryPointer;
					this.capacity = newCapacity;
				}
			}
			catch (OutOfMemoryException exception)
			{
				throw new NativeNetException(exception.Message);
			}
		}
	};
};