﻿using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.BusinessCore.Interface
{
    public interface ISubscriptions
    {
        Task<GlobalResponse> Subscribe(SubscribeInput request);
        Task<GlobalResponse> UnSubscribe(UnSubscribeInput request);
        Task<SubscriptionCheckStatusResponse> CheckStatus(SubscriptionCheckStatusInput request);
    }
}
