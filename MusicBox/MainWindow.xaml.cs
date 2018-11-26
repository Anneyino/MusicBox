﻿using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

namespace MusicBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread;
        string filepath = "";//当前在播放的音乐文件
        public bool isThreadActive = false;//线程是否处于活跃,用来安全的中止线程;
        MediaPlayer Mainplayer = new MediaPlayer();
        public bool isPlaying = false;//是否正处于播放状态
        //public bool isLooped = false;//是否处于单曲循环
        public int totalSeconds = 0;
        public TimeSpan currentTimeSpan = new TimeSpan(0, 0, 0);
        public TimeSpan incrementSpan = new TimeSpan(0, 0, 1);
        public ArrayList songList = new ArrayList();//当前歌单
        private Song currentSong;

        public enum playMode
        {
            SinglePlay = 0,
            SingleLoop = 1,
            ListLoop = 2
        }

        playMode mode = playMode.SinglePlay;
        public MainWindow()
        {
            InitializeComponent();
            CoverBox.Source = new BitmapImage(new Uri(@"Covers/cover1.jpg", UriKind.Relative));
            this.Closing += Window_Closing;
    
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            
            //modify part
            //modify part
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "MP3音乐文件|*.mp3|WAV音乐文件|*.wav";
            fileDialog.Title = "选择音乐文件";
            if (fileDialog.ShowDialog() == true)
            filepath = System.IO.Path.GetFullPath(fileDialog.FileName);
            if (File.Exists(filepath))
            {
                updateSong(filepath);
                /*根据打开的音乐文件创建音乐实例并将其加入当前歌单*/
                Song song = new Song();
                song.Song_name = StateLabel.Content.ToString();
                song.Song_path = filepath;
                song.Song_span = TotalTimeLabel.Content.ToString();
                currentSong = song;
            }
            else
            {
                MessageBox.Show("目标文件不存在");
            }
            //modify part
            

        }
        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            if (thread == null)
            {
                thread = new Thread(
                    new ThreadStart(() =>
                    {
                        for (int i = 0; i <= totalSeconds; i++)
                        {
                            this.MusicProgress.Dispatcher.BeginInvoke((ThreadStart)delegate {
                                this.MusicProgress.Value ++;
                                currentTimeSpan = currentTimeSpan.Add(incrementSpan);
                                string str = currentTimeSpan.ToString("mm\\:ss")+"/";
                                CurrentTimeLabel.Content = str;
                                if ((i == totalSeconds)&&(mode==playMode.SingleLoop))
                                {
                                    i = 0;
                                    this.MusicProgress.Value = 0;
                                    currentTimeSpan = new TimeSpan(0, 0, 0);
                                    Mainplayer.Open(new Uri(filepath, UriKind.Absolute));
                                    Mainplayer.Play();
                                    /*isPlaying = false;
                                    ControlLabel.Content = "播放";
                                    ControlButton.Content = "►";
                                    thread = null;*/
                                }
                            });
                            Thread.Sleep(1000);
                        }
                    }));
            }

            if (isPlaying == false)
            {
                isPlaying = true;
                Mainplayer.Play();
                ControlLabel.Content = "暂停";
                ControlButton.Content = "| |";
                
                if ((thread.ThreadState&ThreadState.Unstarted)!=0)
                {
                    thread.Start();
                }else if(thread.ThreadState == ThreadState.Suspended)
                {
                    thread.Resume();
                }
            }
            else
            {   
                isPlaying = false;
                Mainplayer.Pause();
                ControlLabel.Content = "播放";
                ControlButton.Content = "►";
                if (thread.ThreadState != ThreadState.Stopped)
                {
                    thread.Suspend();
                }
                else
                {
                    thread = null;
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {   
            MessageBoxResult result = MessageBox.Show("确定是退出吗？", "询问", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //关闭窗口
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
                Mainplayer.Stop();
            }
            //不关闭窗口
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void ChangeModelButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (isLooped)
            {
                isLooped = false;
                ChangeModelButton.Content = "顺序播放";
            }
            else
            {
                isLooped = true;
                ChangeModelButton.Content = "单曲循环";
            }*/
            if(mode == playMode.SinglePlay)
            {
                mode = playMode.SingleLoop;
                ChangeModelButton.Content = "单曲循环";
            }else if(mode == playMode.SingleLoop)
            {
                mode = playMode.ListLoop;
                ChangeModelButton.Content = "列表循环";
            }
            else
            {
                mode = playMode.SinglePlay;
                ChangeModelButton.Content = "单曲播放";
            }
            
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            int SongNo;
            if (songList.Contains(currentSong))
            {
                SongNo = songList.IndexOf(currentSong);
                if (SongNo + 1 < songList.Count)
                {
                    isPlaying = true;
                    ControlLabel.Content = "暂停";
                    ControlButton.Content = "| |";
                    currentSong = (Song)songList[SongNo + 1];
                    updateSong(currentSong.Song_path);
                    Mainplayer.Play();
                }
            }

        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            int SongNo;
            if (songList.Contains(currentSong))
            {
                SongNo = songList.IndexOf(currentSong);
                if (SongNo > 0)
                {
                    isPlaying = true;
                    ControlLabel.Content = "暂停";
                    ControlButton.Content = "| |";
                    currentSong = (Song)songList[SongNo - 1];
                    updateSong(currentSong.Song_path);
                    Mainplayer.Play();
                }
            }
        }

        private void AddToSongListButton_Click(object sender, RoutedEventArgs e)
        {
            /*根据打开的音乐文件创建音乐实例并将其加入当前歌单*/
            if (currentSong != null)
            {
                songList.Add(currentSong);
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("当前未选中歌曲！");
            }

        }

        private void updateSong(string path)
        {
            string songTitle = System.IO.Path.GetFileNameWithoutExtension(path);
            StateLabel.Content = songTitle;
            Mainplayer.Open(new Uri(path, UriKind.Absolute));

            /*重置数据*/

            if (thread != null)
            {
                if (thread.ThreadState != ThreadState.Stopped)
                {
                    if (thread.ThreadState == ThreadState.Suspended)
                    {
                        thread.Resume();
                        while (thread.ThreadState == ThreadState.Suspended)
                        {
                            Thread.Sleep(100);
                        }
                    }
                    thread.Abort();
                    while (thread.ThreadState != ThreadState.Aborted)
                    {
                        Thread.Sleep(100);
                    }

                }
            }

            thread = null;
            MusicProgress.Value = 0;
            CurrentTimeLabel.Content = "00:00/";
            currentTimeSpan = new TimeSpan(0, 0, 0);
            totalSeconds = 0;
            isPlaying = false;
            ControlLabel.Content = "播放";
            ControlButton.Content = "►";
            /*重置数据完成*/

            while (true)
            {
                ControlButton.IsEnabled = false;
                ControlButton.Content = "Loading..";
                if (Mainplayer.NaturalDuration.HasTimeSpan)
                {
                    TimeSpan t = Mainplayer.NaturalDuration.TimeSpan;

                    string timestr = t.ToString("mm\\:ss");
                    totalSeconds = (int)t.TotalSeconds;
                    MusicProgress.Maximum = totalSeconds;
                    TotalTimeLabel.Content = timestr;
                    ControlButton.IsEnabled = true;
                    ControlButton.Content = "►";
                    break;
                }
            }
        }
    }
}
