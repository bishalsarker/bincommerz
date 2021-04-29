using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Repositories.Common
{
    public static class TableNameConstants
    {
        public static readonly string SchemaName = "bcomm_pm";
        public static readonly string TagsTable = SchemaName + "." + "tags";
        public static readonly string ProductsTable = SchemaName + "." + "products";
        public static readonly string ProductTagsTable = SchemaName + "." + "product_tags";
        public static readonly string ImagesTable = SchemaName + "." + "images";
        public static readonly string ImageGalleryTable = SchemaName + "." + "image_gallery";
    }
}
