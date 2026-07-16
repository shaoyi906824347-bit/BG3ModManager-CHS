

using DivinityModManager.Models;
using DivinityModManager.Util;

using DynamicData;
using DynamicData.Binding;

using System.Windows;

namespace DivinityModManager.ViewModels;

public class FileDeletionCompleteEventArgs : EventArgs
{
	public int TotalFilesDeleted => DeletedFiles?.Count ?? 0;
	public List<ModFileDeletionData> DeletedFiles { get; set; }
	public bool RemoveFromLoadOrder { get; set; }
	public bool IsDeletingDuplicates { get; set; }

	public FileDeletionCompleteEventArgs()
	{
		DeletedFiles = [];
	}
}

public class DeleteFilesViewData : BaseProgressViewModel
{
	[Reactive] public bool PermanentlyDelete { get; set; }
	[Reactive] public bool RemoveFromLoadOrder { get; set; }
	[Reactive] public bool IsDeletingDuplicates { get; set; }
	[Reactive] public double DuplicateColumnWidth { get; set; }

	public ObservableCollectionExtended<ModFileDeletionData> Files { get; set; } = new ObservableCollectionExtended<ModFileDeletionData>();

	private readonly ObservableAsPropertyHelper<bool> _anySelected;
	public bool AnySelected => _anySelected.Value;

	private readonly ObservableAsPropertyHelper<bool> _allSelected;
	public bool AllSelected => _allSelected.Value;

	private readonly ObservableAsPropertyHelper<string> _selectAllTooltip;
	public string SelectAllTooltip => _selectAllTooltip.Value;

	private readonly ObservableAsPropertyHelper<string> _title;
	public string Title => _title.Value;

	private readonly ObservableAsPropertyHelper<Visibility> _removeFromLoadOrderVisibility;
	public Visibility RemoveFromLoadOrderVisibility => _removeFromLoadOrderVisibility.Value;

	public RxCommandUnit SelectAllCommand { get; private set; }

	public event EventHandler<FileDeletionCompleteEventArgs> FileDeletionComplete;

	public override async Task<bool> Run(CancellationToken cts)
	{
		var targetFiles = Files.Where(x => x.IsSelected).ToList();

		await UpdateProgress("正在确认删除...", "", 0d);

		var result = await DivinityInteractions.ConfirmModDeletion.Handle(new DeleteFilesViewConfirmationData { Total = targetFiles.Count, PermanentlyDelete = PermanentlyDelete, Token = cts });
		if (result)
		{
			var eventArgs = new FileDeletionCompleteEventArgs()
			{
				IsDeletingDuplicates = IsDeletingDuplicates,
				RemoveFromLoadOrder = !IsDeletingDuplicates && RemoveFromLoadOrder,
			};

			await Observable.Start(() => IsProgressActive = true, RxApp.MainThreadScheduler);
			await UpdateProgress($"正在删除 {targetFiles.Count} 个模组文件...", "", 0d);
			double progressInc = 1d / targetFiles.Count;
			foreach (var f in targetFiles)
			{
				try
				{
					if (cts.IsCancellationRequested)
					{
						DivinityApp.Log("Deletion stopped.");
						break;
					}
					if (File.Exists(f.FilePath))
					{
						await UpdateProgress("", $"正在删除 {f.FilePath}...");
#if DEBUG
						eventArgs.DeletedFiles.Add(f);
#else
						if (RecycleBinHelper.DeleteFile(f.FilePath, false, PermanentlyDelete))
						{
							eventArgs.DeletedFiles.Add(f);
							DivinityApp.Log($"Deleted mod file '{f.FilePath}'");
						}
#endif
					}
				}
				catch (Exception ex)
				{
					DivinityApp.Log($"Error deleting file '${f.FilePath}':\n{ex}");
				}
				await UpdateProgress("", "", ProgressValue + progressInc);
			}
			await UpdateProgress("", "", 1d);
			await Task.Delay(500);
			RxApp.MainThreadScheduler.Schedule(() =>
			{
				FileDeletionComplete?.Invoke(this, eventArgs);
				Close();
			});
		}
		return true;
	}

	public override void Close()
	{
		base.Close();
		Files.Clear();
	}

	public void ToggleSelectAll()
	{
		var b = !AllSelected;
		foreach (var f in Files)
		{
			f.IsSelected = b;
		}
	}

	private bool IsClosingAllowed(bool isDeletingDupes, int totalFiles) => !isDeletingDupes || totalFiles <= 0;

	public DeleteFilesViewData() : base()
	{
		RemoveFromLoadOrder = true;
		PermanentlyDelete = false;

		//this.WhenAnyValue(x => x.IsDeletingDuplicates, x => x.Files.Count).Select(x => IsClosingAllowed(x.Item1, x.Item2)).BindTo(this, x => x.CanClose);

		_removeFromLoadOrderVisibility = this.WhenAnyValue(x => x.IsDeletingDuplicates).Select(x => x ? Visibility.Collapsed : Visibility.Visible).ToProperty(this, nameof(RemoveFromLoadOrderVisibility), true, RxApp.MainThreadScheduler);
		_title = this.WhenAnyValue(x => x.IsDeletingDuplicates).Select(b => !b ? "待删除的文件" : "待删除的重复模组").ToProperty(this, nameof(Title), true, RxApp.MainThreadScheduler);

		var filesChanged = this.Files.ToObservableChangeSet().AutoRefresh(x => x.IsSelected).ToCollection().Throttle(TimeSpan.FromMilliseconds(50)).ObserveOn(RxApp.MainThreadScheduler);
		_anySelected = filesChanged.Select(x => x.Any(y => y.IsSelected)).ToProperty(this, nameof(AnySelected));

		_allSelected = filesChanged.Select(x => x.All(y => y.IsSelected)).ToProperty(this, nameof(AllSelected), true, RxApp.MainThreadScheduler);
		_selectAllTooltip = this.WhenAnyValue(x => x.AllSelected).Select(b => b ? "取消全选" : "全选").ToProperty(this, nameof(SelectAllTooltip), true, RxApp.MainThreadScheduler);

		SelectAllCommand = ReactiveCommand.Create(ToggleSelectAll, this.RunCommand.IsExecuting.Select(b => !b), RxApp.MainThreadScheduler);

		this.WhenAnyValue(x => x.AnySelected).BindTo(this, x => x.CanRun);
	}
}
