using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Repositories.Common
{
    public static class TableNameConstants
    {
        public static readonly string PmSchemaName = "bcomm_pm";
        public static readonly string OmSchemaName = "bcomm_om";
        public static readonly string CmSchemaName = "bcomm_cm";
        public static readonly string UserSchemaName = "bcomm_user";

        public static readonly string TagsTable = PmSchemaName + "." + "tags";
        public static readonly string CategoriesTable = PmSchemaName + "." + "categories";
        public static readonly string ProductsTable = PmSchemaName + "." + "products";
        public static readonly string ProductTagsTable = PmSchemaName + "." + "product_tags";
        public static readonly string ImagesTable = PmSchemaName + "." + "images";
        public static readonly string ImageGalleryTable = PmSchemaName + "." + "image_gallery";

        public static readonly string ProcessTable = OmSchemaName + "." + "processes";
        public static readonly string OrdersTable = OmSchemaName + "." + "orders";
        public static readonly string DeliveryChargesTable = OmSchemaName + "." + "delivery_charges";
        public static readonly string OrderItemsTable = OmSchemaName + "." + "order_items";
        public static readonly string OrderProcessLogsTable = OmSchemaName + "." + "order_process_logs";
        public static readonly string OrderPaymentLogsTable = OmSchemaName + "." + "order_payment_logs";
        public static readonly string CouponsTable = OmSchemaName + "." + "coupons";

        public static readonly string PagesTable = CmSchemaName + "." + "pages";
        public static readonly string SlidersTable = CmSchemaName + "." + "sliders";
        public static readonly string SliderImagesTable = CmSchemaName + "." + "slider_images";
        public static readonly string TemplatesTable = CmSchemaName + "." + "templates";

        public static readonly string ShopsTable = UserSchemaName + "." + "shops";
        public static readonly string UsersTable = UserSchemaName + "." + "users";
        public static readonly string UrlMappingsTable = UserSchemaName + "." + "url_mappings";
    }
}
