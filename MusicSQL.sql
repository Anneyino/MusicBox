create database MusicBox;
use MusicBox;
Create table Songs(song_id int auto_increment,
                   song_name varchar(100) unique not null,
                   song_path varchar(200) not null,
                   publish_date date,
                   primary key(song_id)
);
insert into Songs(song_name,song_path,publish_date) VALUES("Joker","SongList/Joker.mp3",'2018-5-29');
insert into Songs(song_name,song_path,publish_date) VALUES("Shiny Ray","SongList/Shiny Ray.mp3",'2017-1-10');
insert into Songs(song_name,song_path,publish_date) VALUES("wadanohara","SongList/wadanohara.mp3",'2015-2-12');