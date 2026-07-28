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
	public unsafe sealed class NativeList<GenericType> : IDisposable where GenericType : IComparable<GenericType>
	{
		private int size;
		private int capacity;
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
		public NativeList(int capacity)
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
			NativeMemory.Free(this.bufferPointer);
			this.size = 0;
			this.capacity = 0;
			this.bufferPointer = null;
		}

		/// <summary>
		///		dynamic
		///		operator[]
		/// </summary>
		/// <param name="index"></param>
		/// <returns>GenericType</returns>
		public GenericType this[int index]
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
		/// </summary>
		/// <returns>void</returns>
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			this.clear();
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>int</returns>
		public int getSize()
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

				int i = 0;

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
			int i = 0;

			for (i = 0;i < this.size;i++)
			{
				if ((this.bufferPointer)[i].Equals(element) == true)
				{
					this.removeByIndex(i);
				}
			}

			throw new NativeNetException("Argument `element` isn't found in the current instance of `NativeList`!");
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="index"></param>
		/// <returns>void</returns>
		/// <exception cref="NativeNetException"></exception>
		public void removeByIndex(int index)
		{
			if ((index >= this.size) || (index < 0))
			{
				throw new NativeNetException("Argument `index` is out of bound: `" + Convert.ToString(index) + "`.");
			}
			else
			{
				int i = 0;

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
		/// <returns>int</returns>
		/// <exception cref="NativeNetException"></exception>
		public int getFirstIndexOfElement(GenericType element)
		{
			int i = 0;

			for (i = 0;i < this.size;i++)
			{
				if ((this.bufferPointer)[i].Equals(element) == true)
				{
					return i;
				}
			}

			throw new NativeNetException("Argument `element` isn't found in the current instance of `NativeList`!");
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="element"></param>
		/// <returns>int</returns>
		/// <exception cref="NativeNetException"></exception>
		public int getLastIndexOfElement(GenericType element)
		{
			int i = this.size - 1;

			for (i = (this.size - 1);i >= 0;i--)
			{
				if ((this.bufferPointer)[i].Equals(element) == true)
				{
					return i;
				}
			}

			throw new NativeNetException("Argument `element` isn't found in the current instance of `NativeList`!");
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>void</returns>
		public void clear()
		{
			if ((this.bufferPointer != null) && (this.capacity > 0))
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

/*public struct MainEntry
{
	public static void Main()
	{
		NativeNet.NativeList<int> list = new NativeNet.NativeList<int>();
		list.add(0);
		list.add(0);
		list.add(0);
		list.add(0);
		list.insert(0,0);
	}
};*/