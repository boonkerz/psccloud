using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class Volume
    {
        #region # static properties #

        private static int _currentPage { get; set; }
        public static int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        private static int _maxPages { get; set; }
        public static int MaxPages
        {
            get
            {
                return _maxPages;
            }
            set
            {
                _maxPages = value;
            }
        }

        #endregion

        #region # public properties #
        public long Id { get; set; } = 0;

        public long Size { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public DateTimeOffset Created { get; set; }

        public VolumeProtection Protection { get; set; }

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all ssh-keys in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Volume>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<Volume> volumeList = new List<Volume>();

            string url = string.Format("/volumes");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Volume.Get.Response response = JsonConvert.DeserializeObject<Objects.Volume.Get.Response>(responseContent);
            
            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.Volume.Universal.Volume responseVolume in response.volumes)
            {
                Volume volume = GetVolumeFromResponseData(responseVolume);

                volumeList.Add(volume);
            }

            return volumeList;
        }

        /// <summary>
        /// Return a ssh-key by the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<Volume> GetAsync(long id)
        {
            Volume volume = new Volume();

            string url = string.Format("/volumes/{0}", id);

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Volume.GetOne.Response response = JsonConvert.DeserializeObject<Objects.Volume.GetOne.Response>(responseContent);
            
            volume = GetVolumeFromResponseData(response.volume);

            return volume;
        }

        #endregion

        #region # public methods #

        public Volume()
        {

        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static Volume GetVolumeFromResponseData(Objects.Volume.Universal.Volume responseData)
        {
            Volume volume = new Volume();

            volume.Id = responseData.id;
            volume.Name = responseData.name;
            volume.Created = responseData.created;
            volume.Size = responseData.size;
            volume.Protection = new VolumeProtection()
            {
                Delete = responseData.protection.delete
            };

            return volume;
        }

        #endregion
    }
}