namespace DivinityModManager.Models;

public class DivinityMissingModData
{
	public string Name { get; set; }
	public int Index { get; set; }
	public string UUID { get; set; }
	public string Author { get; set; }
	public bool IsDependency { get; set; }
	public List<string> RequiredBy { get; } = [];

	public override string ToString()
	{
		List<string> text = [];
		if (Index > 0)
		{
			text.Add($"{Index}. ");
		}
		if(!string.IsNullOrWhiteSpace(Name))
		{
			text.Add(Name);
		}
		else
		{
			text.Add(UUID);
		}
		if (!string.IsNullOrEmpty(Author))
		{
			text.Add("，作者：" + Author);
		}
		if (RequiredBy.Count > 0)
		{
			text.Add("，被以下模组依赖：" + string.Join('；', RequiredBy.Order().Distinct()));
		}
		return string.Join("", text);
	}

	public static DivinityMissingModData FromData(DivinityModData modData, bool isDependency = true, string[] requiredBy = null)
	{
		var data = new DivinityMissingModData
		{
			Name = modData.Name,
			UUID = modData.UUID,
			Author = modData.Author,
			Index = modData.Index,
			IsDependency = isDependency
		};
		if (requiredBy != null)
		{
			data.RequiredBy.AddRange(requiredBy);
		}
		return data;
	}

	public static DivinityMissingModData FromData(DivinityLoadOrderEntry modData, int index, bool isDependency = true, string[] requiredBy = null)
	{
		var data = new DivinityMissingModData
		{
			Name = modData.Name,
			UUID = modData.UUID,
			Index = index,
			IsDependency = isDependency
		};
		if (requiredBy != null)
		{
			data.RequiredBy.AddRange(requiredBy);
		}
		return data;
	}

	public static DivinityMissingModData FromData(ModuleShortDesc modData, int index, bool isDependency = true, string[] requiredBy = null)
	{
		var data = new DivinityMissingModData
		{
			Name = modData.Name,
			UUID = modData.UUID,
			Index = index,
			IsDependency = isDependency
		};
		if (requiredBy != null)
		{
			data.RequiredBy.AddRange(requiredBy);
		}
		return data;
	}
}
