using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using ModelSaber.Models;

namespace ModelSaber.Main.Services
{
    public class FileService
    {
        private static Dictionary<Guid, string> _uploadQueue = new();
        private object _lock = new();

        private static readonly MagickGeometry _geometry = new()
        {
            Greater = true,
            Height = 512,
            Width = 512,
            Less = false
        };

        public FileService()
        {
            var thread = new Thread(UploadScheduler);
            thread.Start();
        }

        private void UploadScheduler()
        {
            Thread.Sleep(Constants.UploadSleepTime);
            lock (_lock)
            {
                foreach (var (id, file) in _uploadQueue)
                {
                    //TODO add upload logic for storage bucket
                }
                _uploadQueue.Clear();
            }
        }

        public async Task HandleThumbnailFile(IFormFile file, Guid modelId, ThumbnailEnum thumbnailType)
        {
            await using var thumbnailInputStream = file.OpenReadStream();
            await using var thumbnailOutputStream = await (thumbnailType switch
            {
                ThumbnailEnum.Video => HandleThumbnailVideo(thumbnailInputStream),
                ThumbnailEnum.Image => HandleThumbnailImage(thumbnailInputStream),
                _ => throw new ArgumentException("Error processing thumbnail")
            });
            thumbnailOutputStream.Seek(0, SeekOrigin.Begin);
            var tmp = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), Constants.TempDirectory, Guid.NewGuid().ToString("N")));
            var stream = tmp.OpenWrite();
            await thumbnailOutputStream.CopyToAsync(stream);
            await stream.DisposeAsync();
            lock (_lock)
            {
                _uploadQueue.Add(modelId, tmp.FullName);
            }
        }
        
        private async Task<Stream> HandleThumbnailImage(Stream stream)
        {
            //check if image is 1:1 or larger or equal to 256x256
            using var image = new MagickImage(stream);
            var aspect = (float)image.Width / image.Height;
            if (Math.Abs(aspect - 1) > float.Epsilon)
                throw new FormatException("Aspect ratio not 1:1");
            if (image.Width < 256)
                throw new FormatException("Size not at or greater than 256x256");

            //resize image if its above 512x512
            image.InterpolativeResize(_geometry, PixelInterpolateMethod.Bilinear);
            
            //convert to webp
            var outputStream = new MemoryStream();
            await image.WriteAsync(outputStream, MagickFormat.WebP);
            return outputStream;
        }

        private async Task<Stream> HandleThumbnailVideo(Stream stream)
        {
            //creating temp file
            var file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), Constants.TempDirectory, Guid.NewGuid().ToString("N")));
            var tmp = file.OpenWrite();
            await stream.CopyToAsync(tmp);
            await tmp.DisposeAsync();
            
            //check if file is 1:1 or larger than 256x256
            var info = await FFProbe.AnalyseAsync(file.FullName);
            var width = info.VideoStreams[0].Width;
            var height = info.VideoStreams[0].Height;
            var aspect = (float)width / height;
            if (Math.Abs(aspect - 1) >= float.Epsilon)
            {
                file.Delete();
                throw new FormatException("Aspect ratio not 1:1");
            }

            if (width < 256)
            {
                file.Delete();
                throw new FormatException("Size not at or greater than 256x256");
            }
            
            //convert to webm and resize if over 512x512
            var outputStream = new MemoryStream();
            await FFMpegArguments
                .FromFileInput(file.FullName)
                .OutputToPipe(new StreamPipeSink(outputStream), options => options
                    .WithVideoCodec("libvpx-vp9")
                    .ForceFormat("webm")
                    .WithConstantRateFactor(12)
                    .WithFramerate(60)
                    .WithFastStart()
                    .ForcePixelFormat("yuv420p")
                    .WithVideoBitrate(500)
                    .DisableChannel(Channel.Audio)
                    .WithCustomArgument("-vf \"scale='min(512,iw)':-1\"")
                    .WithCustomArgument("-quality good")
                    .WithCustomArgument("-cpu-used 0"))
                .ProcessAsynchronously();
            
            file.Delete();
            
            return outputStream;
        }
    }
}
