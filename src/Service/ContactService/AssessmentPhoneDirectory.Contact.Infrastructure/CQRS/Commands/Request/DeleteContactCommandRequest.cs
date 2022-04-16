﻿using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request
{
   public class DeleteContactCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
    }
}
