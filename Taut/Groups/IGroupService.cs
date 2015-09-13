﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Groups
{
    public interface IGroupService
    {
        IObservable<BaseResponse> Archive(string channelId);

        IObservable<BaseResponse> Close(string channelId);

        IObservable<GroupResponse> Create(string name);

        IObservable<GroupResponse> CreateChild(string channelId);
    }
}
