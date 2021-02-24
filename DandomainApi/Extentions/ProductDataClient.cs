using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DandomainApi
{
    public partial class ProductDataClientCustom
    {
        private Uri BaseUrl;
        private readonly string websericeKey;
        private readonly HttpClient httpClient;

        public ProductDataClientCustom(Uri RootUrl, string WebsericeKey)
        {
            BaseUrl = RootUrl;
            websericeKey = WebsericeKey;
        }

        public ProductDataClientCustom(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async System.Threading.Tasks.Task<IPagedResponseDtoOfProductDataResponseDto> GetListAsync(string barcode, System.DateTimeOffset? modificationDate, System.DateTimeOffset? modifiedStartDate, System.DateTimeOffset? modifiedEndDate, IList<Include31> include, int? limit, int? offset)
        {
            string url = BaseUrl.ToString();

            url = url.AppendPathSegment("/v2/products");

            if (barcode != null)
            {
                url = url.SetQueryParam("barcode", barcode);
            }

            if (modificationDate != null)
            {
                url = url.SetQueryParam("modificationDate", modificationDate.Value.ToString("s"));
            }
            if (modifiedStartDate != null)
            {
                url = url.SetQueryParam("modifiedStartDate", modifiedStartDate.Value.ToString("s"));
            }
            if (modifiedEndDate != null)
            {
                url = url.SetQueryParam("modifiedEndDate", modifiedEndDate.Value.ToString("s"));
            }
            if (include != null)
            {
                url = url.SetQueryParam("include", string.Join(",", include).ToLower(), true);
            }
            if (limit != null)
            {
                url = url.SetQueryParam("limit", limit);
            }
            if (offset != null)
            {
                url = url.SetQueryParam("offset", offset);
            }

            return await url
    .WithHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(":" + websericeKey))}")
    .GetJsonAsync<IPagedResponseDtoOfProductDataResponseDto>();
        }
    }
}
