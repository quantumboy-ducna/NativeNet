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
using System.Runtime.InteropServices;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# generic class: `NativeList`.
	/// </summary>
	/// <typeparam name="GenericType"></typeparam>
	public unsafe sealed class NativeList<GenericType> : IDisposable where GenericType : unmanaged,IComparable<GenericType>
	{
		private uint size;
		private uint capacity;
		private GenericType* bufferPointer;


		/// <summary>
		///		Constructor of `NativeList`.
		/// </summary>
		public NativeList()
		{
			this.size = 0;
			this.capacity = 1;
			this.bufferPointer = (GenericType*)(NativeMemory.Alloc((nuint)(sizeof(GenericType) * this.capacity)));
		}

		/// <summary>
		///		Constructor of `NativeList`.
		/// </summary>
		/// <param name="capacity"></param>
		public NativeList(uint capacity)
		{
			this.size = 0;

			if (capacity == 0)
			{
				this.capacity = 1;
			}
			else
			{
				this.capacity = capacity;
			}

			this.bufferPointer = (GenericType*)(NativeMemory.Alloc((nuint)(sizeof(GenericType) * this.capacity)));
		}

		/// <summary>
		///		Copy constructor of `NativeList`.
		/// </summary>
		/// <param name="other"></param>
		/// <exception cref="NativeNetException"></exception>
		public NativeList(NativeList<GenericType> other)
		{
			if (other is null)
			{
				throw new NativeNetException("Can't instantiate an instance of `NativeList` by the copy constructor of `NativeList` from a nulled argument `other`!");
			}
			else
			{
				this.size = other.size;
				this.capacity = other.capacity;
				this.bufferPointer = (GenericType*)(NativeMemory.Alloc((nuint)(sizeof(GenericType) * this.capacity)));
				int i = 0;

				for (i = 0;i < this.size;i++)
				{
					(this.bufferPointer)[i] = (other.bufferPointer)[i];
				}
			}
		}

		/// <summary>
		///		Destructor of `NativeList`.
		/// </summary>
		~NativeList()
		{
			if (this.bufferPointer is not null)
			{
				NativeMemory.Free(this.bufferPointer);
				this.size = 0;
				this.capacity = 0;
				this.bufferPointer = null;
			}
		}
		
		/// <summary>
		///		static
		///		operator==
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator==(NativeList<GenericType> first,NativeList<GenericType> second)
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
				return ((first.size == second.size) && ((new ReadOnlySpan<GenericType>(first.bufferPointer,(int)(first.size))) == (new ReadOnlySpan<GenericType>(second.bufferPointer,(int)(second.size)))));
			}
		}
		
		/// <summary>
		///		static
		///		operator!=
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>bool</returns>
		public static bool operator!=(NativeList<GenericType> first,NativeList<GenericType> second)
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
				return ((first.size != second.size) || ((new ReadOnlySpan<GenericType>(first.bufferPointer,(int)(first.size))) != (new ReadOnlySpan<GenericType>(second.bufferPointer,(int)(second.size)))));
			}
		}

		/// <summary>
		///		dynamic
		///		operator[]
		/// </summary>
		/// <param name="index"></param>
		/// <returns>GenericType</returns>
		public GenericType this[uint index]
		{
			get
			{
				if ((index >= this.size) || (index < 0))
				{
					NativeNetAuxiliary.throwOutOfBoundException(index);

					return default;
				}
				else
				{
					return (this.bufferPointer)[index];
				}
			}
			set
			{
				if ((index >= this.size) || (index < 0))
				{
					NativeNetAuxiliary.throwOutOfBoundException(index);
				}
				else
				{
					(this.bufferPointer)[index] = value;
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
			return ((other is NativeList<GenericType> instance) && (this == instance));
		}

		/// <summary>
		///		dynamic
		///		override
		/// </summary>
		/// <returns>int</returns>
		public override int GetHashCode()
		{
			return HashCode.Combine(this.size,this.capacity,(UIntPtr)(this.bufferPointer));
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>void</returns>
		public void Dispose()
		{
			if (this.bufferPointer is not null)
			{
				NativeMemory.Free(this.bufferPointer);
				this.bufferPointer = null;
			}

			this.size = 0;
			this.capacity = 0;
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>uint</returns>
		public uint count()
		{
			return this.size;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="element"></param>
		/// <returns>void</returns>
		public void add(GenericType element)
		{
			if (this.size >= this.capacity)
			{
				this.increaseCapacity();
			}

			this.size = this.size + 1;
			(this.bufferPointer)[this.size - 1] = element;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="index"></param>
		/// <param name="element"></param>
		/// <returns>void</returns>
		public void insert(int index,GenericType element)
		{
			if ((index >= this.size) || (index < 0))
			{
				throw new NativeNetException("Argument `index` is out of bound: `" + Convert.ToString(index) + "`.");
			}
			else
			{
				this.size = this.size + 1;

				if (this.size >= this.capacity)
				{
					this.increaseCapacity();
				}

				uint i = 0;

				for (i = this.size;i > index;i--)
				{
					(this.bufferPointer)[i] = (this.bufferPointer)[i - 1];
				}

				(this.bufferPointer)[index] = element;
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="element"></param>
		/// <returns>void</returns>
		/// <exception cref="NativeNetException"></exception>
		public void remove(GenericType element)
		{
			uint foundedIndex = (uint)(new ReadOnlySpan<GenericType>(this.bufferPointer,(int)(this.size))).IndexOf(element);

			if (foundedIndex >= 0)
			{
				this.removeByIndex(foundedIndex);
			}
			else
			{
				throw new NativeNetException("Argument `element` isn't found in the current instance of `NativeList`!");
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="index"></param>
		/// <returns>void</returns>
		/// <exception cref="NativeNetException"></exception>
		public void removeByIndex(uint index)
		{
			if ((index >= this.size) || (index < 0))
			{
				throw new NativeNetException("Argument `index` is out of bound: `" + Convert.ToString(index) + "`.");
			}
			else
			{
				uint i = 0;

				for (i = index;i < (this.size - 1);i++)
				{
					(this.bufferPointer)[i] = (this.bufferPointer)[i + 1];
				}

				this.size = this.size - 1;
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="element"></param>
		/// <returns>uint</returns>
		/// <exception cref="NativeNetException"></exception>
		public uint getFirstIndexOfElement(GenericType element)
		{
			uint foundedIndex = (uint)(new ReadOnlySpan<GenericType>(this.bufferPointer,(int)(this.size))).IndexOf(element);

			if (foundedIndex >= 0)
			{
				return foundedIndex;
			}
			else
			{
				throw new NativeNetException("Argument `element` isn't found in the current instance of `NativeList`!");
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="element"></param>
		/// <returns>uint</returns>
		/// <exception cref="NativeNetException"></exception>
		public uint getLastIndexOfElement(GenericType element)
		{
			uint foundedIndex = (uint)(new ReadOnlySpan<GenericType>(this.bufferPointer,(int)(this.size))).LastIndexOf(element);

			if (foundedIndex >= 0)
			{
				return foundedIndex;
			}
			else
			{
				throw new NativeNetException("Argument `element` isn't found in the current instance of `NativeList`!");
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>void</returns>
		public void clear()
		{
			if ((this.bufferPointer is not null) && (this.capacity > 0))
			{
				int i = 0;

				for (i = 0;i < this.capacity;i++)
				{
					(this.bufferPointer)[i] = default;
				}

				this.size = 0;
				this.capacity = 0;
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>List&lt;GenericType&gt;</returns>
		public List<GenericType> convertToStandardList()
		{
			List<GenericType> result = new List<GenericType>();
			int i = 0;

			for (i = 0;i < this.size;i++)
			{
				result.Add((this.bufferPointer)[i]);
			}

			return result;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>ReadOnlySpan&lt;GenericType&gt;</returns>
		public ReadOnlySpan<GenericType> asReadOnlySpan()
		{
			return new ReadOnlySpan<GenericType>(this.bufferPointer,(int)(this.size));
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>void</returns>
		private void increaseCapacity()
		{
			if (this.capacity == 0)
			{
				this.capacity = this.capacity + 1;
				GenericType* newBufferPointer = (GenericType*)(NativeMemory.Alloc((nuint)(sizeof(GenericType) * this.capacity)));
				NativeMemory.Free(this.bufferPointer);
				this.bufferPointer = newBufferPointer;
			}
			else
			{
				this.capacity = this.capacity * 2;
				GenericType* newBufferPointer = (GenericType*)(NativeMemory.Alloc((nuint)(sizeof(GenericType) * this.capacity)));
				int i = 0;

				for (i = 0;i < this.size;i++)
				{
					newBufferPointer[i] = (this.bufferPointer)[i];
				}

				NativeMemory.Free(this.bufferPointer);
				this.bufferPointer = newBufferPointer;
			}
		}
	};
};