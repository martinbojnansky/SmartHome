using SmartHome.Services.RemoteControl;
using SmartSockets;
using SmartSockets.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using Windows.Media.SpeechRecognition;

namespace SmartHome.Services.SpeechRecognition
{
    public class SpeechRecognitionService : ISingleResolvable
    {
        private SpeechRecognizer _speechRecognizer;
        private RemoteControlCommandProcessor _commandProcessor;

        private readonly string[] LIGHT_ON_COMMANDS = { "hello", "light please" };
        private readonly string[] LIGHT_OFF_COMMANDS = { "bye-bye", "good-night", "see you" };

        public SpeechRecognitionService(RemoteControlCommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public async Task StartAsync()
        {
            try
            {
                _speechRecognizer = new SpeechRecognizer(SpeechRecognizer.SystemSpeechLanguage);

                _speechRecognizer.Constraints.Add(new SpeechRecognitionListConstraint(LIGHT_ON_COMMANDS, nameof(LIGHT_ON_COMMANDS)));
                _speechRecognizer.Constraints.Add(new SpeechRecognitionListConstraint(LIGHT_OFF_COMMANDS, nameof(LIGHT_OFF_COMMANDS)));
                await _speechRecognizer.CompileConstraintsAsync();

                _speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
                _speechRecognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
                _speechRecognizer.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.MaxValue;
                await _speechRecognizer.ContinuousRecognitionSession.StartAsync();
            }
            catch
            {
                await StopAsync();
            }
        }

        private async void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            try
            {
#if DEBUG
                Debug.WriteLine($"SpeechRecognition recognized: {args.Result.Text} with status: {args.Result.Status.ToString()} and confidence: {args.Result.Confidence.ToString()} : {args.Result.RawConfidence}");
#endif

                if (args.Result.RawConfidence > 0.5)
                {
                    await ProcessSpeechResultText(args.Result.Text);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"SpeechRecognition threw exception: {ex.Message}");
#endif
                await StopAsync();
                await StartAsync();
            }
        }

        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
        {
            await _speechRecognizer.ContinuousRecognitionSession.StartAsync();
        }

        public async Task StopAsync()
        {
            await _speechRecognizer.ContinuousRecognitionSession.CancelAsync();
            _speechRecognizer.Dispose();
        }

        private async Task ProcessSpeechResultText(string text)
        {
            if (LIGHT_OFF_COMMANDS.Contains(text))
            {
                await _commandProcessor.ProcessCommandAsync(new SocketCommand(SocketCommandType.PIN_OFF, new object[] { Constants.PIN18 }));
            }
            else if (LIGHT_ON_COMMANDS.Contains(text))
            {
                await _commandProcessor.ProcessCommandAsync(new SocketCommand(SocketCommandType.PIN_ON, new object[] { Constants.PIN18 }));
            }
            else { }
        }
    }
}
