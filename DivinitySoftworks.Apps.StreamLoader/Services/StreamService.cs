using DivinitySoftworks.Apps.StreamLoader.Core.Configuration;
using DivinitySoftworks.Apps.StreamLoader.Data.Models;
using MediatR;
using System;
using System.IO;

namespace DivinitySoftworks.Apps.StreamLoader.Services {

    public interface IStreamService {
        StreamItemRequest Add(Uri uri);
    }

    public class StreamService : IStreamService {
        readonly IAppSettings _appSettings;
        readonly IMediator _mediator;

        public StreamService(IAppSettings appSettings, IMediator mediator) {
            _appSettings = appSettings;
            _mediator = mediator;
        }

        public StreamItemRequest Add(Uri uri) {
            if(!Directory.Exists(_appSettings.Folder))
                Directory.CreateDirectory(_appSettings.Folder);

            StreamItemRequest item = new(uri, _appSettings.Folder);

            _mediator.Send(item);

            return item;
        }
    }
}
