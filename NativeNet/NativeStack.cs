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

#pragma warning disable CS8500
#pragma warning disable IDE0001
#pragma warning disable IDE0003
#pragma warning disable IDE0047

/** Inclusion(s) of the standard C# namespace(s).**/
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# generic class: `NativeStack`.
	/// </summary>
	/// <typeparam name="GenericType"></typeparam>
	public unsafe sealed class NativeStack<GenericType> : IDisposable
	{
		private GenericType* bufferPointer;
		private int size;
		private int capacity;


		/// <summary>
		///		Constructor of `NativeStack`.
		/// </summary>
		public NativeStack()
		{
			this.bufferPointer = null;
			this.size = 0;
			this.capacity = 0;
		}

		/// <summary>
		///		Copy constructor of `NativeStack`.
		/// </summary>
		/// <param name="other"></param>
		/// <exception cref="NativeNetException"></exception>
		public NativeStack(NativeStack<GenericType> other)
		{
			if (other is null)
			{
				throw new NativeNetException("Can't instantiate an instance of `NativeStack` by the copy constructor of `NativeStack` from a nulled argument `other`!");
			}
			else
			{
				this.bufferPointer = (GenericType*)(NativeMemory.Alloc((nuint)(Unsafe.SizeOf<GenericType>() * other.capacity)));
				this.size = other.size;
				this.capacity = other.capacity;
			}
		}

		/// <summary>
		///		Destructor of `NativeStack`.
		/// </summary>
		~NativeStack()
		{
			if (this.bufferPointer != null)
			{
				NativeMemory.Free(this.bufferPointer);
				this.bufferPointer = null;
				this.size = 0;
				this.capacity = 0;
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
		public int count()
		{
			return this.size;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="element"></param>
		/// <returns>bool</returns>
		public bool contains(GenericType element)
		{
			if ((this.bufferPointer == null) || (this.size == 0) || (this.capacity == 0))
			{
				return false;
			}
			else
			{
				return ((new ReadOnlySpan<GenericType>(this.bufferPointer,this.size)).Contains(element) == true);
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="element"></param>
		/// <returns>void</returns>
		public void push(GenericType element)
		{
			if ((this.capacity < this.size) || (this.bufferPointer == null))
			{
				this.increaseCapacity();
			}

			(this.bufferPointer)[this.size] = element;
			(this.size)++;
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>GenericType</returns>
		/// <exception cref="NativeNetException"></exception>
		public GenericType pop()
		{
			if (this.bufferPointer == null)
			{
				throw new NativeNetException("The current instance of `NativeStack` is empty!");
			}
			else
			{
				(this.size)--;

				return (this.bufferPointer)[this.size + 1];
			}
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
		/// <exception cref="NativeNetException"></exception>
		/// <returns>void</returns>
		private void increaseCapacity()
		{
			try
			{
				if (this.capacity == 0)
				{
					this.capacity = 1;
				}

				void* reallocatedMemoryPointer = NativeMemory.Realloc(this.bufferPointer,(uint)(Unsafe.SizeOf<GenericType>() * this.capacity * 2));

				if (reallocatedMemoryPointer == null)
				{
					throw new NativeNetException("Can't increase the capacity of the current instance of `NativeStack`!");
				}
				else
				{
					this.bufferPointer = (GenericType*)reallocatedMemoryPointer;
					this.capacity *= 2;
				}
			}
			catch (OutOfMemoryException exception)
			{
				throw new NativeNetException(exception.Message);
			}
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="newCapacity"></param>
		/// <exception cref="NativeNetException"></exception>
		/// <returns>void</returns>
		private void reserveCapacity(int newCapacity)
		{
			try
			{
				void* reallocatedMemoryPointer = NativeMemory.Realloc(this.bufferPointer,(uint)(Unsafe.SizeOf<GenericType>() * newCapacity));

				if (reallocatedMemoryPointer == null)
				{
					throw new NativeNetException("Can't increase the capacity of the current instance of `NativeStack`!");
				}
				else
				{
					this.bufferPointer = (GenericType*)reallocatedMemoryPointer;
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
