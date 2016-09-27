using AForge.Video.FFMPEG;
using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;

namespace Challange.Domain.Entities
{
    public class ChallengeWriter
    {
        private List<Video> videos;
        private string pathToVideos;
        private int width;
        private int height;
        private Dictionary<string, string> camerasNames;

        private IChallengeBuffer challengeBuffers;

        public ChallengeWriter(Dictionary<string, string> camerasNames, string pathToVideos)
        {
            this.videos = UnitePastAndFutureFrames();
            this.pathToVideos = pathToVideos;
            this.width = GetWidth();
            this.height = GetHeight();
            this.camerasNames = camerasNames;
        }

        public void WriteChallenge()
        {
            WriteVideo();
        }

        private void WriteVideo()
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                string tmpFileName;
                foreach (var video in videos)
                {
                    tmpFileName = pathToVideos + video.Name + ".mp4";
                    writer.Open(tmpFileName, width,
                        height, video.FpsValue, VideoCodec.MPEG4);
                    foreach (var fps in video.FpsList)
                    {
                        foreach (var frame in fps.Frames)
                        {
                            try
                            {
                                writer.WriteVideoFrame(frame);
                            }
                            catch
                            {

                            }
                            finally
                            {
                                frame.Dispose();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Unites past and future frames collection in one
        /// </summary>
        /// <returns></returns>
        private List<Video> UnitePastAndFutureFrames()
        {
            var videos = new List<Video>();
            List<Fps> tempVideoFrames;
            string currentVideoName;
            foreach (var pastFrames in challengeBuffers.PastCameraRecords)
            {
                foreach (var futureFrames in challengeBuffers.FutureCameraRecords)
                {
                    if (pastFrames.Key == futureFrames.Key)
                    {
                        tempVideoFrames = new List<Fps>();
                        tempVideoFrames.AddRange(pastFrames.Value);
                        tempVideoFrames.AddRange(futureFrames.Value);
                        camerasNames.TryGetValue(
                                pastFrames.Key, out currentVideoName);
                        videos.Add(new Video(currentVideoName, tempVideoFrames));
                        break;
                    }
                }
            }
            return videos;
        }

        private int GetWidth()
        {
            return videos.First().FpsList[0].Frames[0].Width;
        }

        private int GetHeight()
        {
            return videos.First().FpsList[0].Frames[0].Height;
        }
    }
}
