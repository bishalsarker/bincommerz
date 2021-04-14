﻿using BComm.PM.Models.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ITagsQueryRepository
    {
        Task<IEnumerable<Tag>> GetTags(string shopId);
    }
}