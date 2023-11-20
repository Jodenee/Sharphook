﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharphook.Models
{
    internal class ApiResponce<AbstractedObject>
    {
        public AbstractedObject? ResponceObject { get; private set; }
        public HttpResponseMessage HttpResponse { get; private set; }

        public ApiResponce(AbstractedObject? responceObject, HttpResponseMessage httpResponse) 
        {
            ResponceObject = responceObject;
            HttpResponse = httpResponse;
        }
    }
}