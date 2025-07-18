using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
namespace _15TeamProject
{
    internal class AudioManager
    {
        private WaveOutEvent? bgmOutputDevice;  // 배경음악 재생용
        private AudioFileReader? bgmAudioFile;  // 배경음악 파일
        private bool bgmLooping = false; // 배경음악 반복 여부
        private Task? bgmTask; // 배경음악 재생을 위한 Task
        // 배경음악 재생 메서드
        public void PlayBgm(string path, float volume)
        {
            StopBgm();  // 이전 배경음악 중지 및 해제

            bgmLooping = true;

            bgmTask = Task.Run(() =>
            {
                do
                {
                    bgmAudioFile?.Dispose();                                    // 이전 파일이 있다면 해제
                    bgmOutputDevice?.Dispose();                                 // 이전 출력 장치가 있다면 해제

                    bgmAudioFile = new AudioFileReader($"D:\\C#\\TeamSparta\\15TeamProject\\15TeamProject\\GameSoundResources\\{path}");// 사운드파일 경로
                    bgmOutputDevice = new WaveOutEvent();                       // 새 출력 장치 생성
                    bgmOutputDevice.Init(bgmAudioFile);                        // 출력 장치에 오디오 파일 초기화   
                    bgmAudioFile.CurrentTime = TimeSpan.FromSeconds(3);        // 1초부터 시작
                    SetBgmVolume(volume);
                    bgmOutputDevice.Play();                                    // 배경음악 재생 시작
                    
                    while (bgmLooping && bgmOutputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
                while (bgmLooping);  // 반복 재생이 진행되고 있을 동안.
            });

           
        }

        // 다른 씬으로 이동하면 배경음악 멈추게 하기 위해 작성
        public void StopBgm()
        {
            bgmLooping = false;         // 배경음악 반복 재생 중지
            bgmOutputDevice?.Stop();    // 현재 재생 중인 배경음악 중지
            bgmAudioFile?.Dispose();    // 오디오 파일 해제
            bgmOutputDevice?.Dispose(); // 출력 장치 해제
            bgmAudioFile = null;        // 오디오 파일 참조 해제
            bgmOutputDevice = null;     // 출력 장치 참조 해제
            
            if (bgmTask != null && bgmTask.Status == TaskStatus.Running)
            {
                bgmTask.Wait(1000);     //배경음악 재생 끝날때 까지 최대 1초 대기
                bgmTask = null;
            }

        }

        // 배경음악 볼륨 설정
        public void SetBgmVolume(float volume)
        {
            if (bgmOutputDevice != null)
            {
                bgmAudioFile.Volume = volume; // 배경음악 볼륨 설정
            }
        }






        // /////////////////////////////////////  이펙트 메서드  ////////

        // 검 공격 사운드 재생
        public void PlaySwordSoundAsync()
        {

            Task.Run(() =>    // 비동기적 실행 : 콘솔 실행이랑 사운드 실행 별개로 둘다 진행될 수 있게
            {
                using (var audioFile = new AudioFileReader("D:\\C#\\TeamSparta\\15TeamProject\\15TeamProject\\GameSoundResources\\normalAttackSwordSound.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);  // 재생끝날때 까지 대기
                    }
                }
            });
        }

        // 사운드 이펙트 재생 메서드.. (경로, 볼륨, 소리 시작 시간 위치 설정)
        public void PlaySoundEffect(string path, float volume, float? startSeconds = 0.0f, float? endSeconds = null)
        {
            Task.Run(() =>
            {
                using (var audioFile = new AudioFileReader($"D:\\C#\\TeamSparta\\15TeamProject\\15TeamProject\\GameSoundResources\\{path}"))
                using (var outputDevice = new WaveOutEvent())
                {
                    audioFile.Volume = volume;
                    if (startSeconds > 0)
                        audioFile.CurrentTime = TimeSpan.FromSeconds((double)startSeconds);    // 시작 시간 설정

                    outputDevice.Init(audioFile);  // 오디오 파일 초기화
                    outputDevice.Play();

                    if (endSeconds == null)
                    {
                        // 소리가 끊기거나 에러 발생하는거 방지용
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);    // 재생이 끝날 때까지 대기
                        }
                    }
                    else
                    {
                        double endTime = (double)endSeconds.Value;
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            if (audioFile.CurrentTime.TotalSeconds >= endTime)
                            {
                                outputDevice.Stop();  // 지정된 시간에 도달하면 재생 중지
                                break;  // 반복문 종료
                            }
                            System.Threading.Thread.Sleep(50);
                        }
                    }
                }
            });
        }




        private static AudioManager? instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }

    }// 1. 배경 음악 - 스타트 씬, Pub 씬 완료, 배틀 씬 완료
    //  2. 사운드 이펙트 - 클릭 사운드(InputHelper.cs), 샵 사운드(Shop.cs)
    //                   - 포션 먹는 소리, 인벤토리 장착 소리
}
