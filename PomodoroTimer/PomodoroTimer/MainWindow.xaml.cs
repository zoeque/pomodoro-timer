using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PomodoroTimer
{
	public partial class MainWindow : Window
	{
		// タイマーオブジェクト
		private DispatcherTimer timer;
		// 残り時間（秒単位）
		private int remainingSeconds;
		// 作業時間か休憩時間かを示すフラグ
		private bool isWorkPeriod;
		// 作業時間の長さ（25分）
		private const int workDuration = 25 * 60;
		// 休憩時間の長さ（5分）
		private const int breakDuration = 5 * 60; 

#pragma warning disable CS8618 
		public MainWindow()
#pragma warning restore CS8618 
		{
			InitializeComponent();
			// タイマーの初期化
			InitializeTimer(); 
		}

		/// <summary>
		/// タイマーの初期化
		/// </summary>
		private void InitializeTimer()
		{
			timer = new DispatcherTimer();
			// タイマーの間隔を1秒に設定
			timer.Interval = TimeSpan.FromSeconds(1);
			// タイマーのTickイベントにハンドラを追加
			timer.Tick += Timer_Tick;
			// 初期の残り時間を作業時間に設定
			remainingSeconds = workDuration;
			// 初期状態を作業時間に設定
			isWorkPeriod = true;

			// タイマーを開始
			timer.Start(); 
		}

		/// <summary>
		/// タイマーのTickイベントハンドラ
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">Event args</param>
		private void Timer_Tick(object? sender, EventArgs e)
		{
			remainingSeconds--; // 残り時間を1秒減少
			UpdateCountdownText(); // カウントダウンテキストを更新
			UpdateBackgroundColor(); // 背景色を更新

			// 残り時間が0になった場合の処理
			if (remainingSeconds == 0)
			{
				isWorkPeriod = !isWorkPeriod; // 作業時間と休憩時間を切り替え
				remainingSeconds = isWorkPeriod ? workDuration : breakDuration; // 新しい残り時間を設定
				UpdateBackgroundColor(); // 背景色を更新
			}
		}

		/// <summary>
		/// カウントダウンテキストの表示
		/// </summary>
		private void UpdateCountdownText()
		{
			// 残り時間を分単位に変換
			int remainingMinutes = remainingSeconds / 60;

			// テキストブロックに表示
			countdownText.Text = remainingMinutes.ToString(); 
		}

		/// <summary>
		/// 背景色の更新
		/// </summary>
		private void UpdateBackgroundColor()
		{
			// 作業時間なら赤、休憩時間なら水色に設定
			backgroundEllipse.Fill = isWorkPeriod ? Brushes.Red : Brushes.LightBlue; 
		}

		/// <summary>
		/// ドラッグアンドドロップのイベントハンドラ
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// ウィンドウを移動
			this.DragMove();
		}
	}
}