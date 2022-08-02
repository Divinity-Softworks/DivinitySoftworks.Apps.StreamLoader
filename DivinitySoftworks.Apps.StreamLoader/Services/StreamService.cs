using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.StreamLoader.Core.Configuration;
using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using DivinitySoftworks.Apps.StreamLoader.Data.Models;
using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.StreamLoader.Services {

    public interface IStreamService {

        Task<StreamItem> AddAsync(Uri uri, StreamType streamType);
    }

    public class StreamService : IStreamService {
        readonly IAppSettings _appSettings;
        readonly IConfigurationManager _configurationManager;
        readonly IMediator _mediator;

        public StreamService(IAppSettings appSettings, IConfigurationManager configurationManager, IMediator mediator) {
            _appSettings = appSettings;
            _configurationManager = configurationManager;
            _mediator = mediator;
        }

        public async Task<StreamItem> AddAsync(Uri uri, StreamType streamType) {
            if (!Directory.Exists(_appSettings.Folder))
                Directory.CreateDirectory(_appSettings.Folder);

            string? fileType = await _configurationManager.GetUserSettingAsync<string>("FileType");
            if (string.IsNullOrWhiteSpace(fileType))
                throw new NullReferenceException(nameof(fileType));

            StreamItem item = new(uri, _appSettings.Folder, streamType, fileType);

            if (_mediator is not null)
                _ = _mediator.Send(item);

            return item;
        }
    }
}
