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
		// 長い休憩時間の長さ（15分）
		private const int longBreakDuration = 15 * 60;
		// 休憩回数をカウントする変数
		private int breakCount;
		// タイマーが動作中かどうかを示すフラグ
		private bool isTimerRunning;
		// マウスの初期位置
		private Point mouseStartPosition;
		// ドラッグ中かどうかを示すフラグ
		private bool isDragging;

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
			// 休憩回数を初期化
			breakCount = 0;
			// タイマーが動作中であることを設定
			isTimerRunning = true; 

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
			// タイマー動作中であればカウントを実行する
			if (isTimerRunning)
			{
				// 残り時間を1秒減少
				remainingSeconds--;
				// カウントダウンテキストを更新
				UpdateCountdownText();
				// 背景色を更新
				UpdateBackgroundColor();

				// 残り時間が0になった場合の処理
				if (remainingSeconds == 0)
				{
					if (isWorkPeriod)
					{
						// 作業時間から休憩時間に切り替え
						isWorkPeriod = false;
						// 休憩回数をカウント
						breakCount++;

						if (breakCount % 4 == 0)
						{
							// 4回目の休憩の場合、休憩時間を長くとり、カウンタをリセットする
							remainingSeconds = longBreakDuration;
							breakCount = 0;
						}
						else
						{
							// 通常の休憩
							remainingSeconds = breakDuration;
						}
					}
					else
					{
						isWorkPeriod = true; // 休憩時間から作業時間に切り替え
						remainingSeconds = workDuration; // 作業時間を設定
					}
					UpdateBackgroundColor(); // 背景色を更新
				}
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
			if (isTimerRunning)
			{
				// 作業時間なら赤、休憩時間なら水色に設定
				backgroundEllipse.Fill = isWorkPeriod ? Brushes.Red : Brushes.LightBlue; 
			}
			else
			{
				// タイマー停止中は緑色に設定
				backgroundEllipse.Fill = Brushes.Green; 
			}
		}


		/// <summary>
	    /// ウィンドウをドラッグで移動するためのイベントハンドラ
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			mouseStartPosition = e.GetPosition(this); // マウスの初期位置を記録
			isDragging = false; // ドラッグフラグを初期化
		}

		/// <summary>
		/// ウィンドウをクリックしてタイマーを停止/再開するためのイベントハンドラ
	    /// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (!isDragging)
			{
				isTimerRunning = !isTimerRunning; // タイマーの動作状態を切り替え
				UpdateBackgroundColor(); // 背景色を更新
			}
		}

		/// <summary>
		/// マウス移動イベントハンドラ
		/// </summary>
		/// <param name="sender">sender</param>
	    /// <param name="e">event args</param>
		private void Window_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				Point currentPosition = e.GetPosition(this);
				if ((Math.Abs(currentPosition.X - mouseStartPosition.X) > SystemParameters.MinimumHorizontalDragDistance) ||
				(Math.Abs(currentPosition.Y - mouseStartPosition.Y) > SystemParameters.MinimumVerticalDragDistance))
				{
					// ドラッグフラグを設定
					isDragging = true;
					// ウィンドウを移動
					this.DragMove(); 
				}
			}
		}
	}
}