using DivinityModManager.Views;

using System.Windows;

namespace DivinityModManager.Util;

class RxExceptionHandler : IObserver<Exception>
{
	public static MainWindow view { get; set; }
	public void OnNext(Exception value)
	{
		//if (Debugger.IsAttached) Debugger.Break();

		var message = $"程序处理事件时发生异常：\n类型：{value.GetType()}\t消息：{value.Message}\n来源：{value.Source}\n堆栈跟踪：{value.StackTrace}";
		DivinityApp.Log(message);
		MessageBox.Show(message, "发生错误", MessageBoxButton.OK, MessageBoxImage.Error);
		//MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(view, message, "Error Encountered", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, view.MainWindowMessageBox_OK.Style);
		//RxApp.MainThreadScheduler.Schedule(() => { throw value; });
	}

	public void OnError(Exception value)
	{
		var message = $"程序处理错误时发生异常：\n类型：{value.GetType()}\t消息：{value.Message}\n来源：{value.Source}\n堆栈跟踪：{value.StackTrace}";
		DivinityApp.Log(message);
		//MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(view, message, "Error Encountered", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, view.MainWindowMessageBox_OK.Style);
	}

	public void OnCompleted()
	{
		//if (Debugger.IsAttached) Debugger.Break();
		//RxApp.MainThreadScheduler.Schedule(() => { throw new NotImplementedException(); });
	}
}
