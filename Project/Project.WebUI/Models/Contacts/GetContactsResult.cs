﻿using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models

{
    public class GetContactsResult
    {
        public List<Profile> Profiles { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}