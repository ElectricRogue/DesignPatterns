<Query Kind="Program" />

void Main()
{
	ChocolateBoiler.CBInstance.fill();
	ChocolateBoiler.CBInstance.boil();
	ChocolateBoiler.CBInstance.fill();
	ChocolateBoiler.CBInstance.isBoiled().Dump("Is Boiled");
	ChocolateBoiler.CBInstance.isEmpty().Dump("Is Empty");
	ChocolateBoiler.CBInstance.drain();
	ChocolateBoiler.CBInstance.isEmpty().Dump("Is Empty");
	
}

//not thread safe
public sealed class ChocolateBoiler
{
	private bool empty;
	private bool boiled;
	private static ChocolateBoiler CB = null;
	
	private ChocolateBoiler()
	{
		empty = true;
		boiled = false;
	}
	
	public static ChocolateBoiler CBInstance
	{
		get
		{
			if (CB == null)
			{
				CB = new ChocolateBoiler();
			}
			return CB;
		}
	}

	public void fill()
	{
		if (isEmpty())
		{
			empty = false;
			boiled = false;
			// fill the boiler with a milk/chocolate mixture
			"Filling".Dump();
		}
		else
		{
			"Already Filled".Dump();
		}
	}

	public void drain()
	{
		if (!isEmpty() && isBoiled())
		{
			// drain the boiled milk and chocolate
			empty = true;
			"Draining".Dump();
		}
		else
		{
			"Cannot drain".Dump();
		}
	}

	public void boil()
	{
		if (!isEmpty() && !isBoiled())
		{
			// bring the contents to a boil
			boiled = true;
			"Boiling".Dump();
		}
		else
		{
			"Boiler is empty".Dump();
		}
	}

	public bool isEmpty()
	{
		return empty;
	}

	public bool isBoiled()
	{
		return boiled;
	}
}

//thread safe with lazy intialization
public sealed class Singleton
{
	private Singleton()
	{
		
	}
	
	private static readonly Lazy<Singleton> singleInstance = new Lazy<Singleton>(()=> new Singleton());

	public static Singleton SingleInstance 
	{ 
		get 
		{
			return singleInstance.Value; 
		} 
	}
	
}

//thread safe with lock
public sealed class SingletonWithLock
{
	private static SingletonWithLock singletonInstance = null;
	private static readonly Object myObject = new Object();
	
	private SingletonWithLock()
	{
		
	}

	public static SingletonWithLock SingletonInstance
	{
		get
		{
			lock (myObject)
			{
				if (singletonInstance == null)
				{
					singletonInstance = new SingletonWithLock();
				}
				return singletonInstance;
			}
		}
	}
}

//thread safe but not that lazy
public sealed class MySingleton
{
	private MySingleton()
	{
	}
	
	static MySingleton()
	{
		
	}
	
	
	private static MySingleton myInstance = new MySingleton();

	public static MySingleton MyInstance
	{
		get
		{
			return myInstance;
		}
	}
}