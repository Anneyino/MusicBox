   M S B A M L   `   `   `  ����C  ?MusicBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MusicBoxMusicBox  5   	       MusicBox.Help_Interface   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MusicBox     5   	     � Title$  帮助��5   	   $��424.688��6   $��443.215��6%    � 
ResizeMode$ NoResize=�65   ���	#FFD0D90F.��5	      � $��	0,0,3.6,3q�6    � ColumnDefinitions 5
   
   �� 5      �� 5      $��0* �6   .+�5   
   �� $
��Left=�6   $	��372��6-   $��	10,10,0,0q�6:    �� TextWrapping$
 Wrap=�6M   $�	���	产品介绍：

本应用属于一个基于Mysql数据库的音乐播放软件，允许用户从本地选取音乐文件进行自主播放或是保存到本地的数据库中；同样地，用户可以将数据库中的音乐进行直接地播放。值得一提的是保存在数据库中的音乐的主要信息只包含歌曲名称，歌曲路径以及出版日期，实际读取的是用户本地加载完成的音乐文件，因此如果用户在程序外主动删除音乐文件会造成音乐文件的缺失而无法播放数据库中的音乐，请多加注意。


MySQL登录：

可以选择本机的mysql用户进行登录来连接到本地数据库，注意登陆前需要把目录下的Music.sql脚本文件执行成功来导入数据库，否则会提示未导入数据。


操作方法：

与大多数音乐播放软件相同，本程序包括歌曲的时长进度条的显示，音乐的暂停与继续，同时还支持3种基本的播放模式：单曲播放，单曲循环以及列表循环。其中列表循环是通过把当前导入的歌曲加入当前歌单中来实现的。注意每次启动程序时，初始的当前歌单总为空。
��6a   $	��Top=�6|  $	��413��6�  {�.  ���	#FFE7FF2A$
��Bold"�6�  5      5      