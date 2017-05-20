using Shared.Models.Radio;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace SmartHome.Services.Radio
{
    public abstract class BackgroundMediaPlayerService
    {
        private Timer _volumeTimer;

        public const double VOLUME_STEP = 0.05;
        public bool IsAlarmRinging { get; set; } = false;
        public MediaPlaybackState GetState()
        {
            try { return BackgroundMediaPlayer.Current.PlaybackSession.PlaybackState; }
            catch { return MediaPlaybackState.None; }
        }
        public double Volume
        {
            get
            {
                return BackgroundMediaPlayer.Current.Volume;
            }
            set
            {
                BackgroundMediaPlayer.Current.Volume = value;
            }
        }

        public BackgroundMediaPlayerService()
        {
            BackgroundMediaPlayer.Current.Volume = VOLUME_STEP;
            BackgroundMediaPlayer.Current.IsLoopingEnabled = true;
            BackgroundMediaPlayer.Current.MediaFailed += Alarm_MediaFailed;
        }

        #region Playback

        public SocketResponse Play(RadioStation station)
        {
            try
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(station.Uri));
                BackgroundMediaPlayer.Current.Source = mediaPlaybackItem;

                return new SocketResponse(SocketResponseType.OK);
            }
            catch(Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.ToString()); 
            }
        }

        public SocketResponse Stop()
        {
            try
            {
                DismissAlarm();

                BackgroundMediaPlayer.Current.Source = null;
                return new SocketResponse(SocketResponseType.OK);
            }
            catch (Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.ToString());
            }
        }

        public SocketResponse VolumeUp()
        {
            try
            {
                DismissAlarm();

                if (Volume <= (1 - VOLUME_STEP))
                {
                    Volume = Volume + VOLUME_STEP;
                }
                return new SocketResponse(SocketResponseType.OK);
            }
            catch (Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.ToString());
            }
        }

        public SocketResponse VolumeDown()
        {
            try
            {
                DismissAlarm();

                if (Volume >= VOLUME_STEP)
                {
                    Volume = Volume - VOLUME_STEP;
                }
                return new SocketResponse(SocketResponseType.OK);
            }
            catch (Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.ToString());
            }
        }

        public SocketResponse Mute()
        {
            try
            {
                Volume = 0;
                return new SocketResponse(SocketResponseType.OK);
            }
            catch (Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.ToString());
            }
        }

        #endregion

        #region Alarm

        public void PlayAlarm(RadioStation station, double volume)
        {
            IsAlarmRinging = true;

            Mute();
            Play(station);

            _volumeTimer = new Timer(VolumeTimer_Tick, volume, 0, Timeout.Infinite);
        }

        private void DismissAlarm()
        {
            IsAlarmRinging = false;
        }

        private async void VolumeTimer_Tick(object volume)
        {     
            while (Volume < (double)volume && IsAlarmRinging)
            {
                Volume = Volume + VOLUME_STEP;
                await Task.Delay(60000);
            }

            _volumeTimer.Dispose();
        }

        private void Alarm_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
        {
            if (IsAlarmRinging)
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/alarm.mp3")));
                BackgroundMediaPlayer.Current.Source = mediaPlaybackItem;
            }
        }

        #endregion
    }
}
