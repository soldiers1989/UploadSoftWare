using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;//引用此命名空间
using Windows.Storage;

namespace AppGPS.model
{
    public class LocationManager
    {
        //public async static Task<Geoposition> GetPosition()
        //{
        //    var accessStatus = await Geolocator.RequestAccessAsync();
        //    if (accessStatus != GeolocationAccessStatus.Allowed) throw new Exception();
        //    var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
        //    var position = await geolocator.GetGeopositionAsync();
        //    string lat= position.Coordinate.Latitude.ToString ();
        //    string lon= position.Coordinate.Longitude.ToString();
        //    string str = lat+ ","+ lon;
        //    wratedata(str);
        //    return position;
        //}

        public async static Task<string > GetPosition()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed) throw new Exception();
            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var position = await geolocator.GetGeopositionAsync();
            string lat = position.Coordinate.Latitude.ToString();
            string lon = position.Coordinate.Longitude.ToString();
            string str = lat + "," + lon;

            Global.isok = true;
            Global.datas = str;
            var save= await writefiledatas(str);
            return save;
        }

        public async static Task<string > writefiledatas(string data)
        {
            // Create sample file; replace if exists.
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("sample.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            Debug.Write("" + storageFolder.Path);
            //1.获取文件
            StorageFolder GetstorageFolder = ApplicationData.Current.LocalFolder;
            StorageFile GetsampleFile = await storageFolder.GetFileAsync("sample.txt");
            //通过调用 FileIO 类的 WriteTextAsync 方法，将文本写入文件
            await FileIO.WriteTextAsync(sampleFile, data);
            Global.Addr= storageFolder.Path;
            return storageFolder.Path;
        }


    }
}
