using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Repositories.Common
{
    public static class TableNameConstants
    {
        public static readonly string PmSchemaName = "bcomm_pm";
        public static readonly string OmSchemaName = "bcomm_om";

        public static readonly string TagsTable = PmSchemaName + "." + "tags";
        public static readonly string ProductsTable = PmSchemaName + "." + "products";
        public static readonly string ProductTagsTable = PmSchemaName + "." + "product_tags";
        public static readonly string ImagesTable = PmSchemaName + "." + "images";
        public static readonly string ImageGalleryTable = PmSchemaName + "." + "image_gallery";

        public static readonly string ProcessTable = OmSchemaName + "." + "processes";
        public static readonly string OrdersTable = OmSchemaName + "." + "orders";
        public static readonly string OrderItemsTable = OmSchemaName + "." + "order_items";
        public static readonly string OrderProcessLogsTable = OmSchemaName + "." + "order_process_logs";
        public static readonly string OrderPaymentLogsTable = OmSchemaName + "." + "order_payment_logs";
    }
}
