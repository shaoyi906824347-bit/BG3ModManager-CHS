

using DivinityModManager.Models;

using DynamicData;
using DynamicData.Binding;

using System.Collections.ObjectModel;

namespace DivinityModManager.ViewModels;

public enum ExportOrderFileType
{
	[SettingsEntry("标准 JSON（推荐）", "模组管理器默认使用的 .json 加载顺序格式，适合备份和分享")]
	DefaultJson,
	[SettingsEntry("详细 JSON", "除加载顺序外，还包含作者、说明、标签等模组信息")]
	DetailedJson,
	[SettingsEntry("TSV 表格", "可用 Excel 等表格软件打开，包含每个模组的详细信息")]
	TSV
}

public class ExportOrderFileEntry : ReactiveObject
{
	[Reactive] public bool IsSelected { get; set; }
	[Reactive] public bool IsVisible { get; set; }
	[Reactive] public DivinityModData Mod { get; set; }

	public ExportOrderFileEntry()
	{
		IsVisible = true;
		IsSelected = true;
	}
}

public class ExportOrderToArchiveViewModel : BaseProgressViewModel
{
	[Reactive] public string OutputPath { get; set; }
	[Reactive] public bool IncludeOverrides { get; set; }
	[Reactive] public ExportOrderFileType SelectedOrderType { get; set; }

	private readonly ObservableCollectionExtended<ExportOrderFileType> _orderTypes;

	public ObservableCollectionExtended<ExportOrderFileType> OrderTypes => _orderTypes;

	private readonly ObservableCollectionExtended<ExportOrderFileEntry> _entries;

	protected ReadOnlyObservableCollection<ExportOrderFileEntry> _visibleEntries;
	public ReadOnlyObservableCollection<ExportOrderFileEntry> Entries => _visibleEntries;

	private readonly ObservableAsPropertyHelper<bool> _anySelected;
	public bool AnySelected => _anySelected.Value;

	private readonly ObservableAsPropertyHelper<bool> _allSelected;
	public bool AllSelected => _allSelected.Value;

	private readonly ObservableAsPropertyHelper<string> _selectAllTooltip;
	public string SelectAllTooltip => _selectAllTooltip.Value;

	public RxCommandUnit SelectAllCommand { get; private set; }

	public override async Task<bool> Run(CancellationToken cts)
	{
		//Only visible + selected entries
		var exportedMods = Entries.Where(x => x.IsSelected);

		return true;
	}

	public override void Close()
	{
		base.Close();
		_entries.Clear();
	}

	public void ToggleSelectAll()
	{
		var b = !AllSelected;
		foreach (var f in Entries)
		{
			f.IsSelected = b;
		}
	}

	public ExportOrderToArchiveViewModel() : base()
	{
		IncludeOverrides = true;
		CanRun = true;

		_orderTypes = new ObservableCollectionExtended<ExportOrderFileType>(Enum.GetValues(typeof(ExportOrderFileType)).Cast<ExportOrderFileType>());

		_entries = new ObservableCollectionExtended<ExportOrderFileEntry>();

		var changeSet = _entries.ToObservableChangeSet();
		changeSet.Filter(x => x.IsVisible).ObserveOn(RxApp.MainThreadScheduler).Bind(out _visibleEntries).Subscribe();

		var filesChanged = changeSet.AutoRefresh(x => x.IsSelected).ToCollection().Throttle(TimeSpan.FromMilliseconds(50)).ObserveOn(RxApp.MainThreadScheduler);
		_anySelected = filesChanged.Select(x => x.Any(y => y.IsSelected)).ToProperty(this, nameof(AnySelected));

		_allSelected = filesChanged.Select(x => x.All(y => y.IsSelected)).ToProperty(this, nameof(AllSelected), true, RxApp.MainThreadScheduler);
		_selectAllTooltip = this.WhenAnyValue(x => x.AllSelected).Select(b => b ? "取消全选" : "全选").ToProperty(this, nameof(SelectAllTooltip), true, RxApp.MainThreadScheduler);

		SelectAllCommand = ReactiveCommand.Create(ToggleSelectAll, this.RunCommand.IsExecuting.Select(b => !b), RxApp.MainThreadScheduler);

		this.WhenAnyValue(x => x.IncludeOverrides).Subscribe(b =>
		{
			foreach (var entry in _entries)
			{
				if (entry.Mod.IsForceLoaded && !entry.Mod.IsForceLoadedMergedMod)
				{
					entry.IsVisible = b;
				}
			}
		});
	}
}
