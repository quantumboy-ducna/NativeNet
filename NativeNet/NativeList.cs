/// <summary>
///		Legal & Licensing Information
/// </summary>
/// <remarks>
///		Required Notice: Copyright@2026 QuantumBoy1010 (https://www.github.com/QuantumBoy1010/)
/// </remarks>

#pragma warning disable CS0693
#pragma warning disable CS8500
#pragma warning disable IDE0047
#pragma warning disable IDE0054

/** Inclusion(s) of the standard C# namespace(s).**/
using System.Runtime.InteropServices;


/** Main code.**/

/// <summary>
///		C# namespace: `NativeNet`.
/// </summary>
namespace NativeNet
{
	/// <summary>
	///		C# class: `NativeList`.
	/// </summary>
	/// <typeparam name="GenericType"></typeparam>
	public unsafe sealed class NativeList<GenericType> : IDisposable
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
			this.bufferPointer = (GenericType*)(Marshal.AllocHGlobal(sizeof(GenericType) * this.capacity).ToPointer());
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

			this.bufferPointer = (GenericType*)(Marshal.AllocHGlobal(sizeof(GenericType) * this.capacity).ToPointer());
		}

		/// <summary>
		///		Copy constructor of `NativeList`.
		/// </summary>
		/// <param name="other"></param>
		/// <exception cref="Exception"></exception>
		public NativeList(NativeList<GenericType> other)
		{
			if (other is null)
			{
				throw new Exception("Can't instantiate an instance of `NativeList` by the copy constructor of `NativeList` from a nulled argument `other`!");
			}
			else
			{
				this.size = other.size;
				this.capacity = other.capacity;
				this.bufferPointer = (GenericType*)(Marshal.AllocHGlobal(sizeof(GenericType) * this.capacity).ToPointer());
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
			Marshal.FreeHGlobal((nint)(this.bufferPointer));
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
				return (this.bufferPointer)[index];
			}

			set
			{
				(this.bufferPointer)[index] = value;
			}
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
				throw new Exception("Argument `index` is out of bound: `" + Convert.ToString(index) + "`.");
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
		/// <typeparam name="GenericType"></typeparam>
		/// <param name="element"></param>
		/// <returns>void</returns>
		/// <exception cref="Exception"></exception>
		public void remove<GenericType>(GenericType element) where GenericType : IComparable<GenericType>
		{
			int i = 0;

			for (i = 0;i < this.size;i++)
			{
				if ((this.bufferPointer)[i].Equals(element) == true)
				{
					this.removeByIndex(i);
				}
			}

			throw new Exception("Argument `element` isn't found in the current instance of `NativeList`!");
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <param name="index"></param>
		/// <returns>void</returns>
		/// <exception cref="Exception"></exception>
		public void removeByIndex(int index)
		{
			if ((index >= this.size) || (index < 0))
			{
				throw new Exception("Argument `index` is out of bound: `" + Convert.ToString(index) + "`.");
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
		/// <typeparam name="GenericType"></typeparam>
		/// <param name="element"></param>
		/// <returns>int</returns>
		/// <exception cref="Exception"></exception>
		public int getFirstIndexOfElement<GenericType>(GenericType element) where GenericType : IComparable<GenericType>
		{
			int i = 0;

			for (i = 0;i < this.size;i++)
			{
				if ((this.bufferPointer)[i].Equals(element) == true)
				{
					return i;
				}
			}

			throw new Exception("Argument `element` isn't found in the current instance of `NativeList`!");
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <typeparam name="GenericType"></typeparam>
		/// <param name="element"></param>
		/// <returns>int</returns>
		/// <exception cref="Exception"></exception>
		public int getLastIndexOfElement<GenericType>(GenericType element) where GenericType : IComparable<GenericType>
		{
			int i = this.size - 1;

			for (i = (this.size - 1);i >= 0;i--)
			{
				if ((this.bufferPointer)[i].Equals(element) == true)
				{
					return i;
				}
			}

			throw new Exception("Argument `element` isn't found in the current instance of `NativeList`!");
		}

		/// <summary>
		///		dynamic
		/// </summary>
		/// <returns>void</returns>
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			Marshal.FreeHGlobal((nint)(this.bufferPointer));
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
				GenericType* newBufferPointer = (GenericType*)(Marshal.AllocHGlobal(sizeof(GenericType) * this.capacity).ToPointer());
				Marshal.FreeHGlobal((nint)(this.bufferPointer));
				this.bufferPointer = newBufferPointer;
			}
			else
			{
				this.capacity = this.capacity * 2;
				GenericType* newBufferPointer = (GenericType*)(Marshal.AllocHGlobal(sizeof(GenericType) * this.capacity).ToPointer());
				int i = 0;

				for (i = 0;i < this.size;i++)
				{
					newBufferPointer[i] = (this.bufferPointer)[i];
				}

				Marshal.FreeHGlobal((nint)(this.bufferPointer));
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