using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory
{
	static Inventory instance;

	public static Inventory Instance 
	{
		get
		{
			if (instance == null)
			{
				instance = new Inventory();
			}
			return instance;
		}
	}

	Dictionary<string, int> items = new Dictionary<string, int>();
	private Inventory()
	{
	}
	public void Save(string fileName)
	{
		using (FileStream f = File.Open(Path.Combine(Application.persistentDataPath, fileName), FileMode.OpenOrCreate))
		{
			TextWriter tw = new StreamWriter(f);
			foreach (var item in items)
			{
				for (int i = 0; i < item.Value; i++)
				{
					tw.WriteLine(item.Key);
				}
			}
			
		}
	}
	public void Load(string fileName)
	{
		items.Clear();
		using (FileStream f = File.Open(Path.Combine(Application.persistentDataPath, fileName), FileMode.OpenOrCreate))
		{
			TextReader tr = new StreamReader(f);
			string item = tr.ReadLine();
			while (!string.IsNullOrEmpty(item))
			{
				AddItem(item);
				item = tr.ReadLine();
			}
		}
	}
	public bool HasItem(string name)
	{
		return HowMany(name) > 0;
	}
	public int HowMany(string name)
	{
		return items.ContainsKey(name) ? items[name] : 0;
	}
	public void AddItem(string name)
	{
		if (!items.ContainsKey(name))
		{
			items.Add(name, 1);
		}
		else
		{
			items[name]++;
		}
	}
	public void RemoveItem(string name)
	{
		if (items.ContainsKey(name))
		{
			items[name]--;
			if (items[name] < 1)
			{
				items.Remove(name);
			}
		}
	}
}